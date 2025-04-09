import { forwardRef } from "react";
import { Pressable, PressableProps, View } from "react-native";
import Animated from "react-native-reanimated";

export type CustomizedButtonProps = {
  isLoading?: boolean;
  spinner?: React.ReactNode;
} & PressableProps;

export const Button = Animated.createAnimatedComponent(
  forwardRef(
    ({ className, ...props }: CustomizedButtonProps, ref: React.LegacyRef<View>) => {
      return (
        <Pressable
          {...props}
          className={`${props.disabled ? "opacity-80" : ""} ${className}`}
          ref={ref}
        >
          {props.isLoading ? props.spinner : props.children}
        </Pressable>
      );
    }
  )
);

export default Button;
