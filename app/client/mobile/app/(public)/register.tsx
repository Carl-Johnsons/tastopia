import {
  Alert,
  KeyboardAvoidingView,
  Platform,
  Pressable,
  SafeAreaView,
  ScrollView,
  Text,
  View
} from "react-native";
import { useEffect } from "react";
import { router, usePathname } from "expo-router";
import SignUpForm from "@/components/screen/register/SignUpForm";
import { IDENTIFIER_TYPE, useRegister } from "@/api/user";
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
import { IRegisterAccountDTO } from "@/generated/interfaces/identity.interface";

const Register = () => {
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
  }, [currentRouteName]);

  const onSubmit = async (data: IRegisterAccountDTO) => {
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
    <SafeAreaView>
      <View className='bg-white_black200 h-full'>
        <ScrollView contentContainerClassName='pb-5'>
          <CircleBg />

          <View className={`flex w-full justify-center gap-[3vh] px-4`}>
            <BackButton
              onPress={router.back}
              className='bg-white_black200 w-[38px] rounded-xl border border-black px-4 py-3.5 dark:border-white'
            />
            <Text className='text-black_white font-sans font-semibold text-4xl'>
              Register
            </Text>
            <KeyboardAvoidingView behavior={Platform.OS === "ios" ? "padding" : "height"}>
              <SignUpForm
                onSubmit={onSubmit}
                isLoading={isSubmitting}
              />
            </KeyboardAvoidingView>
            <Pressable onPress={navigateToSignInScreen}>
              <Text className='text-center font-medium text-lg text-gray-300'>
                Already have an account?{" "}
                <Text className='font-medium text-primary'>Login</Text>
              </Text>
            </Pressable>

            <View className='flex-row items-center justify-center gap-5'>
              <View className='h-[1px] grow bg-gray-300' />
              <Text className='text-center font-medium text-lg text-gray-300'>
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
      </View>
    </SafeAreaView>
  );
};

export default Register;
