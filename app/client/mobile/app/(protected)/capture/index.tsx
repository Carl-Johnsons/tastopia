import { ActivityIndicator, TouchableWithoutFeedback } from "react-native";
import { addTagValue } from "@/slices/searchRecipe.slice";
import { Canvas, rect, DiffRect, rrect } from "@shopify/react-native-skia";
import { FontAwesome } from "@expo/vector-icons";
import { globalStyles } from "@/components/common/GlobalStyles";
import { SafeAreaView } from "react-native-safe-area-context";
import { useAppDispatch } from "@/store/hooks";
import { useCallback, useEffect, useRef, useState } from "react";
import { useIngredientPredictMutation } from "@/api/ingredient-predict";
import { useFocusEffect, useNavigation } from "expo-router";
import { useRouter } from "expo-router";
import Ionicons from "@expo/vector-icons/Ionicons";
import uuid from "react-native-uuid";
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
  useCameraDevices
} from "react-native-vision-camera";
import { useBounce, useImagePicking } from "@/hooks";
import Button from "@/components/Button";
import { colors } from "@/constants/colors";

const RNFS = require("react-native-fs");
// the value returned does not include the bottom navigation bar, I am not sure why yours does.

const Capture = () => {
  const [device, setDevice] = useState<CameraDevice | undefined>(undefined);
  const [deviceCode, setDeviceCode] = useState<CameraPosition>("back");
  const [enableFlash, setEnableFlash] = useState(false);
  const [isTakingPhoto, setIsTakingPhoto] = useState(false);
  const [isSearching, setIsSearching] = useState(false);
  const [photo, setPhoto] = useState({
    uri: "",
    width: 0,
    height: 0
  });
  const [prediction, setPrediction] = useState<IngredientStreamResponse>();
  const [imageContainerDimension, setImageContainerDimension] = useState({
    width: 0,
    height: 0
  });
  const { isLoading: isPredictLoading, mutateAsync: predictAsync } =
    useIngredientPredictMutation();
  const { pickImage } = useImagePicking({
    imageCount: 1,
    allowsMultipleSelection: false
  });
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

  // ===== Styling value =====
  const { black } = colors;
  const { animate: animateCaptureBtn, animatedStyles: animatedCaptureBtnStyles } =
    useBounce();
  const { animate: animatedSearchBtn, animatedStyles: animatedSearchBtnStyles } =
    useBounce();

  const requestPermissionAsync = async () => {
    let finalPermissionStatus = Camera.getCameraPermissionStatus();
    if (finalPermissionStatus != "granted") {
      finalPermissionStatus = await Camera.requestCameraPermission();
    }

    if (finalPermissionStatus != "granted") {
      Alert.alert(
        "Permission denied",
        "In order to use the camera you must allowed camera permission!"
      );
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
    if (isTakingPhoto) return;
    if (!camera.current) return;

    animateCaptureBtn();

    try {
      setIsTakingPhoto(true);
      const photo = await camera.current.takePhoto({
        flash: enableFlash ? "on" : "off"
      });
      setIsTakingPhoto(false);
      setPhoto({
        ...photo,
        uri: `file://${photo.path}`
      });

      console.log({ photo });

      const file = {
        uri: `file://${photo.path}`,
        type: "image/jpeg",
        name: "file"
      };

      const predictResponse = await predictAsync({ file: file as unknown as Blob });
      console.log(predictResponse.boxes);

      setPrediction(predictResponse);
    } catch (error) {
      console.log("Error capturing image:", error);
    }
  }, [camera.current, enableFlash]);

  const openGallery = async () => {
    const images = await pickImage();
    const image = images?.[0];

    if (!image) {
      // Alert.alert("Error", "Image not found! Please try again!");
      return;
    }

    setPhoto({
      ...photo,
      uri: image.previewPath
    });
    const predictResponse = await predictAsync({ file: image.file as unknown as Blob });
    setPrediction(predictResponse);
  };

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
    RNFS.unlink(photo.uri);
    setPrediction(undefined);
    setPhoto({
      uri: "",
      width: 0,
      height: 0
    });
  };

  const handleBack = () => {
    router.back();
  };

  const handleSearch = useCallback(() => {
    if (isSearching) return;
    animatedSearchBtn();

    setIsSearching(true);
    dispatch(addTagValue({ code: uuid.v4(), value: ingredientPredict }));

    router.push({
      pathname: "/(protected)/search"
    });
  }, [ingredientPredict, isSearching]);

  useFocusEffect(() => {
    if (isSearching) {
      setIsSearching(false);
    };
  });

  useEffect(() => {
    if (photo.uri) {
      Image.getSize(
        photo.uri,
        (width, height) => setPhoto({ ...photo, width, height }),
        error => console.error("Failed to get image dimensions:", error)
      );
    }
  }, [photo.uri]);

  return (
    <SafeAreaView
      style={styles.container}
      className='bg-black'
    >
      <View className='relative w-full'>
        <View style={{ height: "100%" }}>
          {shouldRenderCamera &&
            (photo.uri ? (
              <>
                <View className='w-full flex-1'>
                  <View
                    className='h-[80%] w-full'
                    onLayout={event => {
                      const { width, height } = event.nativeEvent.layout;
                      setImageContainerDimension({
                        width,
                        height
                      });
                    }}
                  >
                    <Image
                      source={{ uri: photo.uri }}
                      style={{ flex: 1, resizeMode: "contain" }}
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
                      const scaleFactor = Math.min(
                        imageContainerDimension.width / photo.width,
                        imageContainerDimension.height / photo.height
                      );

                      const displayWidth = photo.width * scaleFactor;
                      const displayHeight = photo.height * scaleFactor;
                      const offsetY =
                        (imageContainerDimension.height - displayHeight) / 2;

                      const x1 = box[0] * displayWidth;
                      const y1 = offsetY + box[1] * displayHeight;
                      const x2 = box[2] * displayWidth;
                      const y2 = offsetY + box[3] * displayHeight;
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
                        <Button
                          onPress={handleSearch}
                          style={animatedSearchBtnStyles}
                          className='w-[60%] rounded-3xl bg-primary p-3'
                        >
                          <View className='flex-row items-center'>
                            <FontAwesome
                              name='search'
                              size={24}
                              color='black'
                              className='pr-4'
                            />
                            <Text className='font-bold text-2xl'>Search for recipes</Text>
                          </View>
                        </Button>
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
                <>
                  {!isTakingPhoto && (
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
                  )}
                  <View className='absolute bottom-0 w-full flex-1 flex-row'>
                    {!isTakingPhoto ? (
                      <TouchableWithoutFeedback onPress={openGallery}>
                        <View className='flex-1 items-center justify-center'>
                          <Image source={require("../../../assets/icons/gallery.png")} />
                        </View>
                      </TouchableWithoutFeedback>
                    ) : (
                      <View className='flex-1' />
                    )}
                    <Button
                      onPress={captureImage}
                      style={animatedCaptureBtnStyles}
                    >
                      <View className='flex-1 items-center justify-center'>
                        <Image
                          source={require("../../../assets/icons/dot-circle-icon.png")}
                        />
                      </View>
                    </Button>
                    {!isTakingPhoto ? (
                      <TouchableWithoutFeedback onPress={flipCamera}>
                        <View className='flex-1 items-center justify-center'>
                          <Image
                            source={require("../../../assets/icons/flip-camera.png")}
                          />
                        </View>
                      </TouchableWithoutFeedback>
                    ) : (
                      <View className='flex-1' />
                    )}
                  </View>
                </>
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
