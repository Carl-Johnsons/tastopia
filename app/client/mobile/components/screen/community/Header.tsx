import { Image } from "expo-image";
import { LogoIcon } from "@/components/common/SVG";
import Filter from "@/components/screen/community/Filter";
import React from "react";
import { Text, View, TouchableWithoutFeedback } from "react-native";

type HeaderProps = {
  isRefreshing: boolean;
  handleFilter: (key: string) => void;
  filterSelected: string;
  handleCreateRecipe: () => void;
};

function Header({
  isRefreshing,
  handleFilter,
  filterSelected,
  handleCreateRecipe
}: HeaderProps) {
  return (
    <View className='gap-8 mb-8'>
      <View className='flex-center'>
        <LogoIcon
          isActive={isRefreshing}
          width={60}
          height={60}
        />
      </View>

      <Filter
        handleSelect={handleFilter}
        filterSelected={filterSelected}
      />

      <View className='flex-row px-6 mt-2 flex-start'>
        <View className='flex-row gap-3'>
          <Image
            cachePolicy={"disk"}
            source={require("../../../assets/images/avatar.png")}
            style={{ width: 50, height: 50, borderRadius: 100 }}
          />
          <View className='gap-2'>
            <Text className='paragraph-bold'>Vuong</Text>
            <TouchableWithoutFeedback onPress={handleCreateRecipe}>
              <View className='rounded-2xl border-[1px] border-gray-600 px-4 py-3'>
                <Text className='text-gray-600'>What's cooking? Share your recipe</Text>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </View>
      </View>
    </View>
  );
}

export default Header;
