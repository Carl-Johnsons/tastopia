// components/DishCard.tsx
import React from "react";
import { View, Text, Image, TouchableWithoutFeedback, Touchable } from "react-native";
import { Ionicons, Feather } from "@expo/vector-icons";
import Vote from "./Vote";

const Recipe = ({
  id,
  authorId,
  recipeImgUrl,
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
                <Text className='paragraph-medium'>{authorDisplayName}</Text>
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
            source={{ uri: recipeImgUrl }}
            style={{ width: "100%", height: 240, borderRadius: 10 }}
          />

          <View className='gap-3 px-4'>
            <View className='gap-1'>
              <Text className='text-2xl font-bold'>{title}</Text>
              <Text className=''>{description}</Text>
            </View>

            <View className='flex-row gap-2 flex-start'>
              <Vote voteDiff={voteDiff} />

              <TouchableWithoutFeedback>
                <View className='flex-center flex-row gap-2 rounded-3xl border-[0.5px] border-gray-300 px-3 py-2.5'>
                  <Ionicons
                    name='chatbubble-outline'
                    size={20}
                    color='black'
                  />
                  <Text>{numberOfComment}</Text>
                </View>
              </TouchableWithoutFeedback>
            </View>
          </View>
        </View>
      </View>
    </TouchableWithoutFeedback>
  );
};

export default Recipe;
