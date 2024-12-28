import { useRef, useState } from "react";
import { ActivityIndicator, Platform, Text, TextInput, View } from "react-native";
import { AuthFormProps } from "./LoginForm";
import {
  useAnimatedStyle,
  useSharedValue,
  withSequence,
  withTiming
} from "react-native-reanimated";

import Input from "./Input";
import Button from "./Button";
import { VerifyParams } from "@/api/user";
import { useAuthContext } from "./AuthProvider";
import { AuthState } from "@/slices/auth.slice";

type VerifyFormProps = {
  onSubmit: (data: VerifyParams) => Promise<void>;
} & AuthFormProps;

type VerifyFormFields = Array<string>;

export const VerifyForm = (props: VerifyFormProps) => {
  const { onSubmit, isLoading } = props;
  const { accessToken } = useAuthContext().tokens as AuthState;
  const [formValues, setFormValues] = useState<VerifyFormFields>([
    "",
    "",
    "",
    "",
    "",
    ""
  ]);

  const inputRefs = Array.from({ length: 6 }, () => useRef<TextInput>(null));
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

  const sendCode = () => {
    const data: VerifyParams = {
      OTP: formValues.join("").toString(),
      accessToken: accessToken as string
    };
    onSubmit(data);
  };

  const handleTextChange = (value: string, index: number) => {
    if (value.length < 2) {
      setFormValues(
        formValues.map((char, idx) => (idx === index ? value.toUpperCase() : char))
      );
    }

    if (value.length === 0 && index !== 0) {
      inputRefs[index - 1].current?.focus();
    } else if (index !== inputRefs.length - 1) {
      inputRefs[index + 1].current?.focus();
    }
  };

  const resolvePos = (key: string, index: number) => {
    //if (key === "Backspace") {
      //console.log("Delete/Backspace key pressed");
      // Additional logic for handling backspace
    //}
  };

  return (
    <View className={`gap-3 ${props.className}`}>
      <View className='flex-row gap-2'>
        {inputRefs.map((ref, index) => (
          <Input
            key={index}
            ref={ref}
            autoCapitalize='characters'
            className={`text-primary `}
            value={formValues[index]}
            onChangeText={value => handleTextChange(value, index)}
            onKeyPress={({ nativeEvent }) => resolvePos(nativeEvent.key, index)}
          />
        ))}
      </View>
      <Button
        onPress={() => {
          animate();
          sendCode();
        }}
        style={[animatedStyles]}
        className='w-100 rounded-full bg-primary p-3 text-white'
        isLoading={isLoading}
        spinner={<ActivityIndicator animating={isLoading} />}
      >
        <Text className='text-center text-white'>Send</Text>
      </Button>
    </View>
  );
};

export default VerifyForm;
