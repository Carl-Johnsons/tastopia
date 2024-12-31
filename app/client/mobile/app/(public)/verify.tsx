import { Alert, Platform, Pressable, Text, View } from "react-native";
import React, { useState } from "react";
import { router } from "expo-router";
import { useAppDispatch } from "@/store/hooks";
import {
  ROLE,
  saveAuthData,
  selectAccessToken,
  selectVerifyIdentifier
} from "@/slices/auth.slice";
import { ZodError } from "zod";
import { VerifyParams, resendVerifyCode, verify } from "@/api/user";
import VerifyForm from "@/components/VerifyForm";
import CircleBg from "@/components/CircleBg";
import BackButton from "@/components/BackButton";
import { verifySchema } from "@/lib/validation/auth";

const Verify = () => {
  const isAndroid = Platform.OS === "android";
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const dispatch = useAppDispatch();
  const identifier = selectVerifyIdentifier();
  const accessToken = selectAccessToken() as string;

  const onSubmit = async (data: VerifyParams) => {
    setIsSubmitting(true);

    try {
      console.log("VerifyParams", JSON.stringify(data, null, 2));

      verifySchema.parse(data);
      await verify(data, accessToken);

      dispatch(saveAuthData({ isVerifyingAccount: false, role: ROLE.USER }));
      console.log("saved auth data");

      // Set user's data here
      // dispatch(saveUserData(user));
      //
      const route = "/(protected)";
      router.navigate(route);
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

  const resendCode = async () => {
    console.log("Requesting for new OTP");

    try {
      await resendVerifyCode(accessToken as string);
      Alert.alert("Success", "New OTP is sent.");
    } catch (error: any) {
      Alert.alert("Error", error.message);
    }
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

        <Text className='font-sans font-semibold text-4xl text-black'>
          Verification Code
        </Text>
        <Text className='font-sans text-sm text-gray-300'>
          Please type the verification code sent to{"\n"}
          <Text className='text-primary'>{identifier}</Text>
        </Text>

        <VerifyForm
          onSubmit={onSubmit}
          isLoading={isSubmitting}
          className='mt-[5vh]'
        />

        <Pressable onPress={resendCode}>
          <Text className='text-center'>
            I don’t recevie a code! <Text className='text-primary'>Please resend</Text>
          </Text>
        </Pressable>
      </View>
    </View>
  );
};

export default Verify;
