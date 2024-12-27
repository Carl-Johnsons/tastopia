// components/DishCard.tsx
import React from "react";
import {
  View,
  Text,
  Image,
  TouchableOpacity,
  Button,
  TouchableWithoutFeedback,
  TouchableHighlight,
  TouchableNativeFeedback
} from "react-native";
import { MaterialCommunityIcons, Feather } from "@expo/vector-icons";
import Vote from "./Vote";

const Recipe = ({
  id,
  authorId,
  title,
  description,
  authorDisplayName,
  authorAvtUrl,
  voteDiff,
  numberOfComment
}: RecipeType) => {
  const handleTouchMenu = () => {};
  const handleOnPress = () => {};

  return (
    <TouchableWithoutFeedback onPress={handleOnPress}>
      <View className='pb-4 bg-white_black rounded-3xl'>
        <View className='flex-row px-4 py-2 flex-between'>
          {authorId && authorDisplayName && authorAvtUrl && (
            <TouchableWithoutFeedback
              onPress={() => {
                console.log("go to user detail");
              }}
            >
              <View className='flex-row gap-2 flex-center'>
                <Image
                  source={{ uri: authorAvtUrl }}
                  className='size-[30px] rounded-full'
                />
                <Text>{authorDisplayName}</Text>
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
              <Text className='text-2xl font-bold'>{title}</Text>
              <Text className=''>{description}</Text>
            </View>

            <View className='flex-row flex-start'>
              <Vote />
            </View>
          </View>
        </View>
      </View>
    </TouchableWithoutFeedback>
  );
};

export default Recipe;
