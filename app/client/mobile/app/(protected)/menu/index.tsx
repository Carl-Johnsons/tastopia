import LogoutButton from "@/components/LogoutButton";
import React from "react";
import { Text, View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";

const Menu = () => {
  return (
    <SafeAreaView>
      <View>
        <Text>Menu</Text>
      </View>
      <LogoutButton/>
    </SafeAreaView>
  );
};

export default Menu;
