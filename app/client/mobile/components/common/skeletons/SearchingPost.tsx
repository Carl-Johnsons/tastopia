import { View } from "react-native";
import React from "react";
import { Skeleton } from "@rneui/themed";

const SearchingPost = () => {
  return (
    <View className='flex-row'>
      <Skeleton
        circle
        width={50}
        height={50}
        style={{ marginRight: 10 }}
      />
      <View className='gap-2'>
        <Skeleton
          width={180}
          height={20}
        />
        <Skeleton
          width={150}
          height={20}
        />
      </View>
    </View>
  );
};

export default SearchingPost;
