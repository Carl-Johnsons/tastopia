import React, { useEffect, useState } from "react";
import * as ImagePicker from "expo-image-picker";
import { ReactNativeFile } from "apollo-upload-client";
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

import { extensionToMimeType, generateRNFile } from "@/utils/file";
import { FileObject } from "@/helper/types";
import { globalStyles } from "../GlobalStyles";
import { transformPlatformURI } from "@/utils/functions";
import styles from "./UploadImage.style";

type UploadImageProps = {
  /**
   * Function to handle file change (should be setState function)
   */
  onFileChange: (files: ReactNativeFile[]) => void;

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
   * Image style
   */
  imageStyle?: StyleProp<ViewStyle>;

  /**
   * ImagePicker options
   */
  props?: ImagePicker.ImagePickerOptions;
};

const UploadImage = ({
  onFileChange,
  isDisabled = false,
  isMultiple = true,
  containerStyle,
  imageStyle,
  props
}: UploadImageProps) => {
  const [imageCount, setImageCount] = useState(0);
  const [fileObjects, setFileObjects] = useState<FileObject[]>();
  const [status, requestPermission] = ImagePicker.useMediaLibraryPermissions();

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
      selectionLimit: 5 - imageCount,
      allowsMultipleSelection: isMultiple,
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      ...props
    });

    if (!result.canceled && result.assets) {
      const files = result.assets.map(asset => {
        const fileName =
          asset.fileName ||
          asset.uri.substring(asset.uri.lastIndexOf("/") + 1, asset.uri.length);
        const type = asset.mimeType || extensionToMimeType(asset.uri.split(".").pop()!);

        const RNFile = generateRNFile(asset.uri, fileName, type);

        return {
          id: asset.uri,
          previewPath: asset.uri,
          file: RNFile
        };
      });

      const newFileObjects = [...(fileObjects || []), ...files];
      setFileObjects(newFileObjects);
      onFileChange(newFileObjects.map(file => file.file as ReactNativeFile));
      setImageCount(prev => prev + files.length);
    }
  };

  const handleRemoveImage = (id: string) => {
    const newFileObjects = fileObjects?.filter(file => file.id !== id);
    setFileObjects(newFileObjects);
    onFileChange(newFileObjects!.map(file => file.file as ReactNativeFile));
    setImageCount(prev => prev - 1);
  };

  return (
    <View
      style={[styles.container, imageCount > 0 && styles.containerActive, containerStyle]}
    >
      {/* Preview images */}
      <View style={styles.previewImage}>
        {fileObjects?.map(fileObject => (
          <View
            style={[styles.uploadItem, imageCount <= 2 && styles.flexOne, imageStyle]}
            key={fileObject.id}
          >
            <TouchableHighlight
              onPress={() => !isDisabled && handleRemoveImage(fileObject.id)}
              disabled={isDisabled}
              style={styles.removeItemButton}
              underlayColor={"none"}
            >
              <MaterialIcons
                style={styles.removeItemButtonIcon}
                name='highlight-remove'
                size={30}
                color={globalStyles.color.primary}
              />
            </TouchableHighlight>
            <Image
              source={{ uri: transformPlatformURI(fileObject.previewPath)! }}
              style={styles.uploadItemImage}
            />
          </View>
        ))}

        {imageCount <= 4 && !isDisabled && (
          <TouchableHighlight
            onPress={pickImage}
            style={styles.uploadButton}
            underlayColor={globalStyles.color.secondary}
          >
            <View style={styles.uploadButtonWrapper}>
              <FontAwesome
                name='cloud-upload'
                size={24}
                color={globalStyles.color.light}
              />
              <Text style={styles.uploadButtonText}>Upload</Text>
            </View>
          </TouchableHighlight>
        )}
      </View>
    </View>
  );
};

export default UploadImage;
