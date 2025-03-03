import { useRef, useState } from "react";
import { ActivityIndicator, Alert, Text, TextInput, View } from "react-native";
import { AuthFormProps } from "../login/LoginForm";
import {
  useAnimatedStyle,
  useSharedValue,
  withSequence,
  withTiming
} from "react-native-reanimated";

import Input from "../../Input";
import Button from "../../Button";
import { verifySchema } from "@/lib/validation/auth";
import { colors } from "@/constants/colors";

type VerifyFormProps = {
  onSubmit: (otp: string) => Promise<void>;
} & AuthFormProps;

type VerifyFormFields = Array<string>;

export const VerifyForgotPasswordForm = (props: VerifyFormProps) => {
  const { onSubmit, isLoading } = props;
  const { primary } = colors;
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

  const sendCode = async () => {
    const OTP = formValues.join("").toString().trim();
    await verifySchema.validate({ OTP });
    await onSubmit(OTP);
  };

  const handleTextChange = (value: string, index: number) => {
    const firstChar = value.charAt(0).toUpperCase();

    setFormValues(
      formValues.map((currentChar, idx) => (idx === index ? firstChar : currentChar))
    );

    if (value?.length === 0 && index > 0) {
      inputRefs[index - 1].current?.focus();
    } else if (value.length > 0 && index < inputRefs.length - 1) {
      inputRefs[index + 1].current?.focus();
    }
  };

  return (
    <View className={`gap-[2vh] ${props.className}`}>
      <View className='flex-row justify-center gap-3.5'>
        {inputRefs.map((ref, index) => (
          <Input
            key={index}
            ref={ref}
            autoCapitalize='characters'
            className={`aspect-square w-[53px] shrink grow border-gray-300 text-center text-primary focus:border-primary`}
            value={formValues[index]}
            onChangeText={value => handleTextChange(value, index)}
            autoFocus={index === 0}
            cursorColor={primary}
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

export default VerifyForgotPasswordForm;
