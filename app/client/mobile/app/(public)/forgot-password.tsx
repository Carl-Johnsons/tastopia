import { Alert, Platform, Pressable, Text, View, Image } from "react-native";
import { dismissKeyboard } from "@/utils/keyboard";
import { getIdentifierType } from "@/utils/checker";
import { IDENTIFIER_TYPE, useFindAccount, useRequestChangePassword } from "@/api/user";
import { ISimpleUserResponse } from "@/generated/interfaces/user.interface";
import { stringify } from "@/utils/debug";
import { useCallback, useState } from "react";
import { useRouter } from "expo-router";
import BackButton from "@/components/BackButton";
import Button from "@/components/Button";
import CircleBg from "@/components/CircleBg";
import ForgotPasswordForm, {
  ForgotPasswordFormFields
} from "@/components/screen/forgot/ForgotPasswordForm";
import useBounce from "@/hooks/animation/useBounce";
import { useErrorHandler } from "@/hooks/useErrorHandler";

const ForgotPassword = () => {
  const isAndroid = Platform.OS === "android";
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const { animate, animatedStyles } = useBounce();
  const { mutateAsync: findAccountMutateAsync } = useFindAccount();
  const { mutateAsync: requestChangePasswordAsync } = useRequestChangePassword();

  const router = useRouter();
  const [identifier, setIdentifier] = useState<string>();
  const [simpleUser, setSimpleUser] = useState<ISimpleUserResponse>();
  const [isNotFound, setIsNotFound] = useState<boolean>(false);

  const { handleError } = useErrorHandler();

  const onSubmit = async (data: ForgotPasswordFormFields) => {
    setIsSubmitting(true);
    await findAccountMutateAsync(
      {
        data,
        type: getIdentifierType(data.identifier as string) as IDENTIFIER_TYPE
      },
      {
        onSuccess: async _data => {
          setSimpleUser(_data);
          setIdentifier(data.identifier);
          setIsNotFound(false);
        },
        onError: error => {
          handleError(error);

          if (error.status === 404) {
            setSimpleUser(undefined);
            setIdentifier(undefined);
            setIsNotFound(true);
          }
        },
        onSettled: () => {
          setIsSubmitting(false);
        }
      }
    );
  };

  const onClickChangePassword = useCallback(async () => {
    if (!identifier) {
      return;
    }
    await requestChangePasswordAsync(
      {
        data: {
          identifier
        },
        type: getIdentifierType(identifier as string) as IDENTIFIER_TYPE
      },
      {
        onSuccess: () => {
          router.push({
            pathname: "/change-password",
            params: {
              identifier
            }
          });
        },
        onError: error => handleError(error)
      }
    );
  }, [identifier]);

  return (
    <Pressable
      className='bg-white_black200 relative h-full'
      onPress={dismissKeyboard}
    >
      <CircleBg />
      <View
        className={`absolute ${isAndroid ? "top-[5%]" : "top-[6%]"} flex w-full justify-center gap-[4vh] px-4`}
      >
        <BackButton
          onPress={router.back}
          style={animatedStyles}
          className='bg-white_black200 w-[38px] rounded-xl border border-black px-4 py-3.5 dark:border-white'
        />
        <Text className='text-black_white font-sans font-semibold text-4xl'>
          Find your account
        </Text>
        {isNotFound ? (
          <View className='flex-center flex-1 gap-4'>
            <Text className='paragraph-medium text-primary'>
              Found no result, please try again
            </Text>
          </View>
        ) : (
          simpleUser && (
            <View className='flex-center flex-1 gap-4'>
              <Image
                source={{ uri: simpleUser.avtUrl }}
                className='rounded-full'
                style={{ width: 80, height: 80 }}
              />
              <Text className='paragraph-medium text-black_white'>
                {simpleUser.displayName}
              </Text>
            </View>
          )
        )}

        <ForgotPasswordForm
          onSubmit={onSubmit}
          isLoading={isSubmitting}
        />
        {simpleUser && !isNotFound && (
          <Button
            onPress={() => {
              animate();
              onClickChangePassword();
            }}
            style={[animatedStyles]}
            className='flex-1 rounded-full bg-secondary p-3 py-6'
          >
            <Text className='text-center font-sans font-semibold uppercase text-white'>
              Change password
            </Text>
          </Button>
        )}
      </View>
    </Pressable>
  );
};

export default ForgotPassword;
