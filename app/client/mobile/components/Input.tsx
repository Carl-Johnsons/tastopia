import { forwardRef } from "react";
import { TextInput, TextInputProps } from "react-native";

type CustomizedInputProps = TextInputProps;

export const Input = forwardRef<TextInput, CustomizedInputProps>((props, ref) => {
  return (
    <TextInput
      {...props}
      ref={ref}
      className={`rounded-lg border ${props.className}`}
    />
  );
});

export default Input;
