"use client";

import { useTheme } from "@/context/ThemeProvider";
import { ChangeColorFunction, UseColorizerFn } from "@/types/theme";
import { useCallback } from "react";

/**
 * Provide a function that returns the right value based on the current color scheme.
 *
 * @param colorScheme - An alternative to the default color scheme. Passing this is not recommended.
 */
const useColorizer: UseColorizerFn = () => {
  const { mode } = useTheme();

  const c: ChangeColorFunction = useCallback(
    (lightValue, darkValue) => {
      return mode === "dark" ? darkValue : lightValue;
    },
    [mode]
  );

  return { c };
};

export default useColorizer;
