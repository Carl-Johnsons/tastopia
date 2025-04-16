import NotificationList from "@/components/screen/notification/NotificationList";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
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
      <NotificationList />
    </SafeAreaView>
  );
};

export default Notification;
