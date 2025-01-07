import { SignUpParams } from "@/api/user";
import { ActivityIndicator, Text, View } from "react-native";
import { AuthFormProps } from "./LoginForm";
import Input from "./Input";
import Button from "./Button";
import useBounce from "@/hooks/animation/useBounce";
import { Controller, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { registerSchema } from "@/lib/validation/auth";

export interface SignUpFormFields extends SignUpParams {
  rePassword: string;
}

type SignUpFormProps = {
  onSubmit: (data: SignUpFormFields) => Promise<void>;
} & AuthFormProps;

export const SignUpForm = ({ onSubmit, isLoading, className }: SignUpFormProps) => {
  const { animate, animatedStyles } = useBounce();
  const {
    control,
    handleSubmit,
    formState: { errors }
  } = useForm({
    resolver: yupResolver(registerSchema),
    mode: "onSubmit"
  });

  return (
    <View className={`gap-[2vh] ${className}`}>
      <View>
        <Text className='mb-3 font-sans text-gray-600'>Full name</Text>
        <Controller
          name='fullName'
          control={control}
          render={({ field: { onBlur, onChange, value } }) => (
            <Input
              onBlur={onBlur}
              onChangeText={onChange}
              value={value}
              autoCapitalize='none'
              placeholder='Your full name'
              className={`border-gray-300 p-5 focus:border-primary`}
              placeholderTextColor={"gray"}
            />
          )}
        />
        {errors.fullName ? (
          <Text className='font-sans text-red-400'>{errors.fullName.message}</Text>
        ) : null}
      </View>

      <View>
        <Text className='mb-3 font-sans text-gray-600'>E-mail or phone number</Text>
        <Controller
          name='identifier'
          control={control}
          render={({ field: { onChange, value } }) => (
            <Input
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
          render={({ field: { onChange, value } }) => (
            <Input
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

        <Controller
          name='rePassword'
          control={control}
          render={({ field: { onChange, value } }) => (
            <Input
              onChangeText={onChange}
              value={value}
              autoCapitalize='none'
              placeholder='Confirm password'
              className={`mt-3 border-gray-300 p-5 focus:border-primary`}
              placeholderTextColor={"gray"}
              secureTextEntry
            />
          )}
        />
        {errors.rePassword ? (
          <Text className='font-sans text-red-400'>{errors.rePassword.message}</Text>
        ) : null}
      </View>

      <Button
        onPress={() => {
          animate();
          handleSubmit(onSubmit)();
        }}
        style={[animatedStyles]}
        className='rounded-full bg-primary p-3 px-16 py-6 text-white'
        isLoading={isLoading}
        spinner={
          <ActivityIndicator
            animating={isLoading}
            color={"white"}
          />
        }
      >
        <Text className='text-center font-sans font-semibold uppercase text-white'>
          Register
        </Text>
      </Button>
    </View>
  );
};

export default SignUpForm;
