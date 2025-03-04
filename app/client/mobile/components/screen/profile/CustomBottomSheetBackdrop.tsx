import { BottomSheetBackdropProps, useBottomSheet } from "@gorhom/bottom-sheet";
import { useCallback } from "react";
import { Pressable } from "react-native";
import Animated, { FadeIn, FadeOut, runOnJS } from "react-native-reanimated";

const AnimatedPressable = Animated.createAnimatedComponent(Pressable);

const CustomBottomSheetBackdrop = ({ style }: BottomSheetBackdropProps) => {
  const { close } = useBottomSheet();

  const tapHandler = useCallback(() => {
    runOnJS(close)();
  }, [close]);

  return (
    <AnimatedPressable
      onPress={tapHandler}
      entering={FadeIn.duration(100)}
      exiting={FadeOut.duration(100)}
      className='bg-black/70'
      style={[style]}
    />
  );
};

export default CustomBottomSheetBackdrop;
