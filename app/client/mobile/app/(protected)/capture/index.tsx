import { GestureResponderEvent } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { useCallback, useEffect, useRef, useState } from "react";
import { useFocusEffect, useNavigation } from "expo-router";
import { useRouter } from "expo-router";
import { Alert, Image, StyleSheet, View } from "react-native";
import {
  Camera,
  CameraDevice,
  CameraPosition,
  getCameraDevice,
  useCameraDevices
} from "react-native-vision-camera";
import { useIngredientPredictMutation } from "@/api/ingredient-predict";
import { CameraView, PreviewView } from "@/components/screen/capture";

const Capture = () => {
  const [device, setDevice] = useState<CameraDevice | undefined>(undefined);
  const [deviceCode, setDeviceCode] = useState<CameraPosition>("back");
  const [isSearching, setIsSearching] = useState(false);
  const devices = useCameraDevices();
  const [photo, setPhoto] = useState({
    uri: "",
    width: 0,
    height: 0
  });
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

  return (
    <SafeAreaView
      style={styles.container}
      className='bg-black'
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
