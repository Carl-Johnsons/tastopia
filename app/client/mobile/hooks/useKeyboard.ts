import { useEffect, useState } from "react";
import { Keyboard } from "react-native";

interface UseKeyboardResult {
  /** The device's keyboard current height  */
  keyboardHeight: number;
}

/**
 * Returns the height of the keyboard.
 */
export const useKeyboard = (): UseKeyboardResult => {
  const [keyboardHeight, setKeyboardHeight] = useState<number>(0);

  useEffect(() => {
    const keyboardDidShowListener = Keyboard.addListener("keyboardWillShow", e => {
      setKeyboardHeight(e.endCoordinates.height);
    });

    const keyboardDidHideListener = Keyboard.addListener("keyboardWillHide", () => {
      setKeyboardHeight(0);
    });

    return () => {
      keyboardDidShowListener.remove();
      keyboardDidHideListener.remove();
    };
  }, []);

  return { keyboardHeight };
};
