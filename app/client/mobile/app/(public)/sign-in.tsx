import React, { useState } from "react";
import { Alert, Pressable, Text, View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { router } from "expo-router";
import LoginForm, { LoginFormFields } from "@/components/LoginForm";
import { signIn } from "@/api/user";
import { saveAuthData } from "@/slices/auth.slice";
import { ZodError } from "zod";
import { useAppDispatch } from "@/store/hooks";
import { saveUserData } from "@/slices/user.slice";
import Logo from "../../assets/logo.svg";
import { useColorModeValue } from "@/hooks/alternator";

const SignIn = () => {
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const dispatch = useAppDispatch();

  const login = async (data: LoginFormFields) => {
    setIsSubmitting(true);

    try {
      const res = await signIn(data);
      const jwtToken = res.jwtToken;
      const user = res.user;

      console.log("data in login", JSON.stringify(user, null, 2));

      dispatch(saveAuthData({ jwtToken }));
      dispatch(saveUserData(user));

      const route = "/(protected)";
      console.log("navigating to route", route);

      router.navigate(route);
    } catch (error: any) {
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
    router.navigate("/sign-up");
  };

  return (
    <SafeAreaView>
      <View className='h-full w-full dark:bg-black-200'>
        <View className={`flex h-[86%] justify-center gap-2 p-4`}>
          <View className='items-center'>
            <Logo
              width={100}
              height={100}
              fill={useColorModeValue("black", "white")}
            />
          </View>
          <LoginForm
            onSubmit={login}
            isLoading={isSubmitting}
            className='mt-10'
          />
          <Pressable>
            <Text
              className='active:color-gray text-center active:underline dark:text-white'
              onPress={navigateToSignUpScreen}
            >
              I don't have an account 🥲
            </Text>
          </Pressable>
        </View>
      </View>
    </SafeAreaView>
  );
};

export default SignIn;
