import { Camera, CameraDevice } from "react-native-vision-camera";
import { Dispatch, RefObject, SetStateAction, useCallback, useState } from "react";
import { Ionicons } from "@expo/vector-icons";
import { useBounce, useImagePicking } from "@/hooks";
import { UseMutateAsyncFunction } from "react-query";
import { useNavigation, useRouter } from "expo-router";
import { View, Image, TouchableWithoutFeedback } from "react-native";
import Button from "@/components/Button";
import { useErrorHandler } from "@/hooks/useErrorHandler";

type Props = {
  device: CameraDevice;
  cameraRef: RefObject<Camera>;
  flipCamera: () => void;
  photo: {
    uri: string;
    width: number;
    height: number;
  };
  setPhoto: Dispatch<
    SetStateAction<{
      uri: string;
      width: number;
      height: number;
    }>
  >;
  setPrediction: Dispatch<SetStateAction<IngredientStreamResponse | undefined>>;
  predictAsync: UseMutateAsyncFunction<
    IngredientStreamResponse,
    unknown,
    { file?: Blob },
    unknown
  >;
};

const CameraView = ({
  device,
  cameraRef,
  flipCamera = () => {},
  photo,
  setPhoto,
  setPrediction,
  predictAsync
}: Props) => {
  const [enableFlash, setEnableFlash] = useState(false);
  const [isTakingPhoto, setIsTakingPhoto] = useState(false);

  const navigation = useNavigation();
  const router = useRouter();

  const { handleError } = useErrorHandler();
  const { pickImage } = useImagePicking({
    imageCount: 1,
    allowsMultipleSelection: false
  });
  // ==== Style ====
  const { animate: animateCaptureBtn, animatedStyles: animatedCaptureBtnStyles } =
    useBounce();

  // ===== Inferred value =====
  const isScreenFocused = navigation.isFocused();
  const isActive = isScreenFocused;

  const openGallery = async () => {
    const images = await pickImage();
    const image = images?.[0];

    if (!image) {
      return;
    }

    setPhoto({
      ...photo,
      uri: image.previewPath
    });
    const predictResponse = await predictAsync(
      { file: image.file as unknown as Blob },
      {
        onError: error => handleError(error)
      }
    );
    setPrediction(predictResponse);
  };

  const captureImage = useCallback(async () => {
    if (isTakingPhoto) return;
    if (!cameraRef.current) return;

    animateCaptureBtn();

    try {
      setIsTakingPhoto(true);
      const photo = await cameraRef.current.takePhoto({
        flash: enableFlash ? "on" : "off"
      });
      setIsTakingPhoto(false);
      setPhoto({
        ...photo,
        uri: `file://${photo.path}`
      });

      const file = {
        uri: `file://${photo.path}`,
        type: "image/jpeg",
        name: "file"
      };

      const predictResponse = await predictAsync(
        { file: file as unknown as Blob },
        {
          onError: error => handleError(error)
        }
      );
      setPrediction(predictResponse);
    } catch (error) {
      console.log("Error capturing image:", error);
    }
  }, [cameraRef.current, enableFlash]);

  const toggleFlash = () => {
    setEnableFlash(!enableFlash);
  };

  const handleBack = () => {
    router.back();
  };

  return (
    <>
      <Camera
        // onTouchEnd={onFocusTap}
        style={{
          flex: 1
        }}
        isActive={isActive}
        device={device}
        pixelFormat='rgb'
        ref={cameraRef}
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
                    name='chevron-back-outline'
                    size={28}
                    color='white'
                    style={{
                      textShadowColor: "rgba(0, 0, 0, 0.75)",
                      textShadowOffset: { width: 1, height: 3 },
                      textShadowRadius: 1
                    }}
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
                      style={{
                        textShadowColor: "rgba(0, 0, 0, 0.75)",
                        textShadowOffset: { width: 1, height: 3 },
                        textShadowRadius: 1
                      }}
                    />
                  ) : (
                    <Ionicons
                      name='flash'
                      size={24}
                      color='white'
                      style={{
                        textShadowColor: "rgba(0, 0, 0, 0.75)",
                        textShadowOffset: { width: 1, height: 3 },
                        textShadowRadius: 1
                      }}
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
              <Image source={require("../../../assets/icons/dot-circle-icon.png")} />
            </View>
          </Button>
          {!isTakingPhoto ? (
            <TouchableWithoutFeedback onPress={flipCamera}>
              <View className='flex-1 items-center justify-center'>
                <Image source={require("../../../assets/icons/flip-camera.png")} />
              </View>
            </TouchableWithoutFeedback>
          ) : (
            <View className='flex-1' />
          )}
        </View>
      </>
    </>
  );
};

export { CameraView };
