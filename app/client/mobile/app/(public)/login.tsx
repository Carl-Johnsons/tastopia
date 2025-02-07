import { useState } from "react";
import { Alert, Platform, Pressable, Text, View } from "react-native";
import { router } from "expo-router";
import LoginForm, { LoginFormFields } from "@/components/screen/login/LoginForm";
import { useLogin } from "@/api/user";
import { ROLE, saveAuthData } from "@/slices/auth.slice";
import { useAppDispatch } from "@/store/hooks";
import GoogleButton from "@/components/GoogleButton";
import CircleBg from "@/components/CircleBg";
import BackButton from "@/components/BackButton";
import useBounce from "@/hooks/animation/useBounce";
import useLoginWithGoogle from "@/hooks/auth/useLoginWithGoogle";
import { stringify } from "@/utils/debug";
import useSyncSetting from "@/hooks/user/useSyncSetting";
import useSyncUser from "@/hooks/user/useSyncUser";
import { dismissKeyboard } from "@/utils/keyboard";

const Login = () => {
  const isAndroid = Platform.OS === "android";
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const dispatch = useAppDispatch();
  const { loginWithGoogle } = useLoginWithGoogle();
  const { animate, animatedStyles } = useBounce();
  const loginMutation = useLogin();
  const { fetch: fetchSettings } = useSyncSetting();
  const { fetch: fetchUser } = useSyncUser();

  const onSubmit = async (data: LoginFormFields) => {
    setIsSubmitting(true);
    console.info("Begin login");

    await loginMutation.mutateAsync(data, {
      onSuccess: async data => {
        const accessToken = data.access_token;
        const refreshToken = data.refresh_token;
        const role = ROLE.USER;

        dispatch(saveAuthData({ accessToken, refreshToken, role }));

        await fetchUser();
        await fetchSettings();

        const route = "/(protected)";
        router.navigate(route);
      },
      onError: error => {
        console.log("Error", stringify(error));
        Alert.alert("Error", "An error has occured. Please try again later.");
      },
      onSettled: () => {
        setIsSubmitting(false);
      }
    });
  };

  const navigateToSignUpScreen = () => {
    router.push("/register");
  };

  return (
    <Pressable className='relative h-full' onPress={dismissKeyboard}>
      <CircleBg />
      <View
        className={`absolute ${isAndroid ? "top-[5%]" : "top-[6%]"} flex w-full justify-center gap-[4vh] px-4`}
      >
        <BackButton
          onPress={router.back}
          style={animatedStyles}
          className='w-[38px] rounded-xl border border-black bg-white px-4 py-3.5'
        />
        <Text className='font-sans font-semibold text-4xl text-black'>Login</Text>
        <LoginForm
          onSubmit={onSubmit}
          isLoading={isSubmitting}
        />
        <Pressable onPress={navigateToSignUpScreen}>
          <Text className='text-center font-medium text-sm text-gray-300'>
            Don’t have an account?{" "}
            <Text className='font-medium text-primary'>Sign Up</Text>
          </Text>
        </Pressable>

        <View className='flex-row items-center justify-center gap-5'>
          <View className='h-[1px] grow bg-gray-300' />
          <Text className='text-center font-medium text-sm text-gray-300'>
            Sign in with
          </Text>
          <View className='h-[1px] grow bg-gray-300' />
        </View>

        <View className='flex items-center'>
          <GoogleButton
            onPress={() => {
              animate();
              loginWithGoogle();
            }}
            style={animatedStyles}
            className='rounded-full border border-gray-300 p-3.5'
          />
        </View>
      </View>
    </Pressable>
  );
};

export default Login;
