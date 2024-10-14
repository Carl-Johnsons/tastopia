import { Platform, useColorScheme } from "react-native";

/**
 * Returns the right value based on the color scheme of the device.
 *
 * @param lightValue The value to return if the color scheme is light.
 * @param darkValue The value to return if the color scheme is dark.
 * @returns The value based on the color scheme, either `lightValue` or `darkValue`.
 */
export const useColorModeValue = <L, D>(lightValue: L, darkValue: D): L | D => {
  const colorScheme = useColorScheme();
  return colorScheme === "dark" ? darkValue : lightValue;
};

/**
 * Returns the right value based on the device's operating system.
 *
 * @param iosValue The value to return if the device is running iOS.
 * @param androidValue The value to return if the device is running Android.
 * @returns The value based on the operating system, either `iosValue` or `androidValue`.
 */
export const useOsValue = <I, A>(iosValue: I, androidValue: A): I | A => {
  return Platform.OS === "ios" ? iosValue : androidValue;
};
