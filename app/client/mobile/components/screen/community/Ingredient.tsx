import React from "react";
import { StyleSheet, Text, View } from "react-native";

type IngredientProps = {
  ingredient: string;
};
const Ingredient = ({ ingredient }: IngredientProps) => {
  return (
    <View className='gap-2'>
      <Text>{ingredient}</Text>
      <View className='h-[1px] w-full bg-gray-400'></View>
    </View>
  );
};

export default Ingredient;
