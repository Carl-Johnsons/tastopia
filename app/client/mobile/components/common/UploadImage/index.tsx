import React, { useEffect, useState } from "react";
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

import { FileObject, ImageFileType } from "@/types/image";
import { globalStyles } from "../GlobalStyles";
import { transformPlatformURI } from "@/utils/functions";
import styles from "./UploadImage.style";
import { extensionToMimeType } from "@/utils/file";
import { AntDesign } from "@expo/vector-icons";
import { TouchableWithoutFeedback } from "react-native-gesture-handler";

type UploadImageProps = {
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
  const [startUploadImage, setStartUploadImage] = useState(false);
  const [imageCount, setImageCount] = useState(0);
  const [fileObjects, setFileObjects] = useState<FileObject[]>();
  const [status, requestPermission] = ImagePicker.useMediaLibraryPermissions();
  const [selectedImageId, setSelectedImageId] = useState<string | null>(null);

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
      onFileChange(newFileObjects.map(file => file.file as ImageFileType));
      setImageCount(prev => prev + files.length);
    }
  };

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

  const handleDismiss = () => {
    if (selectedImageId !== null) {
      setSelectedImageId(null);
    }
  };

  return (
    <View style={[styles.container, containerStyle]}>
      <View style={styles.wrapper}>
        {/* Preview image */}
        <View style={styles.previewImage}>
          {fileObjects?.map(fileObject => (
            <TouchableHighlight
              key={fileObject.id}
              style={[styles.uploadItem, imageCount <= 2 && styles.flexOne, imageStyle]}
              onPress={() => handleImagePress(fileObject.id)}
              underlayColor='transparent'
            >
              <View>
                <Image
                  source={{ uri: transformPlatformURI(fileObject.previewPath)! }}
                  style={styles.uploadItemImage}
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
            </TouchableHighlight>
          ))}
        </View>

        {/* Upload image button */}
        {((imageCount <= 4 && !isDisabled && isMultiple) ||
          (imageCount === 0 && !startUploadImage)) && (
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
