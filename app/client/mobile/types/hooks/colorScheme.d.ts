type ColorScheme = "light" | "dark" | "system";
type ChangeColorFunction = <L, D>(lightValue: L, darkValue: D) => L | D;

type UseColorizerResult = {
  /** Return the proper value based on the color scheme. */
  c: ChangeColorFunction;
};

type UseColorizerFn = (colorScheme?: string) => UseColorizerResult;
