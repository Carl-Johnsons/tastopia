import { forwardRef } from "react";
import {
  ImageBackground as ImageBackgroundBase,
  ImageBackgroundProps
} from "react-native";
import Animated from "react-native-reanimated";

export const ImageBackground = Animated.createAnimatedComponent(
  forwardRef((props: ImageBackgroundProps, ref: React.LegacyRef<ImageBackgroundBase>) => {
    return (
      <ImageBackgroundBase
        {...props}
        ref={ref}
      />
    );
  })
);

export default ImageBackground;
