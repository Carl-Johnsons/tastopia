import { Image } from "expo-image";
import { memo } from "react";
import { Text, TouchableWithoutFeedback, View } from "react-native";

const filterData = [
  {
    value: "All",
    imageUrl: require("../../../../mobile/assets/images/all.webp")
  },
  {
    value: "Noodles",
    imageUrl: require("../../../../mobile/assets/images/noodles.webp")
  },
  {
    value: "Spice",
    imageUrl: require("../../../../mobile/assets/images/spice.webp")
  },
  {
    value: "BBQ",
    imageUrl: require("../../../../mobile/assets/images/bbq.webp")
  },
  {
    value: "Seafood",
    imageUrl: require("../../../../mobile/assets/images/seafood.webp")
  }
];

type FilterProps = { filterSelected: string; handleSelect: (key: string) => void };

const Filter = ({ filterSelected, handleSelect }: FilterProps) => {
  const handleSelectItem = (value: string) => {
    handleSelect(value);
  };

  return (
    <View className='w-max-[400px] flex-center flex-row flex-wrap gap-3'>
      {filterData?.map(item => {
        const isSelected = filterSelected === item.value;

        return (
          <TouchableWithoutFeedback
            onPress={() => handleSelectItem(item.value)}
            key={item.value}
          >
            <View
              className={`flex-center flex-row gap-2 rounded-full border-2 border-primary px-2 py-1 pr-4 ${isSelected ? "bg-primary" : "bg-white_black"}`}
            >
              <View className={`rounded-full p-[0.5px] ${isSelected ? "bg-white" : ""}`}>
                <Image
                  source={item.imageUrl}
                  style={[{ width: 50, height: 50, borderRadius: 50 }]}
                  cachePolicy={"disk"}
                  contentFit='cover'
                  transition={200}
                />
              </View>
              <Text
                className={`body-medium text-center ${isSelected ? "text-white" : "text-black_white"}`}
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
