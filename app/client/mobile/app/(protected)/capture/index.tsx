import { Canvas, rect, DiffRect, rrect } from "@shopify/react-native-skia";
import { ActivityIndicator, Dimensions, TouchableWithoutFeedback } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { useCallback, useEffect, useRef, useState } from "react";
import { useIngredientPredictMutation } from "@/api/ingredient-predict";
import { useNavigation } from "expo-router";
import { useRouter } from "expo-router";
import uuid from "react-native-uuid";
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
  getCameraDevice,
  PhotoFile,
  useCameraDevices
} from "react-native-vision-camera";
import { FontAwesome } from "@expo/vector-icons";
import { useAppDispatch } from "@/store/hooks";
import { addTagValue } from "@/slices/searchRecipe.slice";
import { globalStyles } from "@/components/common/GlobalStyles";

const RNFS = require("react-native-fs");
const { width: windowWidth } = Dimensions.get("window");
// the value returned does not include the bottom navigation bar, I am not sure why yours does.

const Capture = () => {
  const [device, setDevice] = useState<CameraDevice | undefined>(undefined);
  const [deviceCode, setDeviceCode] = useState<CameraPosition>("back");
  const [enableFlash, setEnableFlash] = useState(false);
  const [isTakingPhoto, setIsTakingPhoto] = useState(false);
  const [photo, setPhoto] = useState<PhotoFile | undefined>(undefined);
  const [prediction, setPrediction] = useState<IngredientStreamResponse>();
  const [previewContainerHeight, setPreviewContainerHeight] = useState(0);
  const { isLoading: isPredictLoading, mutateAsync: predictAsync } =
    useIngredientPredictMutation();
  const camera = useRef<Camera>(null);
  const devices = useCameraDevices();
  const dispatch = useAppDispatch();
  const navigation = useNavigation();
  const router = useRouter();

  // ===== Inferred value =====
  const ingredientPredict = prediction?.classifications?.[0].name ?? "";
  const isScreenFocused = navigation.isFocused();
  const isActive = isScreenFocused;
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

  useEffect(() => {
    if (deviceCode) {
      setDevice(getCameraDevice(devices, deviceCode));
    }
  }, [deviceCode]);

  const captureImage = useCallback(async () => {
    try {
      if (!camera.current) return;
      setIsTakingPhoto(true);
      const photo = await camera.current.takePhoto({
        flash: enableFlash ? "on" : "off"
      });
      setIsTakingPhoto(false);
      setPhoto(photo);

      const file = {
        uri: `file://${photo.path}`,
        type: "image/jpeg",
        name: "file"
      };

      const predictResponse = await predictAsync({ file: file as unknown as Blob });
      console.log(predictResponse.boxes);

      setPrediction(predictResponse);

      // RNFS.unlink(snapshot.path);
    } catch (error) {
      console.log("Error capturing image:", error);
    }
  }, [camera.current, enableFlash]);

  const toggleGallery = () => {};

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

  const handleBackInPreview = () => {
    setPrediction(undefined);
    setPhoto(undefined);
  };

  const handleBack = () => {
    router.back();
  };

  const handleSearch = useCallback(() => {
    dispatch(addTagValue({ code: uuid.v4(), value: ingredientPredict }));

    router.push({
      pathname: "/(protected)/search"
    });
  }, [ingredientPredict]);

  return (
    <SafeAreaView
      style={styles.container}
      className='bg-black'
    >
      <View className='relative w-full'>
        <View style={{ height: "100%" }}>
          {shouldRenderCamera &&
            (photo ? (
              <>
                <View className='w-full flex-1'>
                  <View
                    className='h-[80%] w-full'
                    onLayout={event => {
                      const { height } = event.nativeEvent.layout;
                      if (height !== previewContainerHeight) {
                        setPreviewContainerHeight(height);
                      }
                    }}
                  >
                    <Image
                      source={{ uri: `file://${photo.path}` }}
                      className='flex-1'
                    />
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
                      const y1 = box[1] * previewContainerHeight;
                      const x2 = box[2] * windowWidth;
                      const y2 = box[3] * previewContainerHeight;
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
                  <View className='flex-1 items-center justify-center gap-4'>
                    {isPredictLoading ? (
                      <>
                        <ActivityIndicator
                          size='large'
                          color={globalStyles.color.primary}
                        />
                      </>
                    ) : (
                      <>
                        <Text className='font-bold text-3xl text-primary'>
                          {ingredientPredict}
                        </Text>
                        <View className='w-[60%] rounded-3xl bg-primary p-3'>
                          <TouchableWithoutFeedback onPress={handleSearch}>
                            <View className='flex-row items-center'>
                              <FontAwesome
                                name='search'
                                size={24}
                                color='black'
                                className='pr-4'
                              />
                              <Text className='font-bold text-2xl'>
                                Search for recipes
                              </Text>
                            </View>
                          </TouchableWithoutFeedback>
                        </View>
                      </>
                    )}
                  </View>
                </View>
                <View className='absolute left-0 right-0 top-0 w-full flex-1 flex-row justify-between pl-4 pr-4 pt-8'>
                  <View>
                    <TouchableWithoutFeedback onPress={handleBackInPreview}>
                      <View>
                        <Ionicons
                          name='chevron-back'
                          size={28}
                          color='white'
                        />
                      </View>
                    </TouchableWithoutFeedback>
                  </View>
                </View>
              </>
            ) : (
              <>
                <Camera
                  // onTouchEnd={onFocusTap}
                  style={{
                    flex: 1
                  }}
                  isActive={isActive}
                  device={device}
                  pixelFormat='rgb'
                  ref={camera}
                  photo={true}
                  preview={true}
                  photoQualityBalance='speed'
                  outputOrientation='preview'
                />
                {isTakingPhoto ? (
                  <View className='absolute bottom-0 left-0 right-0 mb-6'>
                    <ActivityIndicator
                      size='large'
                      color={globalStyles.color.primary}
                    />
                  </View>
                ) : (
                  <>
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
                    <View className='absolute bottom-0 w-full flex-1 flex-row justify-center'>
                      <TouchableWithoutFeedback onPress={toggleGallery}>
                        <View className='flex-1 items-center justify-center'>
                          <Image source={require("../../../assets/icons/gallery.png")} />
                        </View>
                      </TouchableWithoutFeedback>
                      <TouchableWithoutFeedback onPress={captureImage}>
                        <View className='flex-1 items-center justify-center'>
                          <Image
                            source={require("../../../assets/icons/dot-circle-icon.png")}
                          />
                        </View>
                      </TouchableWithoutFeedback>
                      <TouchableWithoutFeedback onPress={flipCamera}>
                        <View className='flex-1 items-center justify-center'>
                          <Image
                            source={require("../../../assets/icons/flip-camera.png")}
                          />
                        </View>
                      </TouchableWithoutFeedback>
                    </View>
                  </>
                )}
              </>
            ))}
        </View>
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
  button: {
    flex: 1,
    alignSelf: "flex-end",
    alignItems: "center"
  }
});

export default Capture;
