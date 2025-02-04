import Recipe from "@/components/common/Recipe";
import { useCallback, useEffect, useRef, useState } from "react";
import Empty from "@/components/screen/community/Empty";
import Header from "@/components/screen/community/Header";
import { View, RefreshControl, SafeAreaView, FlatList } from "react-native";
import { filterUniqueItems } from "@/utils/dataFilter";
import { router } from "expo-router";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";
import { useRecipesFeed } from "@/api/recipe";
import BottomSheet from "@gorhom/bottom-sheet";
import SettingRecipe from "@/components/common/SettingRecipe";

const Community = () => {
  const { c } = useColorizer();
  const { black, white } = colors;

  const bottomSheetRef = useRef<BottomSheet>(null);

  const [currentRecipeId, setCurrentRecipeId] = useState("");
  const [recipes, setRecipes] = useState<RecipeType[]>([]);
  const [filterSelected, setFilterSelected] = useState<string>("All");

  const { data, fetchNextPage, hasNextPage, isFetchingNextPage, refetch, isRefetching } =
    useRecipesFeed(filterSelected);

  const handleCreateRecipe = () => {
    router.push("/(protected)/community/create-recipe");
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
      <View
        className='px-4'
        testID='recipe'
      >
        <Recipe
          {...item}
          setCurrentRecipeId={setCurrentRecipeId}
          bottomSheetRef={bottomSheetRef}
        />
        {index !== recipes.length - 1 && (
          <View className='my-4 h-[1px] w-full bg-gray-300' />
        )}
      </View>
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
      style={{ backgroundColor: c(white.DEFAULT, black[100]), height: "100%" }}
    >
      <FlatList
        removeClippedSubviews
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

      <SettingRecipe
        id={currentRecipeId}
        ref={bottomSheetRef}
        title='Settings'
      />
    </SafeAreaView>
  );
};

export default Community;
