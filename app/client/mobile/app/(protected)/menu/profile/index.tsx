import Header from "@/components/screen/profile/Header";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { SafeAreaView } from "react-native-safe-area-context";

const Profile = () => {
  const { c } = useColorizer();
  const { black, white } = colors;

  return (
    <SafeAreaView
      style={{ backgroundColor: c(white.DEFAULT, black[100]), height: "100%" }}
    >
      <Header/>
    </SafeAreaView>
  );
};

export default Profile;
