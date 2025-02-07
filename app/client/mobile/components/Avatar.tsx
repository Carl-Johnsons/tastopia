import { useEffect, useMemo, useState } from "react";
import {
  Image,
  ImageProps,
  Pressable,
  StyleProp,
  Text,
  View,
  ViewStyle
} from "react-native";
import { Skeleton } from "moti/skeleton";
import { filterImageSource } from "@/utils/dataFilter";
import useColorizer from "@/hooks/useColorizer";

interface AvatarProps {
  /** The avatar's size in pixel */
  size: number;

  /** The image's uri */
  source: { uri?: string | null };

  /** Fallback title for the avatar */
  alt: string | null;

  /** Callback that fires when the image is clicked */
  onPress?: () => void;

  /** The style that is applied to fallback title's container */
  containerStyle?: StyleProp<ViewStyle>;

  /** The class name that is applied to fallback title's container */
  containerClassname?: string | null;

  /** The style that is applied to the fallback title's text */
  titleStyle?: StyleProp<ImageProps>;

  /** The style that is applied to the fallback title's text */
  titleClassname?: string | null;
}

export const Avatar = (props: AvatarProps) => {
  const imgSource: string | undefined = useMemo(
    () => filterImageSource(props.source?.uri),
    [props.source?.uri]
  );
  const [isLoading, setIsLoading] = useState<boolean>(imgSource ? true : false);
  const [isError, setIsError] = useState<boolean>(false);
  const title = useMemo(() => filterTitle(props.alt), [props.alt]);
  const { c } = useColorizer();

  function filterTitle(title: AvatarProps["alt"]) {
    return title && title.length > 0 ? title.charAt(0).toUpperCase() : "";
  }

  useEffect(() => {
    if (imgSource) {
      setIsError(false);
    }
  }, [imgSource]);

  return (
    <View>
      <Skeleton
        colorMode={c("dark", "light")}
        show={isLoading}
        radius={"round"}
      >
        <Pressable onPress={props.onPress}>
          {imgSource && !isError ? (
            <Image
              source={{ uri: imgSource }}
              onError={() => {
                setIsLoading(false);
                setIsError(true);
              }}
              onLoadStart={() => setIsLoading(true)}
              onLoadEnd={() => setIsLoading(false)}
              className={`aspect-square rounded-full`}
              style={{ width: props.size }}
            />
          ) : (
            <ImageTitle
              title={title}
              containerClassname={`aspect-square rounded-full bg-black dark:bg-white ${props.containerClassname}`}
              titleClassname={`text-white dark:text-black ${props.titleClassname}`}
              containerStyle={Object.assign({ width: props.size }, props.containerStyle)}
              titleStyle={props.titleStyle}
            />
          )}
        </Pressable>
      </Skeleton>
    </View>
  );
};

const ImageTitle = (
  props: {
    title: string;
  } & Pick<
    AvatarProps,
    "containerStyle" | "containerClassname" | "titleStyle" | "titleClassname"
  >
) => {
  return (
    <View
      className={`${props.containerClassname} items-center justify-center`}
      style={props.containerStyle}
    >
      <Text
        className={`${props.titleClassname}`}
        style={props.titleStyle}
      >
        {props.title}
      </Text>
    </View>
  );
};

export default Avatar;
