import "react-native-reanimated";
import { Canvas, rect, DiffRect, rrect } from "@shopify/react-native-skia";
import { useCallback, useEffect, useRef, useState } from "react";
import { useRouter } from "expo-router";
import { Worklets } from "react-native-worklets-core";
import {
  Alert,
  GestureResponderEvent,
  Image,
  StyleSheet,
  Text,
  TouchableOpacity,
  View
} from "react-native";
import {
  Camera,
  CameraDevice,
  CameraPosition,
  Frame,
  getCameraDevice,
  runAtTargetFps,
  useFrameProcessor
} from "react-native-vision-camera";
import {
  useIngredientPredictBoxMutation,
  useIngredientPredictMutation
} from "@/api/ingredient-predict";

var RNFS = require("react-native-fs");

const Capture = () => {
  const router = useRouter();
  const [isActive, setIsActive] = useState(true);
  const [deviceCode, setDeviceCode] = useState<CameraPosition>("back");
  const devices = Camera.getAvailableCameraDevices();
  const [device, setDevice] = useState<CameraDevice | undefined>(undefined);
  const camera = useRef<Camera>(null);
  const [prediction, setPrediction] = useState("");
  const [isImageView, setIsImageView] = useState(false);
  const [capturedImage, setCapturedImage] = useState("");
  const { mutateAsync: predictAsync } = useIngredientPredictMutation();
  const { mutateAsync: predictBoxAsync } = useIngredientPredictBoxMutation();
  const [boxes, setBoxes] = useState<number[][]>([]);

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

  const captureImage = async () => {
    if (isImageView) {
      setIsImageView(false);
      setPrediction("");
      setBoxes([]);
      return;
    }
    setPrediction("");
    setBoxes([]);

    try {
      if (!camera) return;
      // const snapshot = await camera.current!.takeSnapshot({
      //   quality: 90,
      // });

      const snapshot = await camera.current!.takePhoto();

      setIsImageView(true);

      // const image = await fetch(`file://${snapshot.path}`);
      // const blob = await image.blob();

      const file = {
        uri: `file://${snapshot.path}`,
        type: "image/jpeg",
        name: "file"
      };
      const predictResponse = await predictAsync({ file: file as unknown as Blob });
      setPrediction(predictResponse.classifications[0].name);
      setCapturedImage(`file://${snapshot.path}`);

      const predictBoxResponse = await predictBoxAsync({ file: file as unknown as Blob });
      if (isImageView) setBoxes(predictBoxResponse.boxes);
      RNFS.unlink(snapshot.path);
    } catch (error) {
      console.error("Error capturing image:", error);
    }
  };

  async function sendFrameToServer(frame: Frame) {
    try {
      if (!camera) return;
      // const file = await camera.current!.takePhoto();

      const snapshot = await camera.current!.takeSnapshot({
        quality: 90
      });

      let formData = new FormData();
      const file = {
        uri: `file://${snapshot.path}`,
        type: "image/jpeg",
        name: "file"
      };
      formData.append("file", file as unknown as Blob);

      // let formData = new FormData();
      // const response = await fetch(`file://${snapshot.path}`);
      // const blob = await response.blob();
      // formData.append("file", blob);
      // console.log(formData);
      const predictResponse = await predictAsync({ file: file as unknown as Blob });

      setPrediction(predictResponse.classifications[0].name);

      RNFS.unlink(snapshot.path);
    } catch (error) {
      console.error("Error sending frame to server:", error);
    }
  }

  const sendFrameToServerJS = Worklets.createRunOnJS(sendFrameToServer);

  const frameProcessor = useFrameProcessor(frame => {
    "worklet";

    runAtTargetFps(1, () => {
      "worklet";

      sendFrameToServerJS(frame);
    });
  }, []);

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

  return (
    <View style={styles.container}>
      <View style={{ position: "relative", height: 500 }}>
        <View style={{ height: "100%" }}>
          {isActive &&
            device &&
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
                enableFpsGraph={true}
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
          {boxes.map((box, index) => {
            const x1 = (1 - box[1]) * 300;
            const y1 = (1 - box[0]) * 300;
            const x2 = (1 - box[3]) * 300;
            const y2 = (1 - box[2]) * 300;
            // console.log(box);
            const line_width = 2;
            const outer = rrect(rect(x1, y1, x2, y2), 0, 0);
            const inner = rrect(
              rect(
                x1 + line_width,
                y1 + line_width,
                x2 - line_width * 2,
                y2 - line_width * 2
              ),
              0,
              0
            );
            return (
              <DiffRect
                key={index}
                outer={outer}
                inner={inner}
                color='lightblue'
              />
            );
          })}
        </Canvas>
      </View>
      <Text style={styles.text}>{prediction}</Text>
      <View style={styles.buttonContainer}>
        <TouchableOpacity
          onPress={() => {
            setIsActive(!isActive);
            // console.log(device);
            // console.log(deviceCode);
          }}
        >
          <Text style={styles.toggle_button}>Toggle Camera</Text>
        </TouchableOpacity>
        <TouchableOpacity>
          <Text
            style={styles.toggle_button}
            onPress={flipCamera}
          >
            Flip Camera
          </Text>
        </TouchableOpacity>
        <TouchableOpacity>
          <Text
            style={styles.toggle_button}
            onPress={captureImage}
          >
            Take photo
          </Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center"
  },
  message: {
    textAlign: "center",
    paddingBottom: 10
  },
  camera: {
    // marginTop: 10,
    flex: 1
  },
  buttonContainer: {
    // flex: 1,
    flexDirection: "row",
    backgroundColor: "transparent",
    bottom: 0
  },
  button: {
    flex: 1,
    alignSelf: "flex-end",
    alignItems: "center"
  },
  image_view: {
    // height: 200,
    // width: 200,
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
