import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { Image } from "expo-image";
import React, { memo, useState } from "react";
import { Text, TouchableWithoutFeedback, View } from "react-native";

const filterData = [
  {
    value: "All",
    imageUrl: require("../../../../mobile/assets/images/all.png")
  },
  {
    value: "Noodles",
    imageUrl: require("../../../../mobile/assets/images/noodles.png")
  },
  {
    value: "Spice",
    imageUrl: require("../../../../mobile/assets/images/spice.png")
  },
  {
    value: "BBQ",
    imageUrl: require("../../../../mobile/assets/images/bbq.png")
  },
  {
    value: "Seafood",
    imageUrl: require("../../../../mobile/assets/images/seafood.png")
  }
];

type FilterItem = {
  imageUrl: string;
  value: string;
};

type FilterProps = {
  filterSelected: string;
  handleSelect: (key: string) => void;
};

const Filter = ({ filterSelected, handleSelect }: FilterProps) => {
  const { c } = useColorizer();
  const { black, white } = colors;

  const handleSelectItem = (value: string) => {
    handleSelect(value);
  };

  return (
    <View className='flex-center flex-row gap-3'>
      {filterData?.map(item => {
        const isSelected = filterSelected === item.value;
        return (
          <TouchableWithoutFeedback
            onPress={() => handleSelectItem(item.value)}
            key={item.value}
          >
            <View
              className={`flex-center w-[64px] rounded-full border-2 border-primary py-2 ${isSelected ? "bg-primary" : "bg-transparent"}`}
            >
              <View
                className={`rounded-full p-[0.5px] ${isSelected ? "bg-white" : ""}`}
                style={{
                  elevation: isSelected ? 5 : 0,
                  shadowColor: "#000",
                  shadowOffset: { width: 0, height: 4 },
                  shadowOpacity: 0.4,
                  shadowRadius: 2
                }}
              >
                <Image
                  source={item.imageUrl}
                  style={{
                    width: 50,
                    height: 50,
                    borderRadius: 50
                  }}
                  cachePolicy={"disk"}
                />
              </View>
              <Text
                className={`body-medium my-2 py-2 text-center ${isSelected ? "text-white_black" : "text-black_white"}`}
              >
                {item.value}
              </Text>
            </View>
          </TouchableWithoutFeedback>
        );
      })}
    </View>
  );
};

export default memo(Filter);
