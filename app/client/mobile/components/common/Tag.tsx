import { StyleSheet, Text, TouchableWithoutFeedback, View } from "react-native";
import React from "react";
import { Image } from "expo-image";
import { AntDesign } from "@expo/vector-icons";
import { globalStyles } from "./GlobalStyles";
import useDarkMode from "@/hooks/useDarkMode";

type TagProps = {
  handleOnPress: () => void;
};

const Tag = ({ handleOnPress }: TagProps) => {
  const isDarkMode = useDarkMode();
  return (
    <TouchableWithoutFeedback onPress={handleOnPress}>
      <View className='flex-row self-start rounded-3xl bg-primary px-1 py-2'>
        <View className='flex-center flex-row gap-2 pr-2'>
          <Image
            style={{ width: 20, height: 20, borderRadius: 100 }}
            source={
              "https://img.freepik.com/premium-vector/cherry-blossom-icon-vector-sakura-illustration_197792-811.jpg"
            }
          />
          <Text className='text-white_black text-center font-semibold'>PORK</Text>
        </View>

        <View className='flex-center'>
          <AntDesign
            name='closecircleo'
            size={16}
            color={isDarkMode ? globalStyles.color.dark : globalStyles.color.light}
          />
        </View>
      </View>
    </TouchableWithoutFeedback>
  );
};

export default Tag;

const styles = StyleSheet.create({});
