export type ColorScheme = "light" | "dark" | "system";
export type ChangeColorFunction = <L, D>(lightValue: L, darkValue: D) => L | D;

export type UseColorizerResult = {
  /** Return the proper value based on the color scheme. */
  c: ChangeColorFunction;
};

export type UseColorizerFn = () => UseColorizerResult;
