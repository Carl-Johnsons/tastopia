import { Platform } from "react-native";

/**
 * Returns the right value based on the color scheme provided.
 *
 * @param colorScheme The current color scheme.
 * @param lightValue The value to return if the color scheme is light.
 * @param darkValue The value to return if the color scheme is dark.
 * @returns The value based on the color scheme, either `lightValue` or
 *     `darkValue`.
 */
export const getColorSchemeValue = <L, D>(
  colorScheme: string,
  lightValue: L,
  darkValue: D
): L | D => {
  return colorScheme === "dark" ? darkValue : lightValue;
};

/**
 * Returns the right value based on the device's operating system.
 *
 * @param iosValue The value to return if the device is running iOS.
 * @param androidValue The value to return if the device is running Android.
 * @returns The value based on the operating system, either `iosValue` or
 *     `androidValue`.
 */
export const o = <I, A>(iosValue: I, androidValue: A): I | A => {
  return Platform.OS === "android" ? androidValue : iosValue;
};
