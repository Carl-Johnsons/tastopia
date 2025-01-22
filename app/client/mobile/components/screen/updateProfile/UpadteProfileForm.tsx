import { Text, View } from "react-native";
import { updateUserSchema } from "@/lib/validation/user";
import Input from "../../Input";
import { Controller, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { InferType } from "yup";
import { useTranslation } from "react-i18next";
import { TextInputProps } from "react-native";
import { selectUser } from "@/slices/user.slice";
import RNPickerSelect, {
  PickerSelectProps,
  PickerStyle
} from "react-native-picker-select";
import { colors } from "@/constants/colors";
import { FONT_PRIMARY } from "@/constants/fonts";
import { ArrowDownIcon } from "@/constants/icons";
import useColorizer from "@/hooks/useColorizer";

export interface UpdateProfileFormFields extends InferType<typeof updateUserSchema> {}

type UpdateProfileFormProps = {
  className?: string;
};

export const UpdateProfileForm = ({ className }: UpdateProfileFormProps) => {
  const { t } = useTranslation("updateProfile");
  const { accountUsername, displayName, bio, gender } = selectUser();
  const {
    control,
    formState: { errors }
  } = useForm({
    resolver: yupResolver(updateUserSchema),
    mode: "onSubmit"
  });

  return (
    <View className={`gap-6 ${className}`}>
      <View>
        <Text className='text-black_white mb-2 font-sans'>{t("displayName.title")}</Text>
        <Controller
          name='displayName'
          control={control}
          render={({ field: { onChange, value } }) => (
            <CustomInput
              onChangeText={onChange}
              value={value}
              defaultValue={displayName}
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
              defaultValue={accountUsername}
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
              defaultValue={bio}
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
              onValueChange={value => {
                onChange(value);
              }}
              items={[
                { label: t("gender.male"), value: "male" },
                { label: t("gender.female"), value: "female" }
              ]}
              placeholder={{ label: t("gender.prompt") }}
            />
          )}
        />
        {errors.gender ? (
          <Text className='font-sans text-red-400'>{errors.gender.message}</Text>
        ) : null}
      </View>
    </View>
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

const Select = (props: PickerSelectProps) => {
  const { gray, black, white } = colors;
  const { c } = useColorizer();

  const styles: PickerStyle = {
    viewContainer: {
      backgroundColor: c(gray[100], black[100]),
      borderRadius: 8
    },
    inputAndroidContainer: {
      paddingLeft: 16
    },
    inputAndroid: {
      color: c(black.DEFAULT, white.DEFAULT)
    },
    inputIOSContainer: {
      width: "100%",
      height: 50,
      paddingLeft: 16,
      justifyContent: "center",
      borderRadius: 8
    },
    inputIOS: {
      color: c(black.DEFAULT, white.DEFAULT)
    },
    placeholder: {
      fontFamily: FONT_PRIMARY.sans.join(", "),
      color: gray[500]
    },
    iconContainer: {
      right: 16,
      top: "50%",
      transform: [{ translateY: "-50%" }]
    }
  };

  return (
    <RNPickerSelect
      style={styles}
      Icon={() => (
        <ArrowDownIcon
          width={16}
          height={16}
          color={gray[400]}
        />
      )}
      {...props}
    />
  );
};

export default UpdateProfileForm;
