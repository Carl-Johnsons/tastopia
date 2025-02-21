import {
  useAnimatedStyle,
  useSharedValue,
  withSequence,
  withTiming
} from "react-native-reanimated";
import { DefaultStyle } from "react-native-reanimated/lib/typescript/hook/commonTypes";

export type UseBounceResult = {
  /** Start the animation. */
  animate: () => void;

  /** The styles to assign to the object. */
  animatedStyles: DefaultStyle
};

export const useBounce = (): UseBounceResult => {
  const buttonScale = useSharedValue(1);
  const buttonOpacity = useSharedValue(1);

  const animate = () => {
    buttonScale.value = withSequence(withTiming(0.96), withTiming(1));
    buttonOpacity.value = withSequence(withTiming(0.7), withTiming(1));
  };

  const animatedStyles = useAnimatedStyle(() => ({
    transform: [{ scale: buttonScale.value }],
    opacity: buttonOpacity.value
  }));

  return { animate, animatedStyles };
};

export default useBounce;
