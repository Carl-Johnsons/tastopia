/** @type {import('tailwindcss').Config} */
import { colors } from "./constants/colors";

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
  "ultralight-italic": ["Sofia Pro UltraLight Italic", "sans-serif"]
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
  "secondary-ultralight-italic": ["Helvetica Neue UltraLight Italic", "sans-serif"]
};

const fontFamily = { ...FONT_PRIMARY, ...FONT_SECONDARY };

module.exports = {
  content: ["./app/**/*.{js,jsx,ts,tsx}", "./components/**/*.{js,jsx,ts,tsx}"],
  presets: [require("nativewind/preset")],
  theme: { extend: { colors, fontFamily }},
  plugins: []
};
