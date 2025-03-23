export type ColorShades = {
  DEFAULT: string;
  100?: string;
  200?: string;
  300?: string;
  400?: string;
  450?: string;
  500?: string;
  600?: string;
  700?: string;
  800?: string;
  850?: string;
  900?: string;
};

export type Colors = {
  primary: string;
  secondary: ColorShades;
  black: ColorShades;
  white: ColorShades;
  gray: ColorShades;
  green: ColorShades;
  red: ColorShades;
};

export const colors: Colors = {
  primary: "#FE724C",
  secondary: { DEFAULT: "#EB5B00", 100: "#FF9001", 200: "#FF8E01" },
  black: {
    DEFAULT: "#000000",
    100: "#1E1E1E",
    200: "#191919",
    300: "#0D0D0D",
    400: "#1E201E",
    450: "#2a3b42",
    500: "#101012"
  },
  white: {
    DEFAULT: "#FFFFFF",
    100: "#EFEFEF",
    600: "#e9edf7",
    700: "#DCE3F1",
    800: "#F4F6F8",
    850: "#FDFDFD",
    900: "#FFFFFF"
  },
  gray: {
    DEFAULT: "#C4C4C4",
    100: "#F3F4F6",
    200: "#D9D9D9",
    300: "#C4C4C4",
    400: "#C4C7D0",
    500: "#9CA3AF",
    600: "#626C70",
    700: "#5B5B5E"
  },
  green: {
    DEFAULT: "#0B986D"
  },
  red: {
    DEFAULT: "#E84646"
  }
};
