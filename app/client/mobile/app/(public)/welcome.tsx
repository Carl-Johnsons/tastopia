import ImageBackground from "@/components/ImageBackground";
import { useEffect } from "react";
import Animated, {
  Easing,
  useAnimatedStyle,
  useSharedValue,
  withDelay,
  withTiming
} from "react-native-reanimated";
import { LinearGradient } from "expo-linear-gradient";
import { Platform, Pressable, Text, View } from "react-native";
import GoogleButton from "@/components/GoogleButton";
import Button from "@/components/Button";
import { router } from "expo-router";
import { useDispatch } from "react-redux";
import { ROLE, saveAuthData } from "@/slices/auth.slice";
import useLoginWithGoogle from "@/hooks/auth/useLoginWithGoogle";

const Welcome = () => {
  const isAndroid = Platform.OS === "android";
  const { loginWithGoogle } = useLoginWithGoogle();
  const textScale = useSharedValue(0.5);
  const textTranslateY = Array.from({ length: 3 }, () => useSharedValue(100));
  const textOpacity = Array.from({ length: 3 }, () => useSharedValue(0));
  const imageBgOpacity = useSharedValue(0);
  const dispatch = useDispatch();
  const DELAY_MARGIN = 900;
  const TEXT_DELAY = 500;

  const textStyles = Array.from({ length: 3 }, (_value, index) =>
    useAnimatedStyle(() => ({
      transform: [{ translateY: textTranslateY[index]?.value }],
      opacity: textOpacity[index].value
    }))
  );

  const imageBackgroundStyles = useAnimatedStyle(() => ({
    opacity: imageBgOpacity.value
  }));

  useEffect(() => {
    animate();
  }, []);

  const animate = () => {
    textScale.value = withDelay(1000, withTiming(1));
    imageBgOpacity.value = withTiming(1);

    textOpacity.forEach((item, index) => {
      item.value = withDelay(
        TEXT_DELAY + index * DELAY_MARGIN,
        withTiming(1, { duration: 1500 })
      );
    });

    textTranslateY.forEach((item, index) => {
      item.value = withDelay(
        TEXT_DELAY + index * DELAY_MARGIN,
        withTiming(0, {
          duration: 2000,
          easing: Easing.out(Easing.exp)
        })
      );
    });
  };

  const navigateToRegisterScreen = () => {
    router.push("/register");
  };

  const navigateToLoginScreen = () => {
    router.push("/login");
  };

  const browseAsGuest = () => {
    dispatch(saveAuthData({ role: ROLE.GUEST }));
    router.replace("/(protected)");
  };

  return (
    <ImageBackground
      className='h-full'
      style={imageBackgroundStyles}
      source={require("@/assets/images/welcome_bg.png")}
    >
      <LinearGradient colors={["transparent", "#191b2f"]}>
        <View className='relative h-full px-3.5'>
          <Button
            onPress={browseAsGuest}
            className={`absolute right-[26px] ${isAndroid ? "top-[2%]" : "top-[6%]"} rounded-full bg-white_black px-4 py-2.5`}
          >
            <Text className='font-sans text-primary'>Skip</Text>
          </Button>

          <Animated.View className='mt-[20vh] flex gap-3.5'>
            <Animated.Text
              style={textStyles[0]}
              className='font-bold text-5xl text-black'
            >
              Welcome to
            </Animated.Text>

            <Animated.Text
              style={textStyles[1]}
              className='font-bold text-4xl text-primary'
            >
              Tastopia
            </Animated.Text>

            <Animated.Text
              style={textStyles[2]}
              className='font-sans text-lg text-white'
            >
              Where Flavors Unite
            </Animated.Text>
          </Animated.View>

          <View className='absolute bottom-[6vh] left-3.5 flex w-full gap-4'>
            <View className='flex-row items-center justify-center gap-5'>
              <View className='h-[1px] grow bg-gray-300' />
              <Animated.Text className='text-center font-medium text-sm text-gray-300'>
                Sign in with
              </Animated.Text>
              <View className='bg-white_black h-[1px] grow' />
            </View>

            <View className='flex items-center'>
              <GoogleButton
                onPress={loginWithGoogle}
                className='bg-white_black rounded-full p-3'
              />
            </View>

            <Button
              onPress={navigateToRegisterScreen}
              className='flex rounded-full border-[1.5px] border-white bg-white/20 py-4 dark:bg-black/20'
            >
              <Text className='text-center font-medium text-lg text-white'>
                Start with email or phone
              </Text>
            </Button>

            <Pressable onPress={navigateToLoginScreen}>
              <Text className='text-center font-medium text-sm text-white'>
                Already have an account?{" "}
                <Text className='font-medium text-white underline'>Sign In</Text>
              </Text>
            </Pressable>
          </View>
        </View>
      </LinearGradient>
    </ImageBackground>
  );
};

export default Welcome;
