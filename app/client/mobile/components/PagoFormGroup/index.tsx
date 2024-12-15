import React from "react";
import { View, ViewProps } from "react-native";

interface PagoFromGroupProps extends ViewProps {
  children: React.ReactNode;
}

const PagoFormGroup = ({ children, ...restProps }: PagoFromGroupProps) => {
  return <View style={[styles.container, restProps.style]}>{children}</View>;
};

export default PagoFormGroup;

const styles = {
  container: {
    flexDirection: "column" as "column"
  }
};
