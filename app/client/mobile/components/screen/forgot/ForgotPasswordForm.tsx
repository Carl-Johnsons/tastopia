import { ActivityIndicator, Pressable, Text, TextInputProps, View } from "react-native";
import { colors } from "@/constants/colors";
import { Controller, useForm } from "react-hook-form";
import { forgotPasswordSchema } from "@/lib/validation/auth";
import { yupResolver } from "@hookform/resolvers/yup";
import Button from "../../Button";
import Input from "../../Input";
import useBounce from "@/hooks/animation/useBounce";

export interface ForgotPasswordFormFields {
  identifier?: string;
}

export type AuthFormProps = {
  isLoading?: boolean;
  className?: string;
};

type ForgotPasswordFormProps = {
  onSubmit: (data: ForgotPasswordFormFields) => Promise<void>;
} & AuthFormProps;

export const ForgotPasswordForm = (props: ForgotPasswordFormProps) => {
  const {
    control,
    handleSubmit,
    formState: { errors }
  } = useForm({
    resolver: yupResolver(forgotPasswordSchema)
  });
  const { onSubmit, isLoading } = props;
  const { animate, animatedStyles } = useBounce();

  return (
    <View className={`gap-[2vh] ${props.className}`}>
      <View>
        <Text className='mb-3 font-sans text-lg text-gray-600'>
          E-mail or phone number
        </Text>
        <Controller
          name='identifier'
          control={control}
          render={({ field: { onChange, value, onBlur } }) => (
            <CustomInput
              value={value}
              onBlur={onBlur}
              onChangeText={onChange}
              placeholder='Your email or phone number'
            />
          )}
        />
        {errors.identifier ? (
          <Text className='font-sans text-red-400'>{errors.identifier.message}</Text>
        ) : null}
      </View>
      <Button
        onPress={() => {
          animate();
          handleSubmit(onSubmit)();
        }}
        style={[animatedStyles]}
        className='rounded-full bg-primary p-3 py-6 text-white'
        isLoading={isLoading}
        spinner={
          <ActivityIndicator
            animating={isLoading}
            color={"white"}
          />
        }
      >
        <Text className='text-center font-sans font-semibold uppercase text-white'>
          Find
        </Text>
      </Button>
    </View>
  );
};

export const CustomInput = ({
  onChangeText,
  onBlur,
  value,
  placeholder,
  className,
  secureTextEntry
}: Pick<
  TextInputProps,
  | "onChangeText"
  | "value"
  | "onBlur"
  | "placeholder"
  | "defaultValue"
  | "className"
  | "secureTextEntry"
>) => {
  const { gray, primary } = colors;

  return (
    <Input
      onBlur={onBlur}
      onChangeText={onChangeText}
      value={value}
      autoCapitalize='none'
      placeholder={placeholder}
      className={`border-gray-300 p-5 focus:border-primary ${className} text-black_white`}
      placeholderTextColor={gray[300]}
      secureTextEntry={secureTextEntry}
      cursorColor={primary}
    />
  );
};

export default ForgotPasswordForm;
