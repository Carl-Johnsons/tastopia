import { Alert, Pressable, Text, View } from "react-native";
import React, { useState } from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { router } from "expo-router";
import SignUpForm, { SignUpFormFields } from "@/components/SignUpForm";
import { ZodError } from "zod";
import { IDENTIFIER_TYPE, register } from "@/api/user";
import GoogleButton from "@/components/GoogleButton";
import { useAuthContext } from "@/components/AuthProvider";
import CircleBg from "@/components/CircleBg";
import BackButton from "@/components/BackButton";
import { UseLoginWithGoogle } from "@/hooks/useLoginWithGoogle";
import { ROLE } from "@/slices/auth.slice";
import {
  registerWithEmailSchema,
  registerWithPhoneNumberSchema
} from "@/lib/validation/auth";

const Register = () => {
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const { setTokens, setIdentifier } = useAuthContext();
  const { loginWithGoogle } = UseLoginWithGoogle();

  const signUp = async (data: SignUpFormFields) => {
    setIsSubmitting(true);

    try {
      const identifierChecker = new RegExp(".*[a-z,A-Z,@].*");
      const registerType = identifierChecker.test(data.identifier)
        ? IDENTIFIER_TYPE.EMAIL
        : IDENTIFIER_TYPE.PHONE_NUMBER;

      if (registerType === IDENTIFIER_TYPE.EMAIL) {
        registerWithEmailSchema.parse(data);
      } else {
        registerWithPhoneNumberSchema.parse(data);
      }

      const res = await register(data, registerType);

      setIdentifier(data.identifier);
      setTokens({
        accessToken: res.access_token,
        refreshToken: res.refresh_token,
        role: ROLE.USER
      });

      const route = "/verify";
      router.push(route);
    } catch (error: any) {
      if (error instanceof ZodError) {
        const firstErr = error.issues[0];
        console.log("Error", error);
        return Alert.alert("Error", firstErr.message);
      }

      Alert.alert("Error", error.message);
    } finally {
      setIsSubmitting(false);
    }
  };

  const navigateToSignInScreen = () => {
    router.push("/login");
  };

  return (
    <SafeAreaView>
      <View className='relative h-full'>
        <CircleBg />
        <View className='absolute top-[38px] flex w-full justify-center gap-[4vh] px-6'>
          <BackButton
            onPress={router.back}
            className='w-[38px] rounded-xl border border-black bg-white px-4 py-3.5'
          />
          <Text className='font-sans font-semibold text-4xl text-black'>Register</Text>
          <SignUpForm
            onSubmit={signUp}
            isLoading={isSubmitting}
          />
          <Pressable onPress={navigateToSignInScreen}>
            <Text className='text-center font-medium text-sm text-gray-300'>
              Already have an account?{" "}
              <Text className='font-medium text-primary'>Login</Text>
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
              onPress={loginWithGoogle}
              className='rounded-full border border-gray-300 p-3.5'
            />
          </View>
        </View>
      </View>
    </SafeAreaView>
  );
};

export default Register;
