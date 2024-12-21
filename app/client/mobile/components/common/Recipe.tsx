// components/DishCard.tsx
import React from "react";
import { View, Text, Image, TouchableOpacity } from "react-native";
import { MaterialCommunityIcons } from "@expo/vector-icons";

interface RecipeProps {
  title: string;
  description: string;
  imageUrl: string;
  votes: number;
  comments: number;
  onPress?: () => void;
}

const Recipe = ({
  title,
  description,
  imageUrl,
  votes,
  comments,
  onPress
}: RecipeProps) => {
  return (
    <TouchableOpacity
      className='rounded-lg'
      onPress={onPress}
    >
      <View className='flex flex-row items-center justify-center'>
        <View>
          <Image
            source={{ uri: imageUrl }}
            className='w-full h-48 rounded-t-lg'
          />
          <Text>quoczuong</Text>
        </View>

        <View>
          <Text>menu</Text>
        </View>
      </View>

      <View className='p-4'>
        <Text className='text-2xl font-bold text-gray-800'>{title}</Text>
        <Text className='mt-1 text-gray-600'>{description}</Text>

        <View className='flex-row mt-4 space-x-4'>
          <View className='flex-row items-center px-4 py-2 bg-gray-100 rounded-full'>
            <MaterialCommunityIcons
              name='arrow-up-down'
              size={20}
              color='gray'
            />
            <Text className='ml-2 text-gray-700'>{votes}</Text>
          </View>

          <View className='flex-row items-center px-4 py-2 bg-gray-100 rounded-full'>
            <MaterialCommunityIcons
              name='comment-outline'
              size={20}
              color='gray'
            />
            <Text className='ml-2 text-gray-700'>{comments}</Text>
          </View>
        </View>
      </View>
    </TouchableOpacity>
  );
};

export default Recipe;
