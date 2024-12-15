import { LoginParams } from "@/api/user";
import { useState } from "react";
import { ActivityIndicator, Platform, Text, View } from "react-native";
import {
  useAnimatedStyle,
  useSharedValue,
  withSequence,
  withTiming
} from "react-native-reanimated";
import Input from "./Input";
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
  const { onSubmit, isLoading } = props;
  const [formValues, setFormValues] = useState<LoginFormFields>({
    username: "",
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
      <Input
        autoCapitalize='none'
        placeholder='Username'
        className={`dark:text-white ${Platform.OS === "ios" ? "p-3.5" : ""}`}
        placeholderTextColor={"gray"}
        onChangeText={username => setFormValues({ ...formValues, username })}
        autoFocus
      ></Input>
      <Input
        autoCapitalize='none'
        placeholder='Password'
        className={`dark:text-white ${Platform.OS === "ios" ? "p-3.5" : ""}`}
        placeholderTextColor={"gray"}
        onChangeText={password => setFormValues({ ...formValues, password })}
        secureTextEntry
      ></Input>
      <Button
        onPress={() => {
          animate();
          onSubmit(formValues);
        }}
        style={[animatedStyles]}
        className='w-100 rounded-full bg-black p-3 text-white'
        isLoading={isLoading}
        spinner={
          <ActivityIndicator
            animating={isLoading}
            color={"white"}
          />
        }
      >
        <Text className='text-center text-white'>Log in</Text>
      </Button>
    </View>
  );
};

export default LoginForm;
