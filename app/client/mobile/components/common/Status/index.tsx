import React from "react";
import { StyleProp, StyleSheet, Text, TextStyle, View, ViewStyle } from "react-native";

import { globalStyles } from "../GlobalStyles";

interface StatusProps {
  title: string;
  color: string;
  containerStyles?: StyleProp<ViewStyle>;
  textStyles?: StyleProp<TextStyle>;
}

export default function Status({
  title,
  color,
  containerStyles,
  textStyles
}: StatusProps) {
  return (
    <View
      style={[
        styles.container,
        { backgroundColor: color },
        containerStyles && containerStyles
      ]}
    >
      <Text style={[styles.text, textStyles && textStyles]}>{title}</Text>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    backgroundColor: globalStyles.color.primary,
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 8
  },
  text: {
    color: globalStyles.color.light,
    fontWeight: "bold"
  }
});
