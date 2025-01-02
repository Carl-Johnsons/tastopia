import { memo } from "react";
import { View, Image, Text } from "react-native";

const Empty = memo(() => (
  <View className='flex-center h-[70%] gap-2'>
    <Image
      source={require("../../../assets/icons/noResult.png")}
      style={{ width: 130, height: 130 }}
    />
    <Text className='paragraph-medium text-center'>
      No recipes found! {"\n"}Time to create your own masterpiece!
    </Text>
  </View>
));

export default Empty;
