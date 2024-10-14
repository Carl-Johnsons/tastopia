import React, { useState } from "react";
import { ImageProps, TouchableWithoutFeedback, View, Image } from "react-native";
import ImageView from "react-native-image-viewing";
import Avatar from "./Avatar";

type PreviewImageProps = {
  imgUrl: string;
  className?: string;
  width?: number;
  height?: number;
  alt: string;
  titleClassName?: string;
} & ImageProps;

const PreviewImage = ({
  imgUrl,
  className,
  width = 100,
  height = 100,
  titleClassName = "",
  alt = ""
}: PreviewImageProps) => {
  const [visible, setIsVisible] = useState(false);
  const [isError, setIsError] = useState(false);

  const handleToggle = () => {
    setIsVisible(pre => !pre);
  };

  return (
    <View>
      {isError ? (
        <TouchableWithoutFeedback onPress={handleToggle}>
          <Avatar
            size={100}
            alt={alt}
            source={{ uri: imgUrl }}
            containerClassname={className}
            titleClassname={titleClassName}
            containerStyle={{ width, height }}
          />
        </TouchableWithoutFeedback>
      ) : (
        <TouchableWithoutFeedback onPress={handleToggle}>
          <Image
            source={{ uri: imgUrl }}
            className={className}
            style={{ width, height }}
            onError={() => setIsError(true)}
          />
        </TouchableWithoutFeedback>
      )}

      {!isError && (
        <ImageView
          images={[{ uri: imgUrl }]}
          imageIndex={0}
          visible={visible}
          onRequestClose={() => setIsVisible(false)}
          animationType='fade'
        />
      )}
    </View>
  );
};

export default PreviewImage;
