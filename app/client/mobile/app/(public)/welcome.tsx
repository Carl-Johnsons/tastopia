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
import { Pressable, SafeAreaView, Text, View } from "react-native";
import GoogleButton from "@/components/GoogleButton";
import { UseLoginWithGoogle } from "@/hooks/useLoginWithGoogle";
import Button from "@/components/Button";
import { router } from "expo-router";

const Welcome = () => {
  const { loginWithGoogle } = UseLoginWithGoogle();
  const textScale = useSharedValue(0.5);
  const textTranslateY = Array.from({ length: 3 }, () => useSharedValue(100));
  const textOpacity = Array.from({ length: 3 }, () => useSharedValue(0));
  const imageBgOpacity = useSharedValue(0);
  const DELAY_MARGIN = 900;
  const TEXT_DELAY = 500;

  useEffect(() => {
    animate();
  }, []);

  const textStyles = Array.from({ length: 3 }, (_value, index) =>
    useAnimatedStyle(() => ({
      transform: [{ translateY: textTranslateY[index]?.value }],
      opacity: textOpacity[index].value
    }))
  );

  const imageBackgroundStyles = useAnimatedStyle(() => ({
    opacity: imageBgOpacity.value
  }));

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
    router.replace("/(protected)");
  };

  return (
    <SafeAreaView>
      <ImageBackground
        className='h-full'
        style={imageBackgroundStyles}
        source={require("@/assets/images/welcome_bg.png")}
      >
        <LinearGradient colors={["transparent", "#191b2f"]}>
          <View className='relative h-full px-3.5'>
            <Button
              onPress={browseAsGuest}
              className='absolute right-[26px] top-[26px] rounded-full bg-white px-5 py-3'
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
                <View className='h-[1px] grow bg-gray-300' />
              </View>

              <View className='flex items-center'>
                <GoogleButton
                  onPress={loginWithGoogle}
                  className='rounded-full border border-gray-300 bg-white p-3'
                />
              </View>

              <Button
                onPress={navigateToRegisterScreen}
                className='flex rounded-full border border-white bg-white/20 py-4'
              >
                <Text className='text-center font-medium text-lg text-white'>
                  Start with email or phone
                </Text>
              </Button>

              <Pressable onPress={navigateToLoginScreen}>
                <Text className='text-center font-medium text-sm text-gray-300'>
                  Already have an account?{" "}
                  <Text className='font-medium text-white underline'>Sign In</Text>
                </Text>
              </Pressable>
            </View>
          </View>
        </LinearGradient>
      </ImageBackground>
    </SafeAreaView>
  );
};

export default Welcome;
