import { FlatList } from "react-native-gesture-handler";
import { MaterialCommunityIcons } from "@expo/vector-icons";
import { useState } from "react";
import { View, Text, StyleSheet, Platform, TouchableOpacity } from "react-native";
import * as ImagePicker from "expo-image-picker";

import { extensionToMimeType, generateRNFile } from "@/utils/file";
import { FileObject, UpdateMediaType } from "@/helper/types";
import { globalStyles } from "@/components/GlobalStyles";
import Button from "@/components/Button/index";
import DropDown from "@/components/DropDown";
import { useTranslation } from "react-i18next";

type UploadAvatarProps = {
  /**
   * Function to handle file change (should be setState function)
   */
  onFileChange: (file: UpdateMediaType) => void;

  /**
   * Before parsing data to component let use convertToImagesURLs to convert image string from API to correct format and make sure it data already loaded
   */
  defaultImages?: FileObject;
};

const UploadAvatar = ({ onFileChange = () => {}, defaultImages }: UploadAvatarProps) => {
  const { t } = useTranslation("profile");
  const [showDropDown, setShowDropDown] = useState<boolean>(false);

  const pickImage = async () => {
    let result = await ImagePicker.launchImageLibraryAsync({
      quality: 1,
      mediaTypes: ImagePicker.MediaTypeOptions.Images
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

      const updateMediaType: UpdateMediaType = {
        additionalFile: [files[0].file],
        fileDeleteUrls: defaultImages?.previewPath ? [defaultImages?.previewPath] : []
      };
      onFileChange(updateMediaType);
    }
  };

  const handleRemoveImage = () => {
    const updateMediaType: UpdateMediaType = {
      additionalFile: [],
      fileDeleteUrls: defaultImages?.previewPath ? [defaultImages?.previewPath] : []
    };
    onFileChange(updateMediaType);
  };

  const listItems = [
    {
      id: "UPLOAD_IMAGE",
      option: t("changeBtn"),
      pressEvent: pickImage
    },
    {
      id: "DELETE_IMAGE",
      option: t("deleteBtn"),
      pressEvent: handleRemoveImage
    }
  ];

  return (
    <View style={styles.container}>
      <View style={styles.uploadButtonContainer}>
        <DropDown
          show={showDropDown}
          align='center'
          dropDownGap={6}
          listStyle={styles.listItem}
          onCancel={() => {
            setShowDropDown(false);
          }}
          renderBtn={() => (
            <Button
              type='iconButton'
              title=''
              buttonStyle={styles.uploadButton}
              leftIcon={
                <MaterialCommunityIcons
                  name='account-edit'
                  size={18}
                  color={globalStyles.color.primary}
                />
              }
              onPress={() => {
                setShowDropDown(true);
              }}
            />
          )}
          renderDropDownContent={() => (
            <FlatList
              data={listItems}
              keyExtractor={item => item.id}
              ItemSeparatorComponent={() => <View style={styles.separator} />}
              renderItem={({ item }) => {
                return (
                  <TouchableOpacity
                    style={styles.item}
                    onPress={item.pressEvent}
                  >
                    <Text style={styles.itemText}>{item.option}</Text>
                  </TouchableOpacity>
                );
              }}
            />
          )}
        />
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    position: "relative"
  },

  uploadButtonContainer: {
    position: "absolute",
    bottom: -5,
    right: -5
  },

  uploadButton: {
    backgroundColor: "#fff",
    borderRadius: 50
  },

  listItem: {
    borderRadius: 5,
    width: 150,
    paddingLeft: 10,
    paddingRight: 10,
    ...(Platform.OS === "ios"
      ? {
          shadowOffset: { width: 0, height: 5 },
          shadowOpacity: 0.1,
          shadowRadius: 3
        }
      : {
          elevation: 5
        })
  },

  item: {
    flexDirection: "row",
    alignItems: "center"
  },

  itemText: {
    fontSize: 14
  },

  separator: {
    height: 1,
    backgroundColor: "#ccc",
    marginTop: 4,
    marginBottom: 4
  }
});

export default UploadAvatar;
