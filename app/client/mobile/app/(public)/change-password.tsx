import { Alert, Platform, Pressable, Text, View, Image } from "react-native";
import { router, useLocalSearchParams, useRouter } from "expo-router";
import {
  IDENTIFIER_TYPE,
  useChangePassword,
  useCheckForgotPasswordOTP,
  useRequestChangePassword
} from "@/api/user";
import { getIdentifierType } from "@/utils/checker";
import { ICheckForgotPasswordDTO } from "@/generated/interfaces/identity.interface";
import { IErrorResponseDTO } from "@/api/tracking";
import { useState, useEffect } from "react";
import BackButton from "@/components/BackButton";
import CircleBg from "@/components/CircleBg";
import VerifyForgotPasswordForm from "@/components/screen/forgot/VerifyForgotPassword";
import ChangePasswordForm, {
  ChangePasswordFormFields
} from "@/components/screen/forgot/ChangePasswordForm";
import { useErrorHandler } from "@/hooks/useErrorHandler";

const ChangePassword = () => {
  const isAndroid = Platform.OS === "android";
  const params = useLocalSearchParams();
  const identifier = params.identifier as string;
  const router = useRouter();

  const [countdown, setCountdown] = useState(0);
  const [successOTP, setSuccessOTP] = useState<string>();

  const { mutateAsync: checkOTP, isLoading: isCheckOTPLoading } =
    useCheckForgotPasswordOTP();
  const { mutateAsync: changePassword, isLoading: isChangePasswordLoading } =
    useChangePassword();
  const { mutateAsync: requestChangePasswordAsync } = useRequestChangePassword();

  const { handleError } = useErrorHandler();

  const onSubmit = async (data: ICheckForgotPasswordDTO) => {
    checkOTP(
      {
        data,
        type: getIdentifierType(data.identifier as string) as IDENTIFIER_TYPE
      },
      {
        onSuccess: () => {
          setSuccessOTP(data.otp);
        },
        onError: error => {
          setSuccessOTP(undefined);
          handleError(error);
        }
      }
    );
  };

  const onSubmitChangePassword = async (data: ChangePasswordFormFields) => {
    changePassword(
      {
        data: {
          identifier,
          otp: successOTP!,
          password: data.password!
        },
        type: getIdentifierType(identifier as string) as IDENTIFIER_TYPE
      },
      {
        onSuccess: () => {
          Alert.alert("Success", "Change password successfully");
          router.push("/login");
        },
        onError: error => handleError(error)
      }
    );
  };

  const resendCode = () => {
    if (countdown > 0) return;

    requestChangePasswordAsync(
      {
        data: {
          identifier
        },
        type: getIdentifierType(identifier as string) as IDENTIFIER_TYPE
      },
      {
        onSuccess: () => {
          Alert.alert("Resend successfully!");
          setCountdown(30);
        },
        onError: error => handleError(error)
      }
    );
  };

  useEffect(() => {
    let interval: NodeJS.Timeout | null = null;
    if (countdown > 0) {
      interval = setInterval(() => {
        setCountdown(prevCountdown => prevCountdown - 1);
      }, 1000);
    } else if (interval) {
      clearInterval(interval);
    }
    return () => {
      if (interval) clearInterval(interval);
    };
  }, [countdown]);

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
        {!successOTP ? (
          <>
            <Text className='text-black_white font-sans font-semibold text-4xl'>
              Verification Code
            </Text>
            <Text className='font-sans text-lg text-gray-300'>
              Please type the verification code sent to{"\n"}
              <Text className='text-primary'>{identifier}</Text>
            </Text>

            <VerifyForgotPasswordForm
              onSubmit={otp =>
                onSubmit({
                  otp,
                  identifier
                })
              }
              isLoading={isCheckOTPLoading}
              className='mt-[5vh]'
            />

            <Pressable onPress={resendCode}>
              <Text className='text-black_white text-center text-lg'>
                I don't receive a code!{" "}
                {countdown > 0 ? (
                  <Text className='text-gray-500'>Please wait {countdown}s</Text>
                ) : (
                  <Text className='text-primary'>Please resend</Text>
                )}
              </Text>
            </Pressable>
          </>
        ) : (
          <>
            <Text className='text-black_white font-sans font-semibold text-4xl'>
              Change password
            </Text>
            <Text className='font-sans text-lg text-gray-300'>
              Enter a new password for{"\n"}
              <Text className='text-primary'>{identifier}</Text>
            </Text>

            <ChangePasswordForm
              onSubmit={onSubmitChangePassword}
              isLoading={isChangePasswordLoading}
            />
          </>
        )}
      </View>
    </View>
  );
};

export default ChangePassword;
