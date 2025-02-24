import { Button, GestureResponderEvent } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { useCallback, useEffect, useRef, useState } from "react";
import { useFocusEffect, useNavigation } from "expo-router";
import { useRouter } from "expo-router";
import { Alert, Image, StyleSheet, View, Text } from "react-native";
import {
  Camera,
  CameraDevice,
  CameraPermissionStatus,
  CameraPosition,
  getCameraDevice,
  useCameraDevices
} from "react-native-vision-camera";
import { useIngredientPredictMutation } from "@/api/ingredient-predict";
import { CameraView, PreviewView } from "@/components/screen/capture";
import { useTranslation } from "react-i18next";
import { TouchableWithoutFeedback } from "react-native-gesture-handler";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";

const Capture = () => {
  const { t } = useTranslation("capture");
  const [device, setDevice] = useState<CameraDevice | undefined>(undefined);
  const [deviceCode, setDeviceCode] = useState<CameraPosition>("back");
  const [isSearching, setIsSearching] = useState(false);
  const devices = useCameraDevices();
  const [photo, setPhoto] = useState({
    uri: "",
    width: 0,
    height: 0
  });
  const [permission, setPermission] = useState<CameraPermissionStatus>("not-determined");
  const [prediction, setPrediction] = useState<IngredientStreamResponse>();

  const { isLoading: isPredictLoading, mutateAsync: predictAsync } =
    useIngredientPredictMutation();
  const cameraRef = useRef<Camera>(null);

  const navigation = useNavigation();
  const router = useRouter();

  // ===== Inferred value =====
  const isScreenFocused = navigation.isFocused();
  const shouldRenderCamera = device && isScreenFocused;

  // ===== Styling value =====
  const { black, white } = colors;
  const { c } = useColorizer();

  const requestPermissionAsync = async () => {
    let finalPermissionStatus = Camera.getCameraPermissionStatus();
    if (finalPermissionStatus !== "granted") {
      finalPermissionStatus = await Camera.requestCameraPermission();
    }
    setPermission(finalPermissionStatus);

    if (finalPermissionStatus !== "granted") {
      Alert.alert(
        t("permissionDeniedAlert.title"),
        t("permissionDeniedAlert.description")
      );
    }
  };

  useEffect(() => {
    if (deviceCode) {
      setDevice(getCameraDevice(devices, deviceCode));
    }
  }, [deviceCode]);

  useFocusEffect(() => {
    if (isSearching) {
      setIsSearching(false);
    }
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

  const flipCamera = () => {
    if (deviceCode == "back") {
      setDeviceCode("front");
    } else {
      setDeviceCode("back");
    }
    setDevice(getCameraDevice(devices, deviceCode));
  };
  const onFocusTap = useCallback(
    ({ nativeEvent: event }: GestureResponderEvent) => {
      if (!device?.supportsFocus) return;
      cameraRef.current?.focus({
        x: event.locationX,
        y: event.locationY
      });
    },
    [device?.supportsFocus]
  );

  if (permission !== "granted") {
    return (
      <SafeAreaView
        style={{ backgroundColor: c(white.DEFAULT, black[100]) }}
        className='flex-1 justify-start'
      >
        <View className='w-full flex-1 items-center justify-center px-4'>
          <Text className='text-black_white mb-6 text-center text-2xl'>
            {t("grantPermission.title")}
          </Text>
          <TouchableWithoutFeedback onPress={requestPermissionAsync}>
            <View className='w-full rounded-full bg-primary pb-3 pl-4 pr-4 pt-3'>
              <Text className='text-white_black font-semibold text-lg'>
                {t("grantPermission.button")}
              </Text>
            </View>
          </TouchableWithoutFeedback>
        </View>
      </SafeAreaView>
    );
  }

  return (
    <SafeAreaView
      style={{ backgroundColor: black[100] }}
      className='flex-1 justify-start'
    >
      <View className='relative w-full'>
        <View style={{ height: "100%" }}>
          {shouldRenderCamera &&
            (photo.uri ? (
              <PreviewView
                photo={photo}
                setPhoto={setPhoto}
                isSearching={isSearching}
                setIsSearching={setIsSearching}
                prediction={prediction}
                setPrediction={setPrediction}
                isPredictLoading={isPredictLoading}
              />
            ) : (
              <CameraView
                cameraRef={cameraRef}
                device={device}
                photo={photo}
                setPhoto={setPhoto}
                flipCamera={flipCamera}
                predictAsync={predictAsync}
                setPrediction={setPrediction}
              />
            ))}
        </View>
      </View>
    </SafeAreaView>
  );
};

export default Capture;
