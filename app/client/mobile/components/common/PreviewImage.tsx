import React, { useMemo, useState } from "react";
import { ImageProps, TouchableWithoutFeedback, View, Image } from "react-native";
import ImageView from "react-native-image-viewing";
import Avatar from "./Avatar";
import { filterImageSource } from "@/utils/dataFilter";

type PreviewImageProps = {
  imgUrl: string;
  className?: string;
  width?: number;
  height?: number;
  alt?: string;
  titleClassName?: string;
} & ImageProps;

const PreviewImage = (props: PreviewImageProps) => {
  const imgUrl: string | undefined = useMemo(
    () => filterImageSource(props.imgUrl),
    [props.imgUrl]
  );
  const { className, width = 100, height = 100, titleClassName = "", alt = "" } = props;

  const [visible, setIsVisible] = useState(false);
  const [isError, setIsError] = useState(false);

  const handleToggle = () => {
    setIsVisible(pre => !pre);
  };

  return (
    <View>
      <TouchableWithoutFeedback onPress={handleToggle}>
        <Image
          source={{ uri: imgUrl }}
          className={className}
          style={{ width, height }}
          onError={() => setIsError(true)}
        />
      </TouchableWithoutFeedback>
      {!isError ? (
        <ImageView
          images={[{ uri: imgUrl }]}
          imageIndex={0}
          visible={visible}
          onRequestClose={() => setIsVisible(false)}
          animationType='fade'
        />
      ) : null}
    </View>
  );
};

export default PreviewImage;
