import { Alert, Platform, Pressable, Text, View } from "react-native";
import React, { useEffect, useState } from "react";
import { router, usePathname } from "expo-router";
import SignUpForm, { SignUpFormFields } from "@/components/SignUpForm";
import { ZodError } from "zod";
import { IDENTIFIER_TYPE, SignUpParams, useRegister } from "@/api/user";
import GoogleButton from "@/components/GoogleButton";
import CircleBg from "@/components/CircleBg";
import BackButton from "@/components/BackButton";
import {
  saveAuthData,
  selectIsVerifyingAccount,
  selectVerifyIdentifier
} from "@/slices/auth.slice";
import {
  registerWithEmailSchema,
  registerWithPhoneNumberSchema
} from "@/lib/validation/auth";
import { useDispatch } from "react-redux";
import { useLoginWithGoogle } from "@/hooks";
const Register = () => {
  const isAndroid = Platform.OS === "android";
  const { loginWithGoogle } = useLoginWithGoogle();
  const dispatch = useDispatch();
  const isVerifyingAccount = selectIsVerifyingAccount();
  const verifyIdentifier = selectVerifyIdentifier();
  const currentRouteName = usePathname();
  const { mutateAsync: register, isLoading: isSubmitting } = useRegister();

  useEffect(() => {
    if (isVerifyingAccount && currentRouteName === "/register") {
      Alert.alert(
        "Verifying account in pending",
        `You have an account ${verifyIdentifier} that is in the verifying process. Do you want to continue the process?`,
        [
          {
            text: "Yes",
            onPress: () => router.navigate("/verify")
          },
          {
            text: "No"
          }
        ]
      );
    }
  }, [isVerifyingAccount, currentRouteName]);

  const onSubmit = async (data: SignUpParams) => {
    const identifierChecker = new RegExp(".*[a-z,A-Z,@].*");
    const registerType = identifierChecker.test(data.identifier)
      ? IDENTIFIER_TYPE.EMAIL
      : IDENTIFIER_TYPE.PHONE_NUMBER;

    if (registerType === IDENTIFIER_TYPE.EMAIL) {
      registerWithEmailSchema.parse(data);
    } else {
      registerWithPhoneNumberSchema.parse(data);
    }

    register(
      { data, type: registerType },
      {
        onSuccess: data => {
          dispatch(
            saveAuthData({
              accessToken: data.access_token,
              refreshToken: data.refresh_token,
              isVerifyingAccount: true,
              verifyIdentifier: data.identifier
            })
          );

          const route = "/verify";
          router.push(route);
        },
        onError: error => {
          if (error instanceof ZodError) {
            const firstErr = error.issues[0];
            console.log("Error", error);
            return Alert.alert("Error", firstErr.message);
          }

          Alert.alert("Error", error.message);
        }
      }
    );
  };

  const navigateToSignInScreen = () => {
    router.push("/login");
  };

  return (
    <View className='relative h-full'>
      <CircleBg />
      <View
        className={`absolute ${isAndroid ? "top-[5%]" : "top-[6%]"} flex w-full justify-center gap-[4vh] px-4`}
      >
        <BackButton
          onPress={router.back}
          className='w-[38px] rounded-xl border border-black bg-white px-4 py-3.5'
        />
        <Text className='font-sans font-semibold text-4xl text-black'>Register</Text>
        <SignUpForm
          onSubmit={onSubmit}
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
  );
};

export default Register;
