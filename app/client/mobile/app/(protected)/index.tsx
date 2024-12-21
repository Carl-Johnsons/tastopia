import Recipe from "@/components/common/Recipe";
import React from "react";
import { Text, SafeAreaView, View } from "react-native";

const Community = () => {
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
      </View>
    </SafeAreaView>
  );
};

export default Community;
