import { Alert, Pressable, Text, View } from "react-native";
import React, { useState } from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { router } from "expo-router";
import { useAppDispatch } from "@/store/hooks";
import { saveAuthData } from "@/slices/auth.slice";
import { ZodError } from "zod";
import { VerifyParams, resendVerifyCode, verify } from "@/api/user";
import VerifyForm from "@/components/VerifyForm";
import { useAuthContext } from "@/components/AuthProvider";
import CircleBg from "@/components/CircleBg";
import BackButton from "@/components/BackButton";

const Verify = () => {
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const dispatch = useAppDispatch();
  const { tokens, identifier } = useAuthContext();

  const onSubmit = async (data: VerifyParams) => {
    setIsSubmitting(true);

    try {
      await verify(data);
      dispatch(saveAuthData(tokens));
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
      await resendVerifyCode(tokens?.accessToken as string);
      Alert.alert("Success", "New OTP is sent.");
    } catch (error: any) {
      Alert.alert("Error", error.message);
    }
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

          <Text className='font-sans font-semibold text-4xl text-black'>
            Verification Code
          </Text>
          <Text className='font-sans text-sm text-gray-300'>
            Please type the verification code sent to {identifier}
          </Text>

          <VerifyForm
            onSubmit={onSubmit}
            isLoading={isSubmitting}
            className='mt-10'
          />

          <Pressable onPress={resendCode}>
            <Text className='text-center'>
              I don’t recevie a code! <Text className='text-primary'>Please resend</Text>
            </Text>
          </Pressable>
        </View>
      </View>
    </SafeAreaView>
  );
};

export default Verify;
