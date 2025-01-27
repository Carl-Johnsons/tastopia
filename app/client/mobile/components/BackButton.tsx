import { View } from "react-native";
import Button, { CustomizedButtonProps } from "./Button";
import { Path, Svg, SvgProps } from "react-native-svg";
import { LegacyRef, forwardRef } from "react";
import Animated from "react-native-reanimated";
import useColorizer from "@/hooks/useColorizer";

export const BackButton = Animated.createAnimatedComponent(
  forwardRef(({ className, ...props }: CustomizedButtonProps, ref: LegacyRef<View>) => {
    return (
      <Button
        {...props}
        className={`bg-white_black rounded-xl border border-black dark:border-white ${className}`}
        ref={ref}
      >
        <View className='flex items-center justify-center'>
          <LeftRoundedIcon />
        </View>
      </Button>
    );
  })
);

export const LeftRoundedIcon = (props: SvgProps) => {
  const { c } = useColorizer();

  return (
    <Svg
      width={7}
      height={12}
      viewBox='0 0 7 12'
      fill='none'
      {...props}
    >
      <Path
        d='M6 1L1 5.68393L6 10.6839'
        stroke={c("black", "white")}
        strokeWidth={2}
        strokeLinecap='round'
        strokeLinejoin='round'
      />
    </Svg>
  );
};

export default BackButton;
