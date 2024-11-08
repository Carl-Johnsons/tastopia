import React from "react";
import { View, ViewProps, StyleSheet, ViewStyle, StyleProp } from "react-native";

interface PagoFormTypes extends ViewProps {
  id?: string;
  style?: StyleProp<ViewStyle>;
  children: React.ReactNode;
}

const PagoForm = ({ id, style, children, ...restProps }: PagoFormTypes) => {
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

export default PagoForm;
