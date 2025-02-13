import { Image } from "expo-image";
import { memo } from "react";
import { Platform, Text, TouchableWithoutFeedback, View } from "react-native";

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
              className={`flex-center flex-row gap-2 rounded-full border-2 border-primary px-1 py-1 ${isSelected ? "bg-primary" : "bg-white_black"}`}
            >
              <View
                className={`rounded-full p-[0.5px] ${isSelected ? "bg-white" : ""}`}
                style={
                  !isSelected &&
                  Platform.select({
                    ios: {
                      shadowColor: "#000",
                      shadowOffset: { width: 0, height: 2 },
                      shadowOpacity: 0.3,
                      shadowRadius: 10
                    },
                    android: { elevation: 5 }
                  })
                }
              >
                <Image
                  source={item.imageUrl}
                  style={[{ width: 50, height: 50, borderRadius: 50 }]}
                  cachePolicy={"disk"}
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
