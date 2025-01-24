import { Text, View } from "react-native";
import { updateUserSchema } from "@/lib/validation/user";
import Input from "../../Input";
import { Controller, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { InferType } from "yup";
import { useTranslation } from "react-i18next";
import { TextInputProps } from "react-native";
import { selectUser } from "@/slices/user.slice";
import { colors } from "@/constants/colors";
import { useCallback, useEffect } from "react";
import Select from "./Select";
import { selectUpdateProfile } from "@/slices/menu/profile/updateProfileForm.slice";
import useUpdateProfile from "@/hooks/components/screen/updateProfile/useUpdateProfile";
import { router } from "expo-router";
import { stringify } from "@/utils/debug";

export interface UpdateProfileFormFields extends InferType<typeof updateUserSchema> {}

type UpdateProfileFormProps = {
  className?: string;
  onSubmit: (data: IUpdateUserDTO) => void;
};

export const UpdateProfileForm = ({ className, onSubmit }: UpdateProfileFormProps) => {
  const { t } = useTranslation("updateProfile");
  const user = selectUser();
  const { avatar, background, isDirty: isDirtyImageFields } = selectUpdateProfile();
  const { setTriggerSubmit } = useUpdateProfile();

  const {
    handleSubmit,
    control,
    formState: { errors, isDirty, dirtyFields }
  } = useForm({
    resolver: yupResolver(updateUserSchema),
    mode: "onSubmit",
    defaultValues: {
      username: user.accountUsername,
      displayName: user.displayName,
      bio: user.bio,
      ...(user.gender && { gender: user.gender.toString() })
    }
  });

  /**
   * Submit the form.
   */
  const submit = useCallback(() => {
    return async () => {
      await handleSubmit(data => {
        const { displayName, username, bio, gender } = data;

        const payload: IUpdateUserDTO = {
          ...(avatar && { avatar }),
          ...(background && { background }),
          ...(displayName && displayName !== user.displayName && { displayName }),
          ...(username && username !== user.accountUsername && { username }),
          ...(bio && bio !== user.bio && { bio }),
          ...(gender && gender !== user.gender && { gender })
        };

        console.debug(
          "isDirty",
          isDirty,
          "dirtyFields",
          stringify(dirtyFields),
          "isDirtyImageFields",
          stringify(isDirtyImageFields)
        );

        if (isDirty || isDirtyImageFields.avatar || isDirtyImageFields.background) {
          onSubmit(payload);
        } else {
          router.back();
        }
      })();
    };
  }, [handleSubmit, avatar, background, isDirty]);

  useEffect(() => {
    if (setTriggerSubmit && submit) {
      setTriggerSubmit(submit);
    }
  }, [setTriggerSubmit, submit]);

  return (
    <>
      <View className={`gap-6 ${className}`}>
        <View>
          <Text className='text-black_white mb-2 font-sans'>
            {t("displayName.title")}
          </Text>
          <Controller
            name='displayName'
            control={control}
            render={({ field: { onChange, value } }) => (
              <CustomInput
                onChangeText={onChange}
                value={value}
              />
            )}
          />
          {errors.displayName ? (
            <Text className='text-black_white mb-2 font-sans'>
              {errors.displayName.message}
            </Text>
          ) : null}
          <Text className='font-sans text-sm text-gray-600'>
            {t("displayName.description")}
          </Text>
        </View>

        <View>
          <Text className='text-black_white mb-2 font-sans'>{t("username.title")}</Text>
          <Controller
            name='username'
            control={control}
            render={({ field: { onChange, value } }) => (
              <CustomInput
                onChangeText={onChange}
                value={value}
              />
            )}
          />
          {errors.username ? (
            <Text className='font-sans text-red-400'>{errors.username.message}</Text>
          ) : null}
        </View>

        <View>
          <Text className='text-black_white mb-2 font-sans'>{t("bio.title")}</Text>
          <Controller
            name='bio'
            control={control}
            render={({ field: { onChange, value } }) => (
              <CustomInput
                onChangeText={onChange}
                value={value}
                placeholder={t("bio.placeholder")}
              />
            )}
          />
          {errors.bio ? (
            <Text className='font-sans text-red-400'>{errors.bio.message}</Text>
          ) : null}
        </View>

        <View>
          <Text className='text-black_white mb-2 font-sans'>{t("gender.title")}</Text>
          <Controller
            name='gender'
            control={control}
            render={({ field: { onChange, value } }) => (
              <Select
                value={value}
                onChangeValue={newValue => {
                  onChange(newValue);
                }}
              />
            )}
          />
          {errors.gender ? (
            <Text className='font-sans text-red-400'>{errors.gender.message}</Text>
          ) : null}
        </View>
      </View>
    </>
  );
};

const CustomInput = ({
  onChangeText,
  value,
  placeholder,
  defaultValue,
  className
}: Pick<
  TextInputProps,
  "onChangeText" | "value" | "placeholder" | "defaultValue" | "className"
>) => {
  const { gray } = colors;

  return (
    <Input
      onChangeText={onChangeText}
      value={value}
      autoCapitalize='none'
      className={`text-black_white border-transparent bg-gray-100 px-4 py-3 focus:border-primary dark:bg-black-100 ${className}`}
      placeholderTextColor={gray[500]}
      placeholder={placeholder ? placeholder : undefined}
      defaultValue={defaultValue ? defaultValue : undefined}
    />
  );
};

export default UpdateProfileForm;
