import { SignUpParams } from "@/api/user";
import { useState } from "react";
import { ActivityIndicator, Text, View } from "react-native";
import { AuthFormProps } from "./LoginForm";
import Input from "./Input";
import Button from "./Button";
import useBounce from "@/hooks/animation/useBounce";

export interface SignUpFormFields extends SignUpParams {}

type SignUpFormProps = {
  onSubmit: (data: SignUpFormFields) => Promise<void>;
} & AuthFormProps;

export const SignUpForm = (props: SignUpFormProps) => {
  const { onSubmit, isLoading } = props;
  const [formValues, setFormValues] = useState<SignUpFormFields>({
    fullName: "",
    identifier: "",
    password: ""
  });
  const { animate, animatedStyles } = useBounce();

  return (
    <View className={`gap-[2vh] ${props.className}`}>
      <View>
        <Text className='mb-3 font-sans text-gray-600'>Full name</Text>
        <Input
          autoCapitalize='none'
          placeholder='Your full name'
          className={`border-gray-300 p-5 focus:border-primary`}
          placeholderTextColor={"gray"}
          onChangeText={fullName => setFormValues({ ...formValues, fullName })}
        />
      </View>

      <View>
        <Text className='mb-3 font-sans text-gray-600'>E-mail or phone number</Text>
        <Input
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
          autoCapitalize='none'
          placeholder='Password'
          className={`border-gray-300 p-5 focus:border-primary`}
          placeholderTextColor={"gray"}
          onChangeText={password => setFormValues({ ...formValues, password })}
          secureTextEntry
        />
      </View>

      <Button
        onPress={() => {
          animate();
          onSubmit(formValues);
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
