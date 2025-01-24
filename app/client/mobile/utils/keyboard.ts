import { GestureResponderEvent, Keyboard } from "react-native";

/**
 * Dismiss the keyboard.
 *
 * @param event - The event object, preferably from a Pressable element.
 */
export const dismissKeyboard = (event: GestureResponderEvent, enablePropagation?: boolean) => {
  if (!enablePropagation) {
    event.stopPropagation();
  }

  Keyboard.dismiss();
};
