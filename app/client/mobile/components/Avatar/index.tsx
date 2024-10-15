import { convertToImagesURLs } from "@/utils/file";
import { Image, StyleSheet, View } from "react-native";
import { UpdateMediaType } from "@/helper/types";
import React, { useState } from "react";
import UploadAvatar from "./UploadAvatar";

type AvatarProps = {
  size?: "lg" | "md" | "sm";
  src: string;
  upload?: boolean;
  defaultImage?: string;
  // Only useable if upload is true
  onFileChange?: (file: UpdateMediaType) => void;
};

const PagoAvatar = ({
  size,
  src,
  upload = false,
  defaultImage = "",
  onFileChange = _file => {}
}: AvatarProps) => {
  const [previewPath, setPreviewPath] = useState(src);

  return (
    <View style={styles.wrapper}>
      <View>
        <Image
          style={[
            styles.avatar,
            size === "lg"
              ? styles.lager
              : size === "md"
                ? styles.medium
                : size === "sm"
                  ? styles.small
                  : styles.medium
          ]}
          source={typeof previewPath != "string" ? previewPath : { uri: previewPath }}
        />
        {upload && (
          <UploadAvatar
            onFileChange={file => {
              setPreviewPath(file.additionalFile?.[0]?.uri ?? defaultImage);
              onFileChange(file);
            }}
            {...(previewPath &&
              typeof previewPath === "string" && {
                defaultImages: convertToImagesURLs(previewPath)?.[0]
              })}
          />
        )}
      </View>
    </View>
  );
};

export default PagoAvatar;

const styles = StyleSheet.create({
  wrapper: {
    margin: 5,
    flexDirection: "row",
    justifyContent: "center"
  },

  avatar: {
    borderRadius: 50
  },
  lager: { width: 80, height: 80 },
  medium: { width: 50, height: 50 },
  small: { width: 30, height: 30 }
});
