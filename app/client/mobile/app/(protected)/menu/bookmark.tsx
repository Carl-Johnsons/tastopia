import { useGetBookmarks } from "@/api/recipe";
import Recipe from "@/components/common/Recipe";
import SettingRecipe from "@/components/common/SettingRecipe";
import Empty from "@/components/screen/community/Empty";
import BookmarkHeader from "@/components/screen/menu/BookmarkHeader";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { RecipeType } from "@/types/recipe";
import { filterUniqueItems } from "@/utils/dataFilter";
import BottomSheet from "@gorhom/bottom-sheet";
import { useFocusEffect } from "expo-router";
import { useCallback, useEffect, useRef, useState } from "react";
import {
  FlatList,
  RefreshControl,
  SafeAreaView,
  StatusBar,
  Text,
  View
} from "react-native";

const bookmark = () => {
  const { c } = useColorizer();
  const { white, black } = colors;

  const bottomSheetRef = useRef<BottomSheet>(null);
  const [currentRecipeId, setCurrentRecipeId] = useState("");
  const [currentAuthorId, setCurrentAuthorId] = useState("");
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
  } = useGetBookmarks();

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
        <Recipe
          {...item}
          setCurrentRecipeId={setCurrentRecipeId}
          setCurrentAuthorId={setCurrentAuthorId}
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
      style={{
        flex: 1,
        backgroundColor: c(white.DEFAULT, black[100])
      }}
    >
      <StatusBar backgroundColor={c(white.DEFAULT, black[100])} />
      <BookmarkHeader />
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
          <Empty type='emptyBookmark' />
        </View>
      )}

      <SettingRecipe
        id={currentRecipeId}
        authorId={currentAuthorId}
        ref={bottomSheetRef}
      />
    </SafeAreaView>
  );
};

export default bookmark;
