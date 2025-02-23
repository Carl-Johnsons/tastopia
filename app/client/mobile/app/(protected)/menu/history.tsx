import Content from "@/components/screen/history/Content";
import Header from "@/components/screen/history/Header";
import { View } from "react-native";

export default function History() {
  return (
    <View className="gap-3 bg-white_black200">
      <Header/>
      <Content/>
    </View>
  );
}
