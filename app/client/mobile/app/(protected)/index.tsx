import { Image } from "expo-image";
import { globalStyles } from "@/components/common/GlobalStyles";
import Recipe from "@/components/common/Recipe";
import { LogoIcon } from "@/components/common/SVG";
import Filter from "@/components/screen/community/Filter";
import i18next from "i18next";
import React, { memo, useCallback, useEffect, useMemo, useState } from "react";
import { Text, View, RefreshControl, SafeAreaView, FlatList } from "react-native";
import Header from "@/components/screen/community/Header";
import { selectAccessToken } from "@/slices/auth.slice";
import { transformPlatformURI } from "@/utils/functions";
import { useRecipesFeed } from "@/api/recipe";
import Empty from "@/components/screen/community/Empty";

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

  const renderItem = useCallback(
    ({ item, index }: { item: RecipeType; index: number }) => (
      <>
        <Recipe {...item} />
        {index !== recipes.length - 1 && (
          <View className='my-4 h-[1px] w-full bg-gray-300' />
        )}
      </>
    ),
    [recipes.length]
  );

  const keyExtractor = useCallback((item: RecipeType) => item.id.toString(), []);

  return (
    <SafeAreaView
      style={{
        backgroundColor: globalStyles.color.light,
        height: "100%"
      }}
    >
      <FlatList
        removeClippedSubviews
        style={{ paddingHorizontal: 16 }}
        data={recipes}
        keyExtractor={keyExtractor}
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
        renderItem={renderItem}
        ListEmptyComponent={() => <Empty />}
      />
    </SafeAreaView>
  );
};

export default Community;
