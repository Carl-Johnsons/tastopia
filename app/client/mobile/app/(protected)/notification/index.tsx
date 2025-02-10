import Header from "@/components/screen/notificaiton/Header";
import NotificationList from "@/components/screen/notificaiton/NotificationList";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { Text, View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";

const Notification = () => {
  const { c } = useColorizer();
  const { black, white } = colors;

  return (
    <SafeAreaView
      style={{
        backgroundColor: c(white.DEFAULT, black[100]),
        height: "100%"
      }}
    >
      <View>
        <Header />
      </View>
      <View>
        <NotificationList />
      </View>
    </SafeAreaView>
  );
};

export default Notification;
