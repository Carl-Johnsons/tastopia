import { Image } from "expo-image";
import { globalStyles } from "@/components/common/GlobalStyles";
import Recipe from "@/components/common/Recipe";
import { LogoIcon } from "@/components/common/SVG";
import Filter from "@/components/screen/community/Filter";
import i18next from "i18next";
import React, { useEffect, useMemo, useState } from "react";
import { Text, View, RefreshControl, SafeAreaView, FlatList } from "react-native";
import Header from "@/components/screen/community/Header";
import { selectAccessToken } from "@/slices/auth.slice";
import { transformPlatformURI } from "@/utils/functions";
import { useRecipesFeed } from "@/api/recipe";

const Community = () => {
  const [filterSelected, setFilterSelected] = useState<string>("All");

  const {
    data,
    fetchNextPage,
    hasNextPage,
    isLoading,
    isFetchingNextPage,
    refetch,
    isRefetching
  } = useRecipesFeed(filterSelected);
  const recipes = useMemo(() => {
    return data?.pages.flatMap(page => page.paginatedData) ?? [];
  }, [data]);

  const handleCreateRecipe = () => {
    console.log("show modal create recipe");
  };

  const handleFilter = (key: string) => {
    setFilterSelected(key);
  };

  const toggleLanguage = () => {
    const currentLang = i18next.language;
    const newLang = currentLang === "en" ? "vi" : "en";
    i18next.changeLanguage(newLang);
  };

  const onRefresh = () => {
    refetch();
  };

  const handleLoadMore = () => {
    if (!isFetchingNextPage && hasNextPage) {
      fetchNextPage();
    }
  };

  return (
    <SafeAreaView
      style={{
        backgroundColor: globalStyles.color.light,
        height: "100%"
      }}
    >
      <FlatList
        style={{ paddingHorizontal: 16 }}
        data={recipes}
        keyExtractor={item => item.id.toString()}
        refreshControl={
          <RefreshControl
            refreshing={isRefetching}
            tintColor={"#fff"}
            onRefresh={onRefresh}
          />
        }
        onEndReached={handleLoadMore}
        onEndReachedThreshold={0.1}
        ListHeaderComponent={Header({
          isRefreshing: isRefetching,
          handleFilter,
          filterSelected,
          handleCreateRecipe
        })}
        renderItem={({ item, index }) => (
          <>
            <Recipe {...item} />
            {index !== recipes.length - 1 && (
              <View className='my-4 h-[1px] w-full bg-gray-300' />
            )}
          </>
        )}
        ListEmptyComponent={() => (
          <View className='flex-center h-[70%] gap-2'>
            <Image
              source={require("../../assets/icons/noResult.png")}
              style={{ width: 130, height: 130 }}
            />
            <Text className='paragraph-medium text-center'>
              No recipes found! {"\n"}Time to create your own masterpiece!
            </Text>
          </View>
        )}
      />
    </SafeAreaView>
  );
};

export default Community;
