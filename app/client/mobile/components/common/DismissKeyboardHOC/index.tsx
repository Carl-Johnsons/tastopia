import React, { ReactNode } from "react";
import { TouchableWithoutFeedback, Keyboard } from "react-native";

interface DismissKeyboardHOCProps {
  children: ReactNode;
}

const DismissKeyboardHOC = ({ children }: DismissKeyboardHOCProps) => {
  return (
    <TouchableWithoutFeedback
      onPress={Keyboard.dismiss}
      accessible={false}
    >
      {children}
    </TouchableWithoutFeedback>
  );
};

export default DismissKeyboardHOC;
