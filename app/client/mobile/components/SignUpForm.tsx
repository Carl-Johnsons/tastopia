import { SignUpParams } from "@/api/user";
import { useState } from "react";
import { ActivityIndicator, Platform, Text, View } from "react-native";
import { AuthFormProps } from "./LoginForm";
import {
  useAnimatedStyle,
  useSharedValue,
  withReanimatedTimer,
  withSequence,
  withTiming
} from "react-native-reanimated";

import Input from "./Input";
import Button from "./Button";

export interface SignUpFormFields extends SignUpParams {}

type SignUpFormProps = {
  onSubmit: (data: SignUpFormFields) => Promise<void>;
} & AuthFormProps;

export const SignUpForm = (props: SignUpFormProps) => {
  const { onSubmit, isLoading } = props;
  const [formValues, setFormValues] = useState<SignUpFormFields>({
    name: "",
    username: "",
    email: "",
    password: ""
  });

  const buttonScale = useSharedValue(1);
  const buttonOpacity = useSharedValue(1);

  const animate = () => {
    buttonScale.value = withSequence(withTiming(0.96), withTiming(1));
    buttonOpacity.value = withSequence(withTiming(0.7), withTiming(1));
  };

  const animatedStyles = useAnimatedStyle(() => ({
    transform: [{ scale: buttonScale.value }],
    opacity: buttonOpacity.value
  }));

  return (
    <View className={`gap-3 ${props.className}`}>
      <View className='flex-row gap-3'>
        <Input
          autoCapitalize='none'
          placeholder='username'
          className={`grow basis-0 dark:text-white ${Platform.OS === "ios" ? "p-3.5" : ""}`}
          placeholderTextColor={"gray"}
          onChangeText={username => setFormValues({ ...formValues, username })}
          autoFocus
        />
        <Input
          autoCapitalize='none'
          placeholder='name'
          className={`grow basis-0 dark:text-white ${Platform.OS === "ios" ? "p-3.5" : ""}`}
          placeholderTextColor={"gray"}
          onChangeText={name => setFormValues({ ...formValues, name })}
        />
      </View>
      <Input
        autoCapitalize='none'
        placeholder='email'
        className={`dark:text-white ${Platform.OS === "ios" ? "p-3.5" : ""}`}
        placeholderTextColor={"gray"}
        onChangeText={email => setFormValues({ ...formValues, email })}
      />
      <Input
        autoCapitalize='none'
        placeholder='password'
        className={`dark:text-white ${Platform.OS === "ios" ? "p-3.5" : ""}`}
        placeholderTextColor={"gray"}
        onChangeText={password => setFormValues({ ...formValues, password })}
        secureTextEntry
      />

      <Button
        onPress={() => {
          animate();
          onSubmit(formValues);
        }}
        style={[animatedStyles]}
        className='w-100 rounded-full bg-black p-3 text-white'
        isLoading={isLoading}
        spinner={<ActivityIndicator animating={isLoading} />}
      >
        <Text className='text-center text-white'>Sign up</Text>
      </Button>
    </View>
  );
};

export default SignUpForm;
