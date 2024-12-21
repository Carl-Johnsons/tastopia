import React from "react";
import { View, ViewProps } from "react-native";

interface FromGroupProps extends ViewProps {
  children: React.ReactNode;
}

const FormGroup = ({ children, ...restProps }: FromGroupProps) => {
  return <View style={[styles.container, restProps.style]}>{children}</View>;
};

export default FormGroup;

const styles = {
  container: {
    flexDirection: "column" as "column"
  }
};
