import { LoginParams } from "@/api/user";
import { ActivityIndicator, Text, TextInputProps, View } from "react-native";
import Input from "../../Input";
import useBounce from "@/hooks/animation/useBounce";
import { Controller, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { loginSchema } from "@/lib/validation/auth";
import Button from "../../Button";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { useState } from "react";
import { EyeIcon } from "@/constants/icons";
import {
  useAnimatedStyle,
  useSharedValue,
  withSequence,
  withTiming
} from "react-native-reanimated";

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

  return (
    <View className={`gap-[2vh] ${props.className}`}>
      <View>
        <Text className='mb-3 font-sans text-gray-600 dark:text-gray-500'>
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

      <View>
        <Text className='mb-3 font-sans text-gray-600 dark:text-gray-500'>Password</Text>
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

      <Text className='text-center font-medium text-sm text-primary'>
        Forgot password?
      </Text>

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

type CustomInputProps = Pick<
  TextInputProps,
  | "onChangeText"
  | "value"
  | "onBlur"
  | "placeholder"
  | "defaultValue"
  | "className"
  | "secureTextEntry"
> & {
  containerClassName?: string;
};

export const CustomInput = ({
  onChangeText,
  onBlur,
  value,
  placeholder,
  className,
  containerClassName,
  secureTextEntry
}: CustomInputProps) => {
  const { gray, primary } = colors;
  const { c } = useColorizer();
  const [isSecureText, setIsSecureText] = useState(secureTextEntry ? true : false);

  const buttonScale = useSharedValue(1);
  const buttonOpacity = useSharedValue(1);

  const animate = () => {
    buttonScale.value = withSequence(withTiming(0.96), withTiming(1));
    buttonOpacity.value = withSequence(withTiming(0.7), withTiming(1));
  };

  const animatedStyles = useAnimatedStyle(() => ({
    transform: [{ scale: buttonScale.value }, { translateY: "-50%" }],
    opacity: buttonOpacity.value
  }));

  return (
    <View className={`relative ${containerClassName}`}>
      <Input
        onBlur={onBlur}
        onChangeText={onChangeText}
        value={value}
        autoCapitalize='none'
        placeholder={placeholder}
        className={`text-black_white border-gray-300 p-5 focus:border-primary dark:border-gray-600 ${secureTextEntry && "pe-14"} ${className}`}
        placeholderTextColor={c(gray[300], gray[600])}
        secureTextEntry={secureTextEntry ? isSecureText : undefined}
        cursorColor={primary}
        numberOfLines={1}
      />
      {secureTextEntry && (
        <Button
          className='absolute right-5 top-1/2'
          style={animatedStyles}
          onPress={() => {
            animate();
            setIsSecureText(!isSecureText);
          }}
        >
          <EyeIcon
            width={18}
            height={12}
            color={c(gray[300], gray[600])}
          />
        </Button>
      )}
    </View>
  );
};

export default LoginForm;
