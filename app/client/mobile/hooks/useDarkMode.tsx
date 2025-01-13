import { useColorScheme } from "nativewind";

const useDarkMode = () => {
  const { colorScheme } = useColorScheme();
  return colorScheme === "dark";
};

export default useDarkMode;
