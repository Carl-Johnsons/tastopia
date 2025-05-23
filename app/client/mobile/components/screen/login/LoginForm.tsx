import { ActivityIndicator, Pressable, Text, TextInputProps, View } from "react-native";
import { colors } from "@/constants/colors";
import { Controller, useForm } from "react-hook-form";
import { LoginParams } from "@/api/user";
import { loginSchema } from "@/lib/validation/auth";
import { useRouter } from "expo-router";
import { yupResolver } from "@hookform/resolvers/yup";
import Button from "../../Button";
import Input from "../../Input";
import useBounce from "@/hooks/animation/useBounce";

export interface LoginFormFields extends LoginParams {}

export type AuthFormProps = {
  isLoading?: boolean;
  className?: string;
};

type LoginFormProps = {
  onSubmit: (data: LoginFormFields) => Promise<void>;
} & AuthFormProps;

export const LoginForm = (props: LoginFormProps) => {
  const {
    control,
    handleSubmit,
    formState: { errors }
  } = useForm<LoginFormFields>({
    resolver: yupResolver(loginSchema)
  });
  const { onSubmit, isLoading } = props;
  const { animate, animatedStyles } = useBounce();
  const router = useRouter();

  const onNavigateToForgot = () => {
    router.push("/forgot-password");
  };

  return (
    <View className={`gap-[2vh] ${props.className}`}>
      <View>
        <Text className='mb-3 font-sans text-lg text-gray-600'>
          E-mail, phone number or username
        </Text>
        <Controller
          name='identifier'
          control={control}
          render={({ field: { onChange, value, onBlur } }) => (
            <CustomInput
              value={value}
              onBlur={onBlur}
              onChangeText={onChange}
              placeholder='Your email, phone number or username'
            />
          )}
        />
        {errors.identifier ? (
          <Text className='font-sans text-red-400'>{errors.identifier.message}</Text>
        ) : null}
      </View>

      <View>
        <Text className='mb-3 font-sans text-lg text-gray-600'>Password</Text>
        <Controller
          name='password'
          control={control}
          render={({ field: { onBlur, onChange, value } }) => (
            <CustomInput
              value={value}
              onBlur={onBlur}
              onChangeText={onChange}
              placeholder='Password'
              secureTextEntry
            />
          )}
        />
        {errors.password ? (
          <Text className='font-sans text-red-400'>{errors.password.message}</Text>
        ) : null}
      </View>
      <View className='flex-center'>
        <Pressable onPress={onNavigateToForgot}>
          <Text className='text-center font-medium text-lg text-primary'>
            Forgot password?
          </Text>
        </Pressable>
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
          Log in
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
  secureTextEntry,
  inputMode
}: Pick<
  TextInputProps,
  | "onChangeText"
  | "value"
  | "onBlur"
  | "placeholder"
  | "defaultValue"
  | "className"
  | "secureTextEntry"
  | "inputMode"
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
      inputMode={inputMode}
    />
  );
};

export default LoginForm;
