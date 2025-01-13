import { useColorScheme } from "nativewind";
import { useCallback } from "react";

/**
 * Provide a function that returns the right value based on the current color scheme.
 *
 * @param colorScheme - An alternative to the default color scheme. Passing this is not recommended.
 */
const useColorizer: UseColorizerFn = colorScheme => {
  const { colorScheme: defaultColorScheme } = useColorScheme();

  const c: ChangeColorFunction = useCallback(
    (lightValue, darkValue) => {
      return (colorScheme || defaultColorScheme) === "dark" ? darkValue : lightValue;
    },
    [colorScheme, defaultColorScheme]
  );

  return { c };
};

export default useColorizer;
