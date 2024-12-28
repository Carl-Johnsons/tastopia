import { useColorScheme } from "react-native";

const useDarkMode = () => {
  const colorScheme = useColorScheme();
  return colorScheme === "dark";
};

export default useDarkMode;
