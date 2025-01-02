import React from "react";
import { View, Text, SafeAreaView } from "react-native";
import { useLocalSearchParams } from "expo-router";

const RecipeDetail = () => {
  const { id } = useLocalSearchParams<{ id: string }>();

  return (
    <SafeAreaView>
      <View>
        <Text>Recipe Detail Screen - ID: {id}</Text>
      </View>
    </SafeAreaView>
  );
};

export default RecipeDetail;
