import { useEffect, useRef, useState } from "react";
import * as ImagePicker from "expo-image-picker";
import FontAwesome from "@expo/vector-icons/FontAwesome";
import {
  Text,
  View,
  Image,
  TouchableHighlight,
  StyleProp,
  ViewStyle,
  Alert
} from "react-native";

import { globalStyles } from "../GlobalStyles";
import { getUniqueItemsWithSet, transformPlatformURI } from "@/utils/functions";
import styles from "./UpdateImage.style";
import { extensionToMimeType } from "@/utils/file";
import { AntDesign } from "@expo/vector-icons";
import { TouchableWithoutFeedback } from "react-native-gesture-handler";
import { useTranslation } from "react-i18next";
import useDarkMode from "@/hooks/useDarkMode";
import OutsidePressHandler from "react-native-outside-press";

type UpdateImageProps = {
  /**
   * Array of image files to set as default images
   */
  images?: UpdateImage;

  /**
   * Function to handle delete image
   */
  onDeleteImage: (deleteImageUrl: string) => void;

  /**
   * Function to handle add image
   */
  onAddImage: (files: ImageFileType[]) => void;

  /**
   * Disable remove image
   */
  isDisabled?: boolean;

  /**
   * Allow upload multiple images
   */
  isMultiple?: boolean;

  /**
   * Container style
   */
  containerStyle?: StyleProp<ViewStyle>;

  /**
   * Image's wrapper style
   */
  imageStyle?: StyleProp<ViewStyle>;

  /**
   * The images' class names
   */
  innerImageClassName?: string;

  /**
   *  Maximum number images
   */
  selectionLimit?: number;

  /**
   * ImagePicker options
   */
  props?: ImagePicker.ImagePickerOptions;
};

const UpdateImage = ({
  images,
  onAddImage,
  onDeleteImage,
  isDisabled = false,
  isMultiple = true,
  containerStyle,
  imageStyle,
  innerImageClassName,
  selectionLimit = 5,
  props
}: UpdateImageProps) => {
  const isDarkMode = useDarkMode();
  const { t } = useTranslation("component");
  const [startUploadImage, setStartUploadImage] = useState(false);
  const [imageCount, setImageCount] = useState(0);
  const [defaultImages, setDefaultImages] = useState<string[]>();
  const [fileObjects, setFileObjects] = useState<ImageFileObject[]>([]); // for add new images
  const [status, requestPermission] = ImagePicker.useMediaLibraryPermissions();
  const [selectedImageId, setSelectedImageId] = useState<string | null>(null);
  const selectedImageIdRef = useRef<string | null>(null);

  useEffect(() => {
    (async () => {
      if (!status?.granted) {
        await requestPermission();
      }
    })();
  }, [status, requestPermission]);

  const pickImage = async () => {
    if (!status?.granted) {
      const { granted } = await requestPermission();
      if (!granted) {
        Alert.alert(
          "Permission Denied",
          "You need to grant permission to access the media library to upload images."
        );
        return;
      }
    }

    let result = await ImagePicker.launchImageLibraryAsync({
      quality: 1,
      selectionLimit: isMultiple ? selectionLimit - imageCount : 1,
      allowsMultipleSelection: isMultiple,
      mediaTypes: ["images"],
      ...props
    });

    if (!result.canceled && result.assets) {
      const files = result.assets.map(asset => {
        const fileName =
          asset.fileName ||
          asset.uri.substring(asset.uri.lastIndexOf("/") + 1, asset.uri.length);

        const type = asset.mimeType || extensionToMimeType(asset.uri.split(".").pop()!);

        return {
          id: asset.uri,
          previewPath: asset.uri,
          file: {
            uri: asset.uri,
            type,
            name: fileName
          }
        };
      });

      const newFileObjects = [...(fileObjects || []), ...files];
      setStartUploadImage(true);
      setFileObjects(newFileObjects);
      onAddImage(newFileObjects.map(file => file.file as ImageFileType));
      setImageCount(prev => prev + files.length);
    }
  };

  const handleImagePress = (id: string) => {
    if (!isDisabled) {
      if (selectedImageId === id) {
        handleRemoveImage(id);
      } else {
        setSelectedImageId(id);
        selectedImageIdRef.current = id;
      }
    }
  };

  const handleRemoveImage = (id: string) => {
    const newFileObjects = fileObjects?.filter(file => file.id !== id);
    setFileObjects(newFileObjects);
    setDefaultImages(prev => prev?.filter(imageUrl => imageUrl !== id));
    onDeleteImage(id);
    setImageCount(prev => prev - 1);
    setSelectedImageId(null);
    selectedImageIdRef.current === null;
  };

  useEffect(() => {
    const imagesFiltered = getUniqueItemsWithSet(
      images?.defaultImages!,
      images?.deleteUrls!
    );

    setImageCount(imagesFiltered.length + images?.additionalImages?.length!);
    setDefaultImages(imagesFiltered);
  }, [images?.defaultImages?.length]);

  useEffect(() => {
    const handledImages = images?.additionalImages?.map(image => {
      return {
        id: image.uri,
        previewPath: image.uri,
        file: image
      } as ImageFileObject;
    });

    setFileObjects(handledImages ?? []);
  }, [images?.additionalImages?.length]);

  return (
    <View style={[styles.container, imageCount === 1 && styles.flexOne, containerStyle]}>
      <View style={[styles.wrapper, imageCount === 1 && styles.sizeFull]}>
        {/* Preview image */}
        <View style={styles.previewImage}>
          {defaultImages?.map(imageUrl => (
            <TouchableHighlight
              key={imageUrl}
              style={[styles.uploadItem, imageCount === 1 && styles.flexOne, imageStyle]}
              onPress={() => handleImagePress(imageUrl)}
              underlayColor='transparent'
            >
              <OutsidePressHandler
                disabled={selectedImageIdRef.current !== imageUrl}
                onOutsidePress={() => {
                  setSelectedImageId(null);
                }}
              >
                <View>
                  <Image
                    source={{ uri: imageUrl }}
                    style={styles.uploadItemImage}
                    className={`aspect-[1.6] ${innerImageClassName}`}
                  />
                  {selectedImageId === imageUrl && (
                    <TouchableHighlight
                      style={styles.removeOverlay}
                      onPress={() => handleRemoveImage(imageUrl)}
                      underlayColor='transparent'
                    >
                      <View style={styles.removeIconWrapper}>
                        <AntDesign
                          name='closecircleo'
                          size={24}
                          color={globalStyles.color.light}
                        />
                      </View>
                    </TouchableHighlight>
                  )}
                </View>
              </OutsidePressHandler>
            </TouchableHighlight>
          ))}

          {fileObjects?.map(fileObject => (
            <TouchableHighlight
              key={fileObject.id}
              style={[styles.uploadItem, imageCount === 1 && styles.flexOne, imageStyle]}
              onPress={() => handleImagePress(fileObject.id)}
              underlayColor='transparent'
            >
              <OutsidePressHandler
                disabled={selectedImageIdRef.current !== fileObject.id}
                onOutsidePress={() => {
                  setSelectedImageId(null);
                }}
              >
                <View>
                  <Image
                    source={{ uri: transformPlatformURI(fileObject.previewPath)! }}
                    style={styles.uploadItemImage}
                    className={`aspect-[1.6] ${innerImageClassName}`}
                  />
                  {selectedImageId === fileObject.id && (
                    <TouchableHighlight
                      style={styles.removeOverlay}
                      onPress={() => handleRemoveImage(fileObject.id)}
                      underlayColor='transparent'
                    >
                      <View style={styles.removeIconWrapper}>
                        <AntDesign
                          name='closecircleo'
                          size={24}
                          color={globalStyles.color.light}
                        />
                      </View>
                    </TouchableHighlight>
                  )}
                </View>
              </OutsidePressHandler>
            </TouchableHighlight>
          ))}
        </View>

        {/* Upload image button */}
        {((imageCount <= 4 && !isDisabled && isMultiple) ||
          (imageCount === 0 && !startUploadImage)) &&
          imageCount < selectionLimit && (
            <TouchableHighlight
              onPress={pickImage}
              style={styles.uploadButton}
              underlayColor={globalStyles.color.secondary}
            >
              <View style={styles.uploadButtonWrapper}>
                <FontAwesome
                  name='cloud-upload'
                  size={24}
                  color={isDarkMode ? globalStyles.color.dark : globalStyles.color.light}
                />
                <Text className='text-white_black body-semibold'>{t("uploadImage")}</Text>
              </View>
            </TouchableHighlight>
          )}
      </View>
    </View>
  );
};

export default UpdateImage;
