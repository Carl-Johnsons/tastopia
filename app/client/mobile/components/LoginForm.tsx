import { LoginParams } from "@/api/user";
import { useState } from "react";
import { ActivityIndicator, Text, View } from "react-native";
import Input from "./Input";
import Button from "./Button";
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
  const { onSubmit, isLoading } = props;
  const [formValues, setFormValues] = useState<LoginFormFields>({
    identifier: "",
    password: ""
  });
  const { animate, animatedStyles } = useBounce();

  return (
    <View className={`gap-[2vh] ${props.className}`}>
      <View>
        <Text className='mb-3 font-sans text-gray-600'>E-mail or phone number</Text>
        <Input
          defaultValue={formValues.identifier}
          autoCapitalize='none'
          placeholder='Your email or phone number'
          className={`border-gray-300 p-5 focus:border-primary`}
          placeholderTextColor={"gray"}
          onChangeText={identifier => setFormValues({ ...formValues, identifier })}
        />
      </View>

      <View>
        <Text className='mb-3 font-sans text-gray-600'>Password</Text>
        <Input
          defaultValue={formValues.password}
          autoCapitalize='none'
          placeholder='Password'
          className={`border-gray-300 p-5 focus:border-primary`}
          placeholderTextColor={"gray"}
          onChangeText={password => setFormValues({ ...formValues, password })}
          secureTextEntry
        />
      </View>

      <Text className='text-center font-medium text-sm text-primary'>
        Forgot password?
      </Text>

      <Button
        onPress={() => {
          animate();
          onSubmit(formValues);
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
