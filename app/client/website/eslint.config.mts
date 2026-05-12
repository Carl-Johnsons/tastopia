import js from "@eslint/js";
import nextVitals from "eslint-config-next/core-web-vitals";
import tailwind from "eslint-plugin-tailwindcss";
import prettier from "eslint-config-prettier";
import cypress from "eslint-plugin-cypress";

export default [
  js.configs.recommended,
  ...nextVitals,
  ...tailwind.configs["flat/recommended"],
  prettier,
  cypress.configs.recommended,

  {
    rules: {
      camelcase: "off",
      "no-unused-vars": "warn",
    },
  },
];
