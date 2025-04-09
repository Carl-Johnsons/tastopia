import { ActivityIndicator, SafeAreaView, View } from "react-native";
import { globalStyles } from "./GlobalStyles";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";

const LoadingFullScreen = () => {
  const { c } = useColorizer();
  const { black, white } = colors;
  return (
    <SafeAreaView
      style={{
        backgroundColor: c(white.DEFAULT, black[100]),
        height: "100%",
        width: "100%"
      }}
    >
      <View className='flex-center size-full'>
        <ActivityIndicator
          color={globalStyles.color.primary}
          size={"large"}
        />
      </View>
    </SafeAreaView>
  );
};

export default LoadingFullScreen;
