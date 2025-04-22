import { HTMLAttributes } from "react";
import { useState } from "react";
import { ImageStyle, StyleProp, Image, StyleSheet, View } from "react-native";
import { Skeleton } from "./Skeleton";

type LoadingImageProps = {
  style: StyleProp<ImageStyle>;
  source: string;
};

const LoadingImage = ({ style, source }: LoadingImageProps) => {
  const [isLoading, setIsLoading] = useState<boolean>(true);

  return (
    <>
      {isLoading ? (
        <Skeleton className='absolute h-[240px] w-[100%] rounded-lg' />
      ) : undefined}

      <Image
        source={{ uri: source }}
        style={[style, isLoading && styles.hidden]}
        onLoadStart={() => setIsLoading(true)}
        onLoadEnd={() => setIsLoading(false)}
      />
    </>
  );
};

const styles = StyleSheet.create({
  hidden: {
    opacity: 0
  }
});

export default LoadingImage;
