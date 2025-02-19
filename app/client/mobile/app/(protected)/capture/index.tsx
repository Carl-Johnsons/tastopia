import { Canvas, rect, DiffRect, rrect } from "@shopify/react-native-skia";
import { Dimensions, TouchableWithoutFeedback } from "react-native";
import { EncodingType, readAsStringAsync } from "expo-file-system";
import { SafeAreaView } from "react-native-safe-area-context";
import { useCallback, useEffect, useRef, useState } from "react";
import { useIngredientPredictMutation } from "@/api/ingredient-predict";
import { useNavigation } from "expo-router";
import { useRouter } from "expo-router";
import { Worklets } from "react-native-worklets-core";
import Ionicons from "@expo/vector-icons/Ionicons";

import {
  Alert,
  GestureResponderEvent,
  Image,
  StyleSheet,
  Text,
  View
} from "react-native";
import {
  Camera,
  CameraDevice,
  CameraPosition,
  Frame,
  getCameraDevice,
  runAtTargetFps,
  useCameraDevices,
  useFrameProcessor
} from "react-native-vision-camera";
import { useFastApiWebsocket } from "@/hooks/capture";
import { FontAwesome } from "@expo/vector-icons";

const RNFS = require("react-native-fs");
const { width: windowWidth } = Dimensions.get("window");
// the value returned does not include the bottom navigation bar, I am not sure why yours does.

const Capture = () => {
  const [capturedImage, setCapturedImage] = useState("");
  const [device, setDevice] = useState<CameraDevice | undefined>(undefined);
  const [deviceCode, setDeviceCode] = useState<CameraPosition>("back");
  const [isCameraActive, setIsCameraActive] = useState(true);
  const [isImageView, setIsImageView] = useState(false);
  const [prediction, setPrediction] = useState<IngredientStreamResponse>();
  const { mutateAsync: predictAsync } = useIngredientPredictMutation();
  const [enableFlash, setEnableFlash] = useState(false);
  const camera = useRef<Camera>(null);
  const devices = useCameraDevices();
  const navigation = useNavigation();
  const router = useRouter();

  const wsRef = useFastApiWebsocket({
    onMessageHandler: event => {
      const response = JSON.parse(event.data) as IngredientStreamResponse;
      // setPrediction(response);
      setPrediction({
        classifications: response.classifications,
        boxes: [] // Disable predict box because I haven't find a way to send the buffer array to the server fast enough always delay 1 second
      });
    }
  });
  const [cameraContainerHeight, setCameraContainerHeight] = useState(0);

  // ===== Inferred value =====
  const ingredientPredict = prediction?.classifications?.[0].name ?? "";
  const isScreenFocused = navigation.isFocused();
  const isActive = isCameraActive && isScreenFocused;
  const shouldRenderCamera = device;

  const requestPermissionAsync = async () => {
    let finalPermissionStatus = Camera.getCameraPermissionStatus();
    if (finalPermissionStatus != "granted") {
      finalPermissionStatus = await Camera.requestCameraPermission();
    }

    if (finalPermissionStatus != "granted") {
      Alert.alert("In order to use the camera you must allowed camera permission!");
      router.back();
    }
  };

  useEffect(() => {
    requestPermissionAsync();
  }, []);

  const flipCamera = () => {
    if (deviceCode == "back") {
      setDeviceCode("front");
    } else {
      setDeviceCode("back");
    }
    setDevice(getCameraDevice(devices, deviceCode));
  };

  if (!device) {
    console.log("deviceCode: ", deviceCode);
    setDevice(getCameraDevice(devices, deviceCode));
  }

  useEffect(() => {
    if (deviceCode) {
      setDevice(getCameraDevice(devices, deviceCode));
    }
  }, [deviceCode]);

  const captureImage = useCallback(async () => {
    try {
      if (!camera.current) return;
      const snapshot = await camera.current.takePhoto({
        flash: enableFlash ? "on" : "off"
      });

      const file = {
        uri: `file://${snapshot.path}`,
        type: "image/jpeg",
        name: "file"
      };

      const predictResponse = await predictAsync({ file: file as unknown as Blob });
      setIsImageView(true);
      setPrediction(predictResponse);

      setCapturedImage(`file://${snapshot.path}`);

      // RNFS.unlink(snapshot.path);
    } catch (error) {
      console.log("Error capturing image:", error);
    }
  }, [camera.current, isImageView, enableFlash]);

  const sendFrameToServer = useCallback(
    async (_frame: Frame) => {
      if (!camera.current || !isActive) return;
      try {
        const snapshot = await camera.current.takePhoto();
        let fileUri = snapshot.path;
        if (!fileUri.startsWith("file://")) {
          fileUri = "file://" + fileUri;
        }

        const base64Image = await readAsStringAsync(fileUri, {
          encoding: EncodingType.Base64
        });

        const binaryString = atob(base64Image);
        const len = binaryString.length;
        const bytes = new Uint8Array(len);
        for (let i = 0; i < len; i++) {
          bytes[i] = binaryString.charCodeAt(i);
        }
        const arrayBuffer = bytes.buffer;
        if (wsRef.current && wsRef.current.readyState === WebSocket.OPEN) {
          wsRef.current.send(arrayBuffer);
        } else {
          console.log(wsRef.current);

          console.log("outside", wsRef.current?.readyState);

          console.log("Websocket is closed await new websocket");
        }

        RNFS.unlink(snapshot.path);
      } catch (error) {
        console.log("Error sending frame to server:", error);
      }
    },
    [camera.current, isActive, wsRef.current]
  );

  const sendFrameToServerJS = Worklets.createRunOnJS(sendFrameToServer);

  const frameProcessor = useFrameProcessor(
    frame => {
      "worklet";

      if (!isActive) return;

      runAtTargetFps(0.5, () => {
        "worklet";
        if (!isActive) return;

        sendFrameToServerJS(frame);
      });
    },
    [isActive]
  );

  const onFocusTap = useCallback(
    ({ nativeEvent: event }: GestureResponderEvent) => {
      if (!device?.supportsFocus) return;
      camera.current?.focus({
        x: event.locationX,
        y: event.locationY
      });
    },
    [device?.supportsFocus]
  );
  const toggleFlash = () => {
    setEnableFlash(!enableFlash);
  };
  const handleBack = () => {
    if (isImageView) {
      setIsImageView(false);
      setPrediction(undefined);
      return;
    }
    router.back();
  };

  const handleSearch = useCallback(() => {
    router.push({
      pathname: "/(protected)/search",
      params: {
        filter: ingredientPredict
      }
    });
  }, [ingredientPredict]);

  return (
    <SafeAreaView
      style={styles.container}
      className='bg-black'
    >
      <View
        style={{ height: "80%" }}
        className='relative w-full'
        onLayout={event => {
          const { height } = event.nativeEvent.layout;
          if (height !== cameraContainerHeight) {
            setCameraContainerHeight(height);
          }
        }}
      >
        <View style={{ height: "100%" }}>
          {shouldRenderCamera &&
            (isImageView ? (
              <View style={styles.camera}>
                <Image
                  source={{ uri: capturedImage }}
                  style={styles.image_view}
                ></Image>
              </View>
            ) : (
              <Camera
                // onTouchEnd={onFocusTap}
                style={styles.camera}
                isActive={isActive}
                device={device}
                frameProcessor={frameProcessor}
                pixelFormat='rgb'
                ref={camera}
                photo={true}
                preview={true}
                photoQualityBalance='speed'
                outputOrientation='preview'
              />
            ))}
        </View>
        <Canvas
          onTouchEnd={onFocusTap}
          style={{
            position: "absolute",
            top: 0,
            left: 0,
            right: 0,
            bottom: 0
          }}
        >
          {(prediction?.boxes ?? []).map((box, index) => {
            const x1 = box[0] * windowWidth;
            const y1 = box[1] * cameraContainerHeight;
            const x2 = box[2] * windowWidth;
            const y2 = box[3] * cameraContainerHeight;
            const boxWidth = x2 - x1;
            const boxHeight = y2 - y1;
            console.log(x1, y1, boxWidth, boxHeight);
            const line_width = 2;
            const outer = rrect(rect(x1, y1, boxWidth, boxHeight), 0, 0);
            const inner = rrect(
              rect(
                x1 + line_width,
                y1 + line_width,
                boxWidth - line_width * 2,
                boxHeight - line_width * 2
              ),
              0,
              0
            );
            return (
              <DiffRect
                key={index}
                outer={outer}
                inner={inner}
                color='red'
              />
            );
          })}
        </Canvas>
        <View className='absolute left-0 right-0 top-0 w-full flex-1 flex-row justify-between pl-4 pr-4 pt-8'>
          <View>
            <TouchableWithoutFeedback onPress={handleBack}>
              <View>
                <Ionicons
                  name='chevron-back'
                  size={28}
                  color='white'
                />
              </View>
            </TouchableWithoutFeedback>
          </View>
          <View>
            <TouchableWithoutFeedback onPress={toggleFlash}>
              <View>
                {enableFlash ? (
                  <Ionicons
                    name='flash-off'
                    size={24}
                    color='white'
                  />
                ) : (
                  <Ionicons
                    name='flash'
                    size={24}
                    color='white'
                  />
                )}
              </View>
            </TouchableWithoutFeedback>
          </View>
        </View>
        {!isImageView && (
          <Text
            style={styles.text}
            className='absolute bottom-0 left-0 right-0 text-primary'
          >
            {ingredientPredict}
          </Text>
        )}
      </View>
      <View className='w-full flex-1 flex-row justify-center'>
        {isImageView ? (
          <View className='flex-1'>
            <Text
              style={styles.text}
              className='mb-6 text-primary'
            >
              {ingredientPredict}
            </Text>
            <View className='w-[60%] self-center rounded-3xl bg-primary p-3'>
              <TouchableWithoutFeedback onPress={handleSearch}>
                <View className='flex-row items-center'>
                  <FontAwesome
                    name='search'
                    size={24}
                    color='black'
                    className='pr-4'
                  />
                  <Text className='font-bold text-2xl'>Search for recipes</Text>
                </View>
              </TouchableWithoutFeedback>
            </View>
          </View>
        ) : (
          <>
            <TouchableWithoutFeedback onPress={captureImage}>
              <View className='flex-1 items-center justify-center'>
                {/* <Image source={require("../../../assets/icons/gallery.png")} /> */}
              </View>
            </TouchableWithoutFeedback>
            <TouchableWithoutFeedback onPress={captureImage}>
              <View className='flex-1 items-center justify-end'>
                <Image source={require("../../../assets/icons/dot-circle-icon.png")} />
              </View>
            </TouchableWithoutFeedback>
            <TouchableWithoutFeedback onPress={flipCamera}>
              <View className='flex-1 items-center justify-center'>
                <Image source={require("../../../assets/icons/flip-camera.png")} />
              </View>
            </TouchableWithoutFeedback>
          </>
        )}
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "flex-start"
  },
  message: {
    textAlign: "center",
    paddingBottom: 10
  },
  camera: {
    flex: 1
  },
  button: {
    flex: 1,
    alignSelf: "flex-end",
    alignItems: "center"
  },
  image_view: {
    flex: 1
  },
  toggle_button: {
    borderColor: "black",
    borderWidth: 1,
    margin: 5,
    padding: 10,
    alignSelf: "center"
  },
  text: {
    fontSize: 24,
    fontWeight: "bold",
    textAlign: "center",
    margin: 10
  }
});

export default Capture;
