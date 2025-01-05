import { LoginParams } from "@/api/user";
import { ActivityIndicator, Alert, Pressable, Text, View } from "react-native";
import Input from "./Input";
import useBounce from "@/hooks/animation/useBounce";
import { useEffect } from "react";
import { Controller, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { loginSchema } from "@/lib/validation/auth";
import Button from "./Button";

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

  useEffect(() => {
    console.log("errors", JSON.stringify(errors, null, 2));
    
  }, [errors]);

  return (
    <View className={`gap-[2vh] ${props.className}`}>
      <View>
        <Text className='mb-3 font-sans text-gray-600'>E-mail or phone number</Text>
        <Controller
          name='identifier'
          control={control}
          render={({ field: { onBlur, onChange, value } }) => (
            <Input
              onBlur={onBlur}
              onChangeText={onChange}
              value={value}
              autoCapitalize='none'
              placeholder='Your email or phone number'
              className={`border-gray-300 p-5 focus:border-primary`}
              placeholderTextColor={"gray"}
            />
          )}
        />
        {errors.identifier ? (
          <Text className='font-sans text-red-400'>{errors.identifier.message}</Text>
        ) : null}
      </View>

      <View>
        <Text className='mb-3 font-sans text-gray-600'>Password</Text>
        <Controller
          name='password'
          control={control}
          render={({ field: { onBlur, onChange, value } }) => (
            <Input
              onBlur={onBlur}
              onChangeText={onChange}
              value={value}
              autoCapitalize='none'
              placeholder='Password'
              className={`border-gray-300 p-5 focus:border-primary`}
              placeholderTextColor={"gray"}
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

export default LoginForm;
