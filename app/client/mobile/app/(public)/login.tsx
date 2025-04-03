import {
  KeyboardAvoidingView,
  Platform,
  Pressable,
  SafeAreaView,
  ScrollView,
  Text,
  View
} from "react-native";
import { ROLE, saveAuthData } from "@/slices/auth.slice";
import { router } from "expo-router";
import { useAppDispatch } from "@/store/hooks";
import { useLogin } from "@/api/user";
import { useState } from "react";
import BackButton from "@/components/BackButton";
import CircleBg from "@/components/CircleBg";
import GoogleButton from "@/components/GoogleButton";
import LoginForm, { LoginFormFields } from "@/components/screen/login/LoginForm";
import useBounce from "@/hooks/animation/useBounce";
import useLoginWithGoogle from "@/hooks/auth/useLoginWithGoogle";
import useSyncSetting from "@/hooks/user/useSyncSetting";
import useSyncUser from "@/hooks/user/useSyncUser";
import { useErrorHandler } from "@/hooks/useErrorHandler";
import { isAxiosError } from "axios";
import { Alert } from "react-native";
import { useTranslation } from "react-i18next";

const Login = () => {
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const dispatch = useAppDispatch();
  const { loginWithGoogle } = useLoginWithGoogle();
  const { animate, animatedStyles } = useBounce();
  const loginMutation = useLogin();

  const { fetch: fetchSettings } = useSyncSetting();
  const { fetch: fetchUser } = useSyncUser();

  const { t } = useTranslation("error");

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
        if (!isAxiosError(error)) {
          console.log("Error login", error);
          Alert.alert(t("alertTitle"), t("General"));
          return;
        }

        if (error.response?.status >= 400) {
          Alert.alert(t("alertTitle"), t("Identity.InvalidCredential"));
        }
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
    <SafeAreaView>
      <View className='bg-white_black200 h-full'>
        <ScrollView contentContainerClassName='pb-5'>
          <CircleBg />
          <View className={`flex w-full justify-center gap-[4vh] px-4`}>
            <BackButton
              onPress={router.back}
              style={animatedStyles}
              className='bg-white_black200 w-[38px] rounded-xl border border-black px-4 py-3.5 dark:border-white'
            />
            <Text className='text-black_white font-sans font-semibold text-4xl'>
              Login
            </Text>

            <KeyboardAvoidingView behavior={Platform.OS === "ios" ? "padding" : "height"}>
              <LoginForm
                onSubmit={onSubmit}
                isLoading={isSubmitting}
              />
            </KeyboardAvoidingView>
            <View className='flex-center'>
              <Pressable onPress={navigateToSignUpScreen}>
                <Text className='text-center font-medium text-lg text-gray-300'>
                  Don’t have an account?{" "}
                  <Text className='font-medium text-primary'>Sign Up</Text>
                </Text>
              </Pressable>
            </View>

            <View className='flex-row items-center justify-center gap-5'>
              <View className='h-[1px] grow bg-gray-300' />
              <Text className='text-center font-medium text-lg text-gray-300'>
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
        </ScrollView>
      </View>
    </SafeAreaView>
  );
};

export default Login;
