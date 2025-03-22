import { ColorScheme } from "@/types/theme";

type ThemeObject = {
  value: ColorScheme;
  label: string;
  icon: string;
};

export const themes: ThemeObject[] = [
  { value: "light", label: "Light", icon: "/assets/icons/sun.svg" },
  { value: "dark", label: "Dark", icon: "/assets/icons/moon.svg" },
  { value: "system", label: "System", icon: "/assets/icons/computer.svg" }
];
