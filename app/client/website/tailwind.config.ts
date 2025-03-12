/** @type {import('tailwindcss').Config} */

import { colors } from "./src/constants/colors";

const FONT_PRIMARY = {
  sans: ["Sofia Pro Regular", "sans-serif"],
  bold: ["Sofia Pro Bold", "sans-serif"],
  light: ["Sofia Pro Light", "sans-serif"],
  medium: ["Sofia Pro Medium", "sans-serif"],
  semibold: ["Sofia Pro Semi Bold", "sans-serif"],
  black: ["Sofia Pro Black", "sans-serif"],
  extralight: ["Sofia Pro ExtraLight", "sans-serif"],
  ultralight: ["Sofia Pro UltraLight", "sans-serif"],
  italic: ["Sofia Pro Regular Italic", "sans-serif"],
  "bold-italic": ["Sofia Pro Bold Italic", "sans-serif"],
  "light-italic": ["Sofia Pro Light Italic", "sans-serif"],
  "medium-italic": ["Sofia Pro Medium Italic", "sans-serif"],
  "semibold-italic": ["Sofia Pro Semi Bold Italic", "sans-serif"],
  "black-italic": ["Sofia Pro Black Italic", "sans-serif"],
  "extralight-italic": ["Sofia Pro ExtraLight Italic", "sans-serif"],
  "ultralight-italic": ["Sofia Pro UltraLight Italic", "sans-serif"],
};

const FONT_SECONDARY = {
  "secondary-black": ["Helvetica Neue Black", "sans-serif"],
  "secondary-black-italic": ["Helvetica Neue Black Italic", "sans-serif"],
  "secondary-bold": ["Helvetica Neue Bold", "sans-serif"],
  "secondary-bold-italic": ["Helvetica Neue Bold Italic", "sans-serif"],
  "secondary-heavy": ["Helvetica Neue Heavy", "sans-serif"],
  "secondary-heavy-italic": ["Helvetica Neue Heavy Italic", "sans-serif"],
  "secondary-italic": ["Helvetica Neue Italic", "sans-serif"],
  "secondary-light": ["Helvetica Neue Light", "sans-serif"],
  "secondary-light-italic": ["Helvetica Neue Light Italic", "sans-serif"],
  "secondary-medium": ["Helvetica Neue Medium", "sans-serif"],
  "secondary-medium-italic": ["Helvetica Neue Medium Italic", "sans-serif"],
  "secondary-semibold": ["Helvetica Neue Semi Bold", "sans-serif"],
  "secondary-roman": ["Helvetica Neue Roman", "sans-serif"],
  "secondary-thin": ["Helvetica Neue Thin", "sans-serif"],
  "secondary-thin-italic": ["Helvetica Neue Thin Italic", "sans-serif"],
  "secondary-ultralight": ["Helvetica Neue UltraLight", "sans-serif"],
  "secondary-ultralight-italic": ["Helvetica Neue UltraLight Italic", "sans-serif"],
};

const fontFamily = { ...FONT_PRIMARY, ...FONT_SECONDARY };

module.exports = {
  darkMode: ["class"],
  content: [
    "./pages/**/*.{ts,tsx}",
    "./components/**/*.{ts,tsx}",
    "./app/**/*.{ts,tsx}",
    "./src/**/*.{ts,tsx}",
    "./src/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    container: {
      center: true,
      padding: "2rem",
    },
    extend: {
      screen: {
        sm: "600px",
      },
      colors,
      fontFamily,
      boxShadow: {
        "light-100":
          "0px 12px 20px 0px rgba(184, 184, 184, 0.03), 0px 6px 12px 0px rgba(184, 184, 184, 0.02), 0px 2px 4px 0px rgba(184, 184, 184, 0.03)",
        "light-200": "10px 10px 20px 0px rgba(218, 213, 213, 0.10)",
        "light-300": "-10px 10px 20px 0px rgba(218, 213, 213, 0.10)",
        "dark-100": "0px 2px 10px 0px rgba(46, 52, 56, 0.10)",
        "dark-200": "2px 0px 20px 0px rgba(39, 36, 36, 0.04)",
      },
      backgroundImage: {
        "auth-dark": "url('/assets/images/auth-dark.png')",
        "auth-light": "url('/assets/images/auth-light.png')",
      },
      screens: {
        xs: "420px",
      },
      keyframes: {
        "accordion-down": {
          from: {
            height: 0,
          },
          to: {
            height: "var(--radix-accordion-content-height)",
          },
        },
        "accordion-up": {
          from: {
            height: "var(--radix-accordion-content-height)",
          },
          to: {
            height: 0,
          },
        },
      },
      animation: {
        "accordion-down": "accordion-down 0.2s ease-out",
        "accordion-up": "accordion-up 0.2s ease-out",
      },
      borderRadius: {
        lg: "var(--radius)",
        md: "calc(var(--radius) - 2px)",
        sm: "calc(var(--radius) - 4px)",
      },
    },
  },
  plugins: [require("tailwindcss-animate"), require("@tailwindcss/typography")],
};
