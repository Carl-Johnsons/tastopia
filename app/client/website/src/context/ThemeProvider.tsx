"use client";

import { ColorScheme } from "@/types/theme";
import React, {
  createContext,
  useContext,
  useState,
  useEffect,
  useCallback,
  useMemo,
  SetStateAction,
  Dispatch
} from "react";

interface ThemeContextProps {
  mode: ColorScheme;
  setMode: Dispatch<SetStateAction<ColorScheme>>;
}

const ThemeContext = createContext<ThemeContextProps | undefined>(undefined);

export function ThemeProvider({ children }: { readonly children: React.ReactNode }) {
  const [mode, setMode] = useState<ColorScheme>("light");

  const value = useMemo(() => {
    return {
      mode,
      setMode
    };
  }, [mode]);

  const handleThemeChange = useCallback(() => {
    if (
      localStorage.theme === "dark" ||
      (!("theme" in localStorage) &&
        window.matchMedia("(prefers-color-schema: dark)").matches)
    ) {
      setMode("dark");
      document.documentElement.classList.add("dark");
    } else if (
      localStorage.theme === "light" ||
      (!("theme" in localStorage) &&
        window.matchMedia("(prefers-color-schema: light)").matches)
    ) {
      setMode("light");
      document.documentElement.classList.remove("dark");
    }
  }, []);

  useEffect(() => {
    handleThemeChange();
  }, [mode, handleThemeChange]);

  return <ThemeContext.Provider value={value}>{children}</ThemeContext.Provider>;
}

export function useTheme() {
  const context = useContext(ThemeContext);

  if (context === undefined) {
    throw new Error("useTheme must be use within a ThemeProvider");
  }

  return context;
}
