import { Text, TouchableOpacity } from "react-native";
import React from "react";

type CustomButtonProps = {
  title: string;
  handlePress?: () => void;
  containerStyles?: string;
  textStyles?: string;
  isLoading?: boolean;
};

const CustomButton = ({
  title,
  handlePress,
  containerStyles,
  textStyles,
  isLoading
}: CustomButtonProps) => {
  return (
    <TouchableOpacity
      className={`min-h-[62px] items-center justify-center rounded-xl bg-secondary ${containerStyles} ${
        isLoading ? "opacity-50" : ""
      }`}
      onPress={handlePress}
    >
      <Text className={`font-psemibold text-lg text-primary ${textStyles}`}>{title}</Text>
    </TouchableOpacity>
  );
};

export default CustomButton;
