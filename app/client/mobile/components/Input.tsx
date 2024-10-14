import { useColorModeValue } from "@/hooks/alternator";
import { useState } from "react";
import { TextInput, TextInputProps } from "react-native";

type CustomizedInputProps = {} & TextInputProps;

export const Input = (props: CustomizedInputProps) => {
  const [isFocused, setIsFocused] = useState<boolean>(false);
  const coloredBorder = useColorModeValue("border-black-100", "border-white");

  return (
    <TextInput
      {...props}
      className={`${useColorModeValue("border-black-100", "border-white")} rounded-lg border border-gray-400 p-2 ${isFocused && coloredBorder} ${props.className}`}
      onFocus={() => setIsFocused(true)}
      onBlur={() => setIsFocused(false)}
    />
  );
};

export default Input;
