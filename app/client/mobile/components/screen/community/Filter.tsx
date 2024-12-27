import React, { useState } from "react";
import { Image, Text, TouchableWithoutFeedback, View } from "react-native";

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
  handleSelect: (key: string) => void;
};

const Filter = ({ handleSelect }: FilterProps) => {
  const [itemSelected, setItemSelected] = useState<string>(filterData[0].value);

  const handleSelectItem = (value: string) => {
    setItemSelected(value);
    handleSelect(value);
  };

  return (
    <View className='flex-row gap-3 flex-center'>
      {filterData?.map(item => {
        return (
          <TouchableWithoutFeedback
            onPress={() => handleSelectItem(item.value)}
            key={item.value}
          >
            <View
              className={`flex-center w-[60px] rounded-full border-2 border-primary py-2 ${itemSelected === item.value ? "bg-primary" : "bg-white"}`}
            >
              <View
                className={`rounded-full p-[0.5px] ${itemSelected === item.value ? "bg-white" : ""}`}
                style={{
                  elevation: itemSelected === item.value ? 5 : 0,
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
                />
              </View>
              <Text
                className={`body-medium my-2 py-2 text-center ${itemSelected === item.value ? "text-white_black" : "text-black_white"}`}
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

export default Filter;
