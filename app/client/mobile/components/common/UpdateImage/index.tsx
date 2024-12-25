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
  Alert,
  StyleProp,
  ViewStyle
} from "react-native";

import styles from "./UpdateImage.style";
import { globalStyles } from "../GlobalStyles";
import { FileObject, UpdateMediaType } from "@/helper/types";
import { convertImagesToRNFile, extensionToMimeType, generateRNFile } from "@/utils/file";
import { transformPlatformURI } from "@/utils/functions";

type UpdateImageProps = {
  /**
   * Function to handle file change (should be setState functinn)
   */
  onFileChange: (updateImages: any) => void;

  /**
   * Before parsing data to component let use convertToImagesURLs to convert image string from API to correct format and make sure it data already loaded
   */
  defaultImages?: FileObject[];

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

const UpdateImage = ({
  onFileChange,
  defaultImages = [],
  isDisabled = false,
  isMultiple = true,
  containerStyle,
  imageStyle,
  ...props
}: UpdateImageProps) => {
  const [imageCount, setImageCount] = useState(defaultImages.length);
  const [fileObjects, setFileObjects] = useState<FileObject[]>(
    defaultImages.length > 0 ? convertImagesToRNFile(defaultImages) : []
  );
  const [updateFiles, setUpdateFiles] = useState<UpdateMediaType>({
    additionalFile: [],
    fileDeleteUrls: []
  });
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

      if (fileObjects.length + files.length > 5) {
        return Alert.alert("You can only upload 5 images");
      } else {
        const newFileObjects = [...(fileObjects || []), ...files];
        setFileObjects(newFileObjects);

        const newAdditionalFile = [
          ...updateFiles.additionalFile,
          ...files.map(file => file.file as ReactNativeFile)
        ];

        if (newAdditionalFile.length > 5) {
          return Alert.alert("You can only upload 5 images");
        } else {
          setUpdateFiles(prev => ({
            additionalFile: newAdditionalFile,
            fileDeleteUrls: prev.fileDeleteUrls
          }));
          setImageCount(prev => prev + files.length);
        }
      }
    }
  };

  /**
   * Remove file from API
   * Remove file that user just uploaded
   */
  const handleRemoveImage = (id: string) => {
    const newFileObjects = fileObjects?.filter(file => file.id !== id);
    setFileObjects(newFileObjects);

    const fileToRemove = fileObjects?.find(file => file.id === id);

    setUpdateFiles(prev => {
      const newAdditionalFile = prev.additionalFile.filter(
        file => file !== fileToRemove?.file
      );

      const newFileDeleteUrls = [
        ...prev.fileDeleteUrls,
        fileToRemove?.previewPath as string
      ];

      return {
        additionalFile: defaultImages?.find(
          defaultImage => defaultImage.file === fileToRemove?.file
        )
          ? prev.additionalFile
          : newAdditionalFile,
        fileDeleteUrls: defaultImages?.find(
          defaultImage => defaultImage.previewPath === fileToRemove?.previewPath
        )
          ? newFileDeleteUrls
          : prev.fileDeleteUrls
      };
    });

    setImageCount(prev => prev - 1);
  };

  useEffect(() => {
    onFileChange(updateFiles);
  }, [updateFiles, onFileChange]);

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

export default UpdateImage;
