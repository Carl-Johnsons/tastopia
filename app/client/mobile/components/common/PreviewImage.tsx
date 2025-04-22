import { useMemo, useState } from "react";
import { ImageProps, TouchableWithoutFeedback, View, Image } from "react-native";
import ImageView from "react-native-image-viewing";
import { filterImageSource } from "@/utils/dataFilter";
import { Skeleton } from "./Skeleton";

type PreviewImageProps = {
  imgUrl: string;
  className?: string;
  width?: number;
  height?: number;
  alt?: string;
  titleClassName?: string;
  defaultImage?: any;
  isFill?: boolean;
} & ImageProps;

const PreviewImage = (props: PreviewImageProps) => {
  const imgUrl: string | undefined = useMemo(
    () => filterImageSource(props.imgUrl),
    [props.imgUrl]
  );
  const { className, width = 100, height = 100 } = props;

  const [visible, setIsVisible] = useState(false);
  const [isError, setIsError] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  const handleToggle = () => {
    setIsVisible(pre => !pre);
  };

  return (
    <View>
      {isLoading ? (
        <Skeleton className='absolute h-[200px] w-[100%] rounded-lg' />
      ) : undefined}

      <TouchableWithoutFeedback onPress={handleToggle}>
        <Image
          source={isError || !imgUrl ? props.defaultImage : { uri: imgUrl }}
          className={className}
          style={{ width: props.isFill ? "100%" : width, height }}
          onError={() => setIsError(true)}
          onLoadStart={() => setIsLoading(true)}
          onLoadEnd={() => setIsLoading(false)}
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
