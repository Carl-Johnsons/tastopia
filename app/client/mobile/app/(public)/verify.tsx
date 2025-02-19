import { Alert, Platform, Pressable, Text, View } from "react-native";
import { router } from "expo-router";
import { useAppDispatch } from "@/store/hooks";
import { ROLE, saveAuthData, selectVerifyIdentifier } from "@/slices/auth.slice";
import {
  useVerify,
  useResendVerifyCode,
  useGetUserDetails,
  useGetUserSettings,
  IDENTIFIER_TYPE,
  VerifyParams
} from "@/api/user";
import VerifyForm from "@/components/screen/verify/VerifyForm";
import CircleBg from "@/components/CircleBg";
import BackButton from "@/components/BackButton";
import { getIdentifierType } from "@/utils/checker";
import { saveUserData } from "@/slices/user.slice";
import { saveSettingData } from "@/slices/setting.slice";

const Verify = () => {
  const isAndroid = Platform.OS === "android";
  const dispatch = useAppDispatch();
  const identifier = selectVerifyIdentifier() as string;
  const type = getIdentifierType(identifier) as IDENTIFIER_TYPE;

  const { mutateAsync: verify, isLoading: isVerifyLoading } = useVerify();
  const { mutateAsync: resendVerifyCode, isLoading: isResendVerifyCodeLoading } =
    useResendVerifyCode();
  const getUserDetails = useGetUserDetails();
  const getUserSettings = useGetUserSettings();

  const fetchUserData = async () => {
    const { data: user } = await getUserDetails.refetch();
    dispatch(saveUserData({ ...user }));

    const { data: settings } = await getUserSettings.refetch();
    const UNION_SETTING: any = {};

    settings?.map(item => {
      const key = item.setting.code;
      const value = item.settingValue;

      UNION_SETTING[key] = value;
    });

    dispatch(saveSettingData(UNION_SETTING));
  };

  const onSubmit = async (data: VerifyParams) => {
    verify(data, {
      onSuccess: async _data => {
        dispatch(saveAuthData({ isVerifyingAccount: false, role: ROLE.USER }));
        console.log("saved auth data");

        await fetchUserData();

        const route = "/(protected)";
        router.navigate(route);
      },
      onError: error => {
        console.log("Verify error", JSON.stringify(error, null, 2));
        Alert.alert("Error", error.message);
      }
    });
  };

  const resendCode = () => {
    resendVerifyCode(
      { type },
      {
        onSuccess: () => {
          Alert.alert("Success", "New OTP is sent.");
        },
        onError: () => {
          Alert.alert("Error", "Resend verification code failed.");
        }
      }
    );
  };

  return (
    <View className='bg-white_black200 relative h-full'>
      <CircleBg />

      <View
        className={`absolute ${isAndroid ? "top-[5%]" : "top-[6%]"} flex w-full justify-center gap-[4vh] px-4`}
      >
        <BackButton
          onPress={router.back}
          className='bg-white_black200 w-[38px] rounded-xl border border-black px-4 py-3.5 dark:border-white'
        />

        <Text className='text-black_white font-sans font-semibold text-4xl'>
          Verification Code
        </Text>
        <Text className='font-sans text-lg text-gray-300'>
          Please type the verification code sent to{"\n"}
          <Text className='text-primary'>{identifier}</Text>
        </Text>

        <VerifyForm
          onSubmit={onSubmit}
          isLoading={isVerifyLoading || isResendVerifyCodeLoading}
          className='mt-[5vh]'
        />

        <Pressable onPress={resendCode}>
          <Text className='text-black_white text-center text-lg'>
            I don’t recevie a code! <Text className='text-primary'>Please resend</Text>
          </Text>
        </Pressable>
      </View>
    </View>
  );
};

export default Verify;
