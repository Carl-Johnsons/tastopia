import Recipe from "@/components/common/Recipe";
import i18next from "i18next";
import React from "react";
import { Text, View, Button } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";

const Community = () => {
  const toggleLanguage = () => {
    const currentLang = i18next.language;
    const newLang = currentLang === "en" ? "vi" : "en";
    i18next.changeLanguage(newLang);
  };
  return (
    <SafeAreaView>
      <View className='size-full'>
        <Recipe
          title='Chicken Hawaiian'
          description='Chicken, Cheese and pineapple'
          imageUrl='YOUR_IMAGE_URL'
          votes={777}
          comments={777}
          onPress={() => console.log("Card pressed")}
        />

        <Button
          title={"Toggle language"}
          onPress={() => toggleLanguage()}
        ></Button>
      </View>
    </SafeAreaView>
  );
};

export default Community;
