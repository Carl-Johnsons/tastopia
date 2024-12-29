import { StyleSheet, Text, TouchableWithoutFeedback, View } from "react-native";
import React, { useState } from "react";
import { Image } from "expo-image";
import { globalStyles } from "./GlobalStyles";
import { abbreviateNumber } from "@/utils/format";

type UserProps = {
  id: string;
  avtUrl: string;
  displayName: string;
  username: string;
  numberOfRecipe: number;
  isFollowing: boolean;
};

const User = ({
  id,
  avtUrl,
  displayName,
  username,
  numberOfRecipe,
  isFollowing
}: UserProps) => {
  const [followed, setIsFollowed] = useState<boolean>(isFollowing);

  const handleFollowUnFollow = () => {
    setIsFollowed(prev => !prev);
    // call api later
  };

  return (
    <TouchableWithoutFeedback
      onPress={() => {
        console.log("press user card");
      }}
    >
      <View className='flex-row items-start justify-between gap-16'>
        <View className='flex-row flex-1 gap-2'>
          <Image
            source={avtUrl}
            style={{
              width: 36,
              height: 36,
              borderRadius: 100
            }}
          />
          <View>
            <Text
              numberOfLines={1}
              ellipsizeMode='tail'
              className='base-medium'
            >
              {displayName}
            </Text>
            <Text
              numberOfLines={1}
              ellipsizeMode='tail'
              className='paragraph-medium'
            >
              @{username}
            </Text>
            <Text className='body-regular'>
              {abbreviateNumber(numberOfRecipe)}{" "}
              {numberOfRecipe === 1 ? "recipe" : "recipes"}
            </Text>
          </View>
        </View>
        <View>
          <TouchableWithoutFeedback onPress={handleFollowUnFollow}>
            <View className='px-6 py-2 rounded-3xl bg-primary'>
              <Text className='text-center paragraph-bold text-white_black'>
                {followed ? "Follow" : "Unfollow"}
              </Text>
            </View>
          </TouchableWithoutFeedback>
        </View>
      </View>
    </TouchableWithoutFeedback>
  );
};

export default User;

const styles = StyleSheet.create({});
