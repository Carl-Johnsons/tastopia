import i18next from "i18next";
import { useRecipesFeed } from "@/api/recipe";
import Recipe from "@/components/common/Recipe";
import { useCallback, useEffect, useMemo, useState } from "react";
import Empty from "@/components/screen/community/Empty";
import Header from "@/components/screen/community/Header";
import { globalStyles } from "@/components/common/GlobalStyles";
import { View, RefreshControl, SafeAreaView, FlatList } from "react-native";
import { filterUniqueItems } from "@/utils/dataFilter";
import { router } from "expo-router";

const Community = () => {
  const [recipes, setRecipes] = useState<RecipeType[]>([]);
  const [filterSelected, setFilterSelected] = useState<string>("All");

  //TODO: apply loading later
  const {
    data,
    fetchNextPage,
    hasNextPage,
    isLoading,
    isFetchingNextPage,
    refetch,
    isRefetching
  } = useRecipesFeed(filterSelected);

  const handleCreateRecipe = () => {
    router.push("/(modals)/CreateRecipe");
  };

  const handleFilter = (key: string) => {
    setFilterSelected(key);
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

  useEffect(() => {
    if (data?.pages) {
      const uniqueData = filterUniqueItems(data.pages);
      setRecipes(uniqueData);
    }
  }, [data]);

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
