import React, { useState } from "react";
import { Alert, Pressable, Text, View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { router } from "expo-router";
import LoginForm, { LoginFormFields } from "@/components/LoginForm";
import { IDENTIFIER_TYPE, login } from "@/api/user";
import { ROLE, saveAuthData } from "@/slices/auth.slice";
import { ZodError } from "zod";
import { useAppDispatch } from "@/store/hooks";
import GoogleButton from "@/components/GoogleButton";
import CircleBg from "@/components/CircleBg";
import BackButton from "@/components/BackButton";
import { UseLoginWithGoogle } from "@/hooks/useLoginWithGoogle";
import useBounce from "@/hooks/animation/useBounce";
import { loginWithEmailSchema, loginWithPhoneNumberSchema } from "@/lib/validation/auth";

const Login = () => {
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const dispatch = useAppDispatch();
  const { loginWithGoogle } = UseLoginWithGoogle();
  const { animate, animatedStyles } = useBounce();

  const onSubmit = async (data: LoginFormFields) => {
    setIsSubmitting(true);
    console.log("Begin login");

    try {
      const identifierChecker = /[a-zA-Z@]/;

      const loginType = identifierChecker.test(data.identifier)
        ? IDENTIFIER_TYPE.EMAIL
        : IDENTIFIER_TYPE.PHONE_NUMBER;

      console.log("Login type", loginType);

      if (loginType === IDENTIFIER_TYPE.EMAIL) {
        loginWithEmailSchema.parse(data);
      } else {
        loginWithPhoneNumberSchema.parse(data);
      }

      const res = await login(data);
      const accessToken = res.access_token;
      const refreshToken = res.refresh_token;
      const role = ROLE.USER;

      dispatch(saveAuthData({ accessToken, refreshToken, role }));
      console.log("Saved tokens");

      // Need to get user's info as well
      // dispatch(saveUserData(user));

      const route = "/(protected)";
      router.navigate(route);
    } catch (error: any) {
      console.log("Error", error);

      if (error instanceof ZodError) {
        const firstErr = error.issues[0];
        return Alert.alert("Error", firstErr.message);
      }

      Alert.alert("Error", error.message);
    } finally {
      setIsSubmitting(false);
    }
  };

  const navigateToSignUpScreen = () => {
    router.push("/register");
  };

  return (
    <SafeAreaView>
      <View className='relative h-full'>
        <CircleBg />
        <View className='absolute top-[38px] flex w-full justify-center gap-[4vh] px-6'>
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
          <Pressable>
            <Text
              className='text-center font-medium text-sm text-gray-300'
              onPress={navigateToSignUpScreen}
            >
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
      </View>
    </SafeAreaView>
  );
};

export default Login;
