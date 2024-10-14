import { ImagePickerAsset, launchImageLibraryAsync } from "expo-image-picker";
import { useState } from "react";

interface UseImagePickingResult {
  /** The image data object */
  image: ImagePickerAsset | null;
  /** The image's base64 url */
  imageBase64Url: string;
  /** Launches the image picker */
  pickImage: () => Promise<void>;
  /** Clears the data of the picked image */
  removeImage: () => void;
}

/**
 * A hook that handles image picking from the device's gallery.
 *
 * @param initialValue The initial image data object
 */
export const useImagePicking = (
  initialValue?: ImagePickerAsset
): UseImagePickingResult => {
  const [image, setImage] = useState<ImagePickerAsset | null>(initialValue || null);
  const [imageBase64Url, setImageBase64Url] = useState<string>(initialValue ? `data:image/jpeg;base64,${initialValue.base64}` : "");

  const pickImage = async () => {
    const result = await launchImageLibraryAsync({ base64: true });

    if (!result.canceled) {
      setImage(result.assets[0]);
      setImageBase64Url(`data:image/jpeg;base64,${result.assets[0].base64}`);
    }
  };

  const removeImage = () => {
    setImage(null);
    setImageBase64Url("");
  };

  return { image, imageBase64Url, pickImage, removeImage };
};
