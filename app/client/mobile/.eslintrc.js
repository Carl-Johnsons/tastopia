module.exports = {
  env: {
    node: true
  },
  extends: ["expo", "plugin:tailwindcss/recommended", "prettier"],
  plugins: ["prettier", "import"],
  rules: {
    quotes: ["off", "single"],
    "no-unused-vars": "warn",
    "no-custom-class": "off",
    "no-restricted-imports": [
      "error",
      {
        paths: [
          {
            name: "react-redux",
            importNames: ["useSelector", "useDispatch"],
            message: "Please use the Redux hooks provided from @/store/hooks instead."
          }
        ]
      }
    ]
  },
  overrides: [
    {
      files: ["*.ts", "*.tsx", "*.js"],
      parser: "@typescript-eslint/parser"
    }
  ],
  settings: {
    "import/parsers": {
      "@typescript-eslint/parser": [".ts", ".tsx"],
      "import/internal-regex": "^@"
    }
  }
};
