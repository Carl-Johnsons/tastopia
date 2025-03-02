import { useGetDeletedRecipe } from "@/api/recipe";
import Empty from "@/components/screen/community/Empty";
import DeletedHeader from "@/components/screen/menu/DeletedHeader";
import DeletedRecipeItem from "@/components/screen/menu/DeletedRecipeItem";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { RecipeType } from "@/types/recipe";
import { filterUniqueItems } from "@/utils/dataFilter";
import { useFocusEffect } from "expo-router";
import { useCallback, useEffect, useState } from "react";
import { FlatList, RefreshControl, SafeAreaView, StatusBar, View } from "react-native";

const DeletedRecipe = () => {
  const { c } = useColorizer();
  const { white, black } = colors;
  const [recipes, setRecipes] = useState<RecipeType[]>([]);

  const {
    data,
    fetchNextPage,
    hasNextPage,
    isFetchingNextPage,
    refetch,
    isRefetching,
    isStale,
    isLoading
  } = useGetDeletedRecipe();

  const onRefresh = useCallback(() => {
    if (isStale) {
      refetch();
    }
  }, [isStale]);

  useFocusEffect(onRefresh);

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
        <DeletedRecipeItem {...item} />
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
      style={{
        flex: 1,
        backgroundColor: c(white.DEFAULT, black[100])
      }}
    >
      <StatusBar backgroundColor={c(white.DEFAULT, black[100])} />
      <DeletedHeader />
      <FlatList
        className='h-full'
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
        renderItem={renderItem}
      />

      {!isLoading && recipes.length === 0 && (
        <View className='size-full'>
          <Empty type='emptyDeleted' />
        </View>
      )}
    </SafeAreaView>
  );
};

export default DeletedRecipe;
