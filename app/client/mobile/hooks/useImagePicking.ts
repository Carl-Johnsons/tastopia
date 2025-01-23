import { extensionToMimeType } from "@/utils/file";
import {
  ImagePickerOptions,
  launchImageLibraryAsync,
  useMediaLibraryPermissions
} from "expo-image-picker";
import { Alert } from "react-native";

interface UseImagePickingParams extends ImagePickerOptions {
  /** The current number of previewing image. */
  imageCount: number;
}

interface UseImagePickingResult {
  /** Launch the image picker. */
  pickImage: () => Promise<ImageFileObject[] | null>;
}

/**
 * A hook that handles image picking from the device's gallery.
 *
 * @param initialValue The initial image data object
 */
export const useImagePicking = ({
  selectionLimit,
  imageCount,
  allowsMultipleSelection,
  ...props
}: UseImagePickingParams): UseImagePickingResult => {
  const [status, requestPermission] = useMediaLibraryPermissions();

  const pickImage = async () => {
    if (!status?.granted) {
      const { granted } = await requestPermission();

      if (!granted) {
        Alert.alert(
          "Permission Denied",
          "You need to grant permission to access the media library to upload images."
        );

        return null;
      }
    }

    const result = await launchImageLibraryAsync({
      quality: 1,
      selectionLimit: selectionLimit ? selectionLimit - imageCount : 0,
      allowsMultipleSelection,
      mediaTypes: "images",
      ...props
    });

    if (result.canceled) {
      return null;
    }

    const files = result.assets.map(asset => {
      const fileName =
        asset.fileName ||
        asset.uri.substring(asset.uri.lastIndexOf("/") + 1, asset.uri.length);

      const type = asset.mimeType || extensionToMimeType(asset.uri.split(".").pop()!);
      const file: ImageFileObject = {
        id: asset.uri,
        previewPath: asset.uri,
        file: { uri: asset.uri, type, name: fileName }
      };

      return file;
    });

    return files;
  };

  return { pickImage };
};
