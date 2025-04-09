import React, { memo, useEffect, useRef, useState } from "react";
import * as ImagePicker from "expo-image-picker";
import FontAwesome from "@expo/vector-icons/FontAwesome";
import MaterialIcons from "@expo/vector-icons/MaterialIcons";
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
import { transformPlatformURI } from "@/utils/functions";
import styles from "./UploadImage.style";
import { extensionToMimeType } from "@/utils/file";
import { AntDesign } from "@expo/vector-icons";
import { TouchableWithoutFeedback } from "react-native-gesture-handler";
import { useTranslation } from "react-i18next";
import useDarkMode from "@/hooks/useDarkMode";
import OutsidePressHandler from "react-native-outside-press";

type UploadImageProps = {
  /**
   * Array of image files to set as default images
   */
  defaultImages?: ImageFileType[];

  /**
   * Function to handle file change (should be setState function)
   */
  onFileChange: (files: ImageFileType[]) => void;

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

const UploadImage = ({
  defaultImages,
  onFileChange,
  isDisabled = false,
  isMultiple = true,
  containerStyle,
  imageStyle,
  innerImageClassName,
  selectionLimit = 5,
  props
}: UploadImageProps) => {
  const isDarkMode = useDarkMode();
  const { t } = useTranslation("component");
  const [startUploadImage, setStartUploadImage] = useState(false);
  const [imageCount, setImageCount] = useState(0);
  const [fileObjects, setFileObjects] = useState<ImageFileObject[]>([]);
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
          t("uploadImage.permissionDeniedTitle"),
          t("uploadImage.permissionDeniedMessage")
        );
        return;
      }
    }

    let result = await ImagePicker.launchImageLibraryAsync({
      quality: 1,
      selectionLimit: selectionLimit - imageCount,
      allowsMultipleSelection: isMultiple,
      mediaTypes: ["images"],
      ...props
    });

    if (!result.canceled && result.assets) {
      const MAX_SIZE = 15 * 1024 * 1024;

      const validAssets = [];
      const oversizedAssets = [];

      for (const asset of result.assets) {
        if (asset.fileSize && asset.fileSize <= MAX_SIZE) {
          validAssets.push(asset);
        } else {
          oversizedAssets.push(asset.fileName || "Unnamed image");
        }
      }

      if (oversizedAssets.length > 0) {
        Alert.alert(
          t("uploadImage.fileSizeExceededTitle"),
          t("uploadImage.fileSizeExceededMessage", { count: oversizedAssets.length }),
          [{ text: t("uploadImage.okButton") }]
        );
      }

      const files = validAssets.map(asset => {
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

      if (files.length > 0) {
        const newFileObjects = [...(fileObjects || []), ...files];
        setStartUploadImage(true);
        setFileObjects(newFileObjects);
        onFileChange(newFileObjects.map(file => file.file as ImageFileType));
        setImageCount(prev => prev + files.length);
      }
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
    onFileChange(newFileObjects!.map(file => file.file as ImageFileType));
    setImageCount(prev => prev - 1);
    setSelectedImageId(null);
    selectedImageIdRef.current === null;
  };

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

  return (
    <View style={[styles.container, imageCount === 1 && styles.flexOne, containerStyle]}>
      <View style={[styles.wrapper, imageCount === 1 && styles.sizeFull]}>
        {/* Preview image */}
        <View style={styles.previewImage}>
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
                <Text className='text-white_black body-semibold'>
                  {t("uploadImage.title")}
                </Text>
              </View>
            </TouchableHighlight>
          )}
      </View>
    </View>
  );
};

export default UploadImage;
