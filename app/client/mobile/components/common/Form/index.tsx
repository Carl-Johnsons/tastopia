import React from "react";
import { View, ViewProps, StyleSheet, ViewStyle, StyleProp } from "react-native";

interface FormTypes extends ViewProps {
  id?: string;
  style?: StyleProp<ViewStyle>;
  children: React.ReactNode;
}

const Form = ({ id, style, children, ...restProps }: FormTypes) => {
  return (
    <View
      style={style}
      {...restProps}
    >
      {children}
    </View>
  );
};

const styles = StyleSheet.create({});

export default Form;
