import React from "react";
import {
  ActivityIndicator,
  StyleProp,
  Text,
  TextStyle,
  View,
  ViewStyle
} from "react-native";

import styles from "./Loader.style";
import { globalStyles } from "../GlobalStyles";

type LoaderProps = {
  useFor?: "component" | "form";
  size?: "small" | "large";
  loaderStyle?: StyleProp<ViewStyle>;
  loaderColor?: string;
};

export default function Loader({ useFor, loaderStyle, loaderColor, size }: LoaderProps) {
  return useFor === "form" ? (
    <View style={[styles.container, loaderStyle]}>
      <ActivityIndicator
        size={size}
        color={loaderColor || globalStyles.color.primary}
      />
    </View>
  ) : (
    <View style={[styles.container, styles.component, loaderStyle]}>
      <ActivityIndicator
        size={size}
        color={loaderColor || globalStyles.color.primary}
      />
    </View>
  );
}
