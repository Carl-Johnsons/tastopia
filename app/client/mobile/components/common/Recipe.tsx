// components/DishCard.tsx
import React from "react";
import {
  View,
  Text,
  Image,
  TouchableOpacity,
  Button,
  TouchableWithoutFeedback,
  TouchableHighlight
} from "react-native";
import { MaterialCommunityIcons, Feather } from "@expo/vector-icons";
import Vote from "./Vote";

interface RecipeProps {
  id: string;
  title: string;
  description: string;
  imageUrl: string;
  username: string;
  avatar: string;
  votes: number;
  comments: number;
  onPress?: () => void;
}

const Recipe = ({
  id,
  title,
  description,
  imageUrl,
  username,
  avatar,
  votes,
  comments,
  onPress
}: RecipeProps) => {
  const handleTouchMenu = () => {};
  return (
    <TouchableWithoutFeedback onPress={onPress}>
      <View className='bg-white_black rounded-3xl pb-4'>
        <View className='flex-between flex-row px-4 py-2'>
          {username && avatar && (
            <TouchableWithoutFeedback
              onPress={() => {
                console.log("go to user detail");
              }}
            >
              <View className='flex-center flex-row gap-2'>
                <Image
                  source={require("../../assets/images/logo-icon.png")}
                  className='size-[30px] rounded-full'
                />
                <Text>Vuong</Text>
              </View>
            </TouchableWithoutFeedback>
          )}

          <TouchableWithoutFeedback onPress={handleTouchMenu}>
            <Feather
              name='more-horizontal'
              size={24}
              color='black'
            />
          </TouchableWithoutFeedback>
        </View>
        <View className='flex gap-3'>
          <Image
            source={require("../../assets/images/recipe.png")}
            className='h=[350px] w-full'
          />

          <View className='gap-3 px-4'>
            <View className='gap-1'>
              <Text className='font-bold text-2xl'>Chicken Hawaiian</Text>
              <Text className=''>Chicken, Cheese and pineapple</Text>
            </View>

            <View className='flex-start flex-row'>
              <Vote />
            </View>
          </View>
        </View>
      </View>
    </TouchableWithoutFeedback>
  );
};

export default Recipe;
