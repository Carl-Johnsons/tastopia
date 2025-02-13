import Header from "@/components/screen/profile/Header";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { useLocalSearchParams } from "expo-router";
import { SafeAreaView } from "react-native-safe-area-context";

const Profile = () => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const { id } = useLocalSearchParams();

  console.log("in profile detail", id);

  return (
    <SafeAreaView
      style={{ backgroundColor: c(white.DEFAULT, black[100]), height: "100%" }}
    >
      <Header />
    </SafeAreaView>
  );
};

export default Profile;
