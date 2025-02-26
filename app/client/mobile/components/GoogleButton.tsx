import { View, Text } from "react-native";
import { Path, Svg } from "react-native-svg";
import Button, { CustomizedButtonProps } from "./Button";
import Animated from "react-native-reanimated";
import { LegacyRef, forwardRef } from "react";

const GoogleButton = Animated.createAnimatedComponent(
  forwardRef((props: CustomizedButtonProps, ref: LegacyRef<View>) => {
    return (
      <Button
        {...props}
        ref={ref}
      >
        <View className='flex-row items-center justify-center gap-3.5'>
          <GoogleIcon />
          <Text className='font-medium text-sm uppercase text-black_white'>Google</Text>
        </View>
      </Button>
    );
  })
);

export const GoogleIcon = () => {
  return (
    <Svg
      width='31'
      height='31'
      viewBox='0 0 31 31'
      fill='none'
    >
      <Path
        d='M15.8961 5.84057C18.0602 5.80727 20.1531 6.61351 21.7355 8.09008L25.9978 3.92216C23.2632 1.35974 19.6433 -0.0453416 15.8961 0.00111641C13.0962 0.000469455 10.3514 0.778697 7.96858 2.24877C5.58574 3.71884 3.65882 5.8228 2.40332 8.32535L7.2872 12.1171C7.88437 10.3013 9.03647 8.71881 10.581 7.59271C12.1256 6.4666 13.9846 5.85375 15.8961 5.84057Z'
        fill='#E84646'
      />
      <Path
        d='M30.3954 15.4401C30.4132 14.4019 30.3059 13.3653 30.0759 12.3527H15.8962V17.9583H24.2205C24.0626 18.9411 23.7075 19.8818 23.1767 20.7239C22.6458 21.5659 21.9502 22.2919 21.1316 22.8582L25.8978 26.5497C27.3832 25.1156 28.5514 23.3859 29.3268 21.4723C30.1022 19.5587 30.4678 17.5037 30.3997 15.4401H30.3954Z'
        fill='#60A5FA'
      />
      <Path
        d='M7.30482 18.0914C6.9746 17.1299 6.80436 16.1207 6.80089 15.1041C6.80691 14.0891 6.97105 13.0813 7.28739 12.1169L2.40351 8.32507C1.34424 10.4281 0.79248 12.7501 0.79248 15.1048C0.79248 17.4596 1.34424 19.7816 2.40351 21.8846L7.30482 18.0914Z'
        fill='#FBBC05'
      />
      <Path
        d='M15.8965 30.2075C19.5754 30.3116 23.1544 29.0025 25.898 26.5493L21.1318 22.8577C19.5864 23.8938 17.7562 24.4218 15.8965 24.3681C13.9867 24.3567 12.1289 23.7443 10.5868 22.6177C9.04467 21.491 7.89645 19.9074 7.30502 18.0915L2.42114 21.8847C3.67326 24.3862 5.59702 26.4896 7.97699 27.9596C10.357 29.4295 13.0991 30.2079 15.8965 30.2075Z'
        fill='#34A853'
      />
    </Svg>
  );
};

export default GoogleButton;
