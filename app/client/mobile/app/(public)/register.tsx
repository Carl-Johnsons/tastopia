import { Alert, Platform, Pressable, ScrollView, Text, View } from "react-native";
import { useEffect } from "react";
import { router, usePathname } from "expo-router";
import SignUpForm from "@/components/screen/register/SignUpForm";
import { IDENTIFIER_TYPE, SignUpParams, useRegister } from "@/api/user";
import GoogleButton from "@/components/GoogleButton";
import CircleBg from "@/components/CircleBg";
import BackButton from "@/components/BackButton";
import {
  saveAuthData,
  selectIsVerifyingAccount,
  selectVerifyIdentifier
} from "@/slices/auth.slice";
import { useDispatch } from "react-redux";
import useLoginWithGoogle from "@/hooks/auth/useLoginWithGoogle";
import { getIdentifierType } from "@/utils/checker";
import useSyncSetting from "@/hooks/user/useSyncSetting";
import useSyncUser from "@/hooks/user/useSyncUser";

const Register = () => {
  const isAndroid = Platform.OS === "android";
  const { loginWithGoogle } = useLoginWithGoogle();
  const dispatch = useDispatch();
  const isVerifyingAccount = selectIsVerifyingAccount();
  const verifyIdentifier = selectVerifyIdentifier();
  const currentRouteName = usePathname();
  const { mutateAsync: register, isLoading: isSubmitting } = useRegister();
  const { upload: uploadSettings } = useSyncSetting();
  const { fetch: fetchUser } = useSyncUser();

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
    const registerType = getIdentifierType(data.identifier as string) as IDENTIFIER_TYPE;

    register(
      { data, type: registerType },
      {
        onSuccess: async res => {
          dispatch(
            saveAuthData({
              accessToken: res.access_token,
              refreshToken: res.refresh_token,
              isVerifyingAccount: true,
              verifyIdentifier: data.identifier
            })
          );

          await uploadSettings();
          await fetchUser();

          const route = "/verify";
          router.push(route);
        },
        onError: error => {
          Alert.alert("Error", error.message);
        }
      }
    );
  };

  const navigateToSignInScreen = () => {
    router.push("/login");
  };

  return (
    <ScrollView className='relative h-full'>
      <CircleBg />
      <View
        className={`absolute ${isAndroid ? "top-[5%]" : "top-[6%]"} flex w-full justify-center gap-[3vh] px-4`}
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
    </ScrollView>
  );
};

export default Register;
