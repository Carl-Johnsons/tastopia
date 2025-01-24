import { ReactNode, useCallback, useEffect, useState } from "react";
import {
  Text,
  View,
  Image,
  StyleProp,
  ViewStyle,
  Pressable,
  GestureResponderEvent
} from "react-native";

import { transformPlatformURI } from "@/utils/functions";
import { AntDesign } from "@expo/vector-icons";
import { colors } from "@/constants/colors";
import { CameraPlusIcon, UploadIcon } from "@/constants/icons";
import { useImagePicking } from "@/hooks";
import Button from "@/components/Button";
import { AnimatedStyle } from "react-native-reanimated";
import { dismissKeyboard } from "@/utils/keyboard";
import { Gesture } from "react-native-gesture-handler/lib/typescript/handlers/gestures/gesture";

type ReplaceImageModeOptions = {
  /**
   * Image will only get replaced when pressed.
   *
   * @default false
   */
  enabled?: boolean;

  /**
   * Make the on press callback fire when the user clicks on anywhere on the replace image layer.
   *
   * @default false
   */
  globalCallback?: boolean;

  /**
   * Allow the user to replace the image by pressing on the image itself.
   *
   * @default false
   */
  replaceImageOnPress?: boolean;

  /**
   * The layer's class name.
   */
  className?: string;

  /**
   * The layer's style.
   */
  style?: StyleProp<AnimatedStyle<StyleProp<ViewStyle>>>;

  /**
   * The icon's size.
   */
  iconSize?: {
    width: number;
    height: number;
  };
};

type UploadImageProps = {
  /**
   * Array of image files to set as default images.
   */
  defaultImages?: ImageFileType[];

  /**
   * Function to handle file change (should be setState function).
   */
  onFileChange: (files: ImageFileType[]) => void;

  /**
   * Disable remove image.
   *
   * @default false
   */
  isDisabled?: boolean;

  /**
   * Allow upload multiple images.
   *
   * @default false
   */
  allowsMultipleSelection?: boolean;

  /**
   * Container's style.
   */
  containerStyle?: StyleProp<ViewStyle>;

  /**
   * Container's class name.
   */
  containerClassName?: string;

  /**
   * Image's wrapper style.
   */
  imageWrapperStyle?: StyleProp<ViewStyle>;

  /**
   * The images' class names.
   */
  innerImageClassName?: string;

  /**
   *  Maximum number of images uploadable at the same time.
   *
   *  @default 1
   */
  selectionLimit?: number;

  /**
   * Replace image mode configuration object.
   */
  replaceImageMode?: ReplaceImageModeOptions;

  /**
   * A function that renders the preview image element.
   */
  renderImage?: (imageFile: ImageFileObject, imageClassName?: string) => ReactNode;
};

const UploadImage = ({
  defaultImages,
  onFileChange,
  isDisabled,
  allowsMultipleSelection,
  containerStyle,
  containerClassName,
  imageWrapperStyle,
  innerImageClassName,
  selectionLimit = 1,
  replaceImageMode,
  renderImage
}: UploadImageProps) => {
  const [startUploadImage, setStartUploadImage] = useState(false);
  const [imageCount, setImageCount] = useState(0);
  const [fileObjects, setFileObjects] = useState<ImageFileObject[]>([]);
  const [selectedImageId, setSelectedImageId] = useState<string | null>(null);
  const { white } = colors;
  const { pickImage } = useImagePicking({ imageCount, allowsMultipleSelection });

  const isShowingUploadImageLayer =
    !replaceImageMode?.enabled &&
    ((!isDisabled && allowsMultipleSelection) ||
      (!startUploadImage && imageCount < selectionLimit));

  useEffect(() => {
    if (defaultImages && defaultImages.length > 0) {
      const initialFiles = defaultImages.map(image => ({
        id: image.uri,
        previewPath: image.uri,
        file: image
      }));

      setFileObjects(initialFiles);
      setImageCount(initialFiles.length);
    }
  }, [defaultImages]);

  const addImage = useCallback(async () => {
    const files = await pickImage();

    if (files) {
      const newFileObjects = [...(fileObjects || []), ...files];
      setStartUploadImage(true);
      setFileObjects(newFileObjects);
      onFileChange(newFileObjects.map(file => file.file as ImageFileType));
      setImageCount(prev => prev + files.length);
    }
  }, [pickImage, onFileChange]);

  const handleImagePress = (id: string) => {
    if (!isDisabled) {
      setSelectedImageId(selectedImageId === id ? null : id);
    }
  };

  const handleRemoveImage = (id: string) => {
    const newFileObjects = fileObjects?.filter(file => file.id !== id);
    setFileObjects(newFileObjects);
    onFileChange(newFileObjects!.map(file => file.file as ImageFileType));
    setImageCount(prev => prev - 1);
    setSelectedImageId(null);
  };

  const replaceImage = useCallback(async () => {
    const files = await pickImage();

    if (files) {
      const newFileObjects = [...files];
      setStartUploadImage(true);
      setFileObjects(newFileObjects);
      onFileChange(newFileObjects.map(file => file.file as ImageFileType));
      setImageCount(files.length);
    }
  }, [pickImage]);

  return (
    <View
      style={containerStyle}
      className={containerClassName}
    >
      <View className='flex-row flex-wrap gap-6'>
        {fileObjects?.map(fileObject => (
          <Pressable
            key={fileObject.id}
            style={imageWrapperStyle}
            onPress={() => handleImagePress(fileObject.id)}
          >
            <View className='relative'>
              <Pressable
                onPress={replaceImageMode?.replaceImageOnPress ? replaceImage : undefined}
              >
                {renderImage ? (
                  renderImage(fileObject, innerImageClassName)
                ) : (
                  <Image
                    source={{ uri: transformPlatformURI(fileObject.previewPath)! }}
                    className={`w-full ${innerImageClassName}`}
                  />
                )}
              </Pressable>

              {replaceImageMode?.enabled && (
                <ReplaceImageLayer
                  onButtonPress={replaceImage}
                  {...replaceImageMode}
                />
              )}
              {!replaceImageMode && selectedImageId === fileObject.id && (
                <View className='absolute h-full w-full items-center justify-center bg-black/50'>
                  <AntDesign
                    name='closecircleo'
                    size={24}
                    color={white.DEFAULT}
                    onPress={() => handleRemoveImage(fileObject.id)}
                  />
                </View>
              )}
            </View>
          </Pressable>
        ))}
      </View>

      {isShowingUploadImageLayer && <UploadImageLayer onButtonPress={addImage} />}
    </View>
  );
};

const ReplaceImageLayer = ({
  onButtonPress,
  globalCallback,
  className,
  iconSize,
  style
}: Pick<
  ReplaceImageModeOptions,
  "globalCallback" | "className" | "style" | "iconSize"
> & {
  onButtonPress: () => void;
}) => {
  const { white } = colors;

  return (
    <Button
      className={`absolute h-full w-full items-center justify-center bg-black/50 ${className}`}
      onPress={globalCallback ? onButtonPress : undefined}
      style={style}
    >
      <View className='flex items-center justify-center rounded-2xl'>
        <CameraPlusIcon
          width={iconSize ? iconSize.width : 28}
          height={iconSize ? iconSize.height : 28}
          color={white.DEFAULT}
          onPress={onButtonPress}
        />
      </View>
    </Button>
  );
};

const UploadImageLayer = ({ onButtonPress }: { onButtonPress: () => void }) => {
  const { black } = colors;

  return (
    <View className='h-full w-full items-center justify-center'>
      <View className='flex items-center justify-center rounded-2xl bg-gray-100 px-10 pb-3'>
        <UploadIcon
          width={62}
          height={51}
          color={black.DEFAULT}
          onPress={onButtonPress}
        />
        <Text className='font-semibold text-black'>Upload</Text>
      </View>
    </View>
  );
};

export default UploadImage;
