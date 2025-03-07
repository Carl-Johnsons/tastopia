export type ColorShades = {
  DEFAULT: string;
  100?: string;
  200?: string;
  300?: string;
  400?: string;
  500?: string;
  600?: string;
  700?: string;
};

export type Colors = {
  primary: string;
  secondary: ColorShades;
  black: ColorShades;
  white: ColorShades;
  gray: ColorShades;
};

export const colors: Colors = {
  primary: "#FE724C",
  secondary: { DEFAULT: "#FF9C01", 100: "#FF9001", 200: "#FF8E01" },
  black: {
    DEFAULT: "#000000",
    100: "#1E1E1E",
    200: "#191919",
    300: "#0D0D0D",
  },
  white: { DEFAULT: "#FFFFFF", 100: "#EFEFEF" },
  gray: {
    DEFAULT: "#C4C4C4",
    100: "#F3F4F6",
    200: "#D9D9D9",
    300: "#C4C4C4",
    400: "#C4C7D0",
    500: "#9CA3AF",
    600: "#626C70",
    700: "#5B5B5E",
  },
};
