import { ActivityIndicator, StyleSheet, Text, View } from "react-native";
import React from "react";
import { globalStyles } from "./GlobalStyles";

const Loading = () => {
  return (
    <View className='bg-white_black100 flex-1 items-center justify-center'>
      <ActivityIndicator
        size='large'
        color={globalStyles.color.primary}
      />
    </View>
  );
};

export default Loading;

const styles = StyleSheet.create({});
