declare function useColorScheme(): {
  /** Sets the color scheme to a specified value. */
  setColorScheme(scheme: "light" | "dark" | "system"): void;
  /** The current color scheme. */
  colorScheme: "light" | "dark" | "system";
  /** Toggle between "light" and "dark". */
  toggleColorScheme: () => void;
};

