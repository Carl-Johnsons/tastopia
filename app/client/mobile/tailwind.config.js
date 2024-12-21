/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./app/**/*.{js,jsx,ts,tsx}", "./components/**/*.{js,jsx,ts,tsx}"],
  presets: [require("nativewind/preset")],
  theme: {
    extend: {
      colors: {
        primary: "#FE724C",
        secondary: {
          DEFAULT: "#FF9C01",
          100: "#FF9001",
          200: "#FF8E01"
        },
        black: {
          DEFAULT: "#000",
          100: "#1E1E2D",
          200: "#232533"
        },
        gray: {
          100: "#CDCDE0"
        }
      },
      fontFamily: {
        sans: ["Sofia-Pro-Regular", "sans-serif"],
        bold: ["Sofia-Pro-Bold", "sans-serif"],
        light: ["Sofia-Pro-Light", "sans-serif"],
        medium: ["Sofia-Pro-Medium", "sans-serif"],
        semibold: ["Sofia-Pro-Semi-Bold", "sans-serif"],
        black: ["Sofia-Pro-Black", "sans-serif"],
        extralight: ["Sofia-Pro-ExtraLight", "sans-serif"],
        ultralight: ["Sofia-Pro-UltraLight", "sans-serif"],
        italic: ["Sofia-Pro-Regular-Italic", "sans-serif"],
        "bold-italic": ["Sofia-Pro-Bold-Italic", "sans-serif"],
        "light-italic": ["Sofia-Pro-Light-Italic", "sans-serif"],
        "medium-italic": ["Sofia-Pro-Medium-Italic", "sans-serif"],
        "semibold-italic": ["Sofia-Pro-Semi-Bold-Italic", "sans-serif"],
        "black-italic": ["Sofia-Pro-Black-Italic", "sans-serif"],
        "extralight-italic": ["Sofia-Pro-ExtraLight-Italic", "sans-serif"],
        "ultralight-italic": ["Sofia-Pro-UltraLight-Italic", "sans-serif"]
      }
    }
  },
  plugins: []
};
