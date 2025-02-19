import {
  IRecipeViewingHistoryResponse,
  useSearchRecipeViewingHistory
} from "@/api/tracking";
import MiniRecipe from "@/components/common/MiniRecipe";
import { colors } from "@/constants/colors";
import { filterUniqueItems } from "@/utils/dataFilter";
import { stringify } from "@/utils/debug";
import { useFocusEffect } from "expo-router";
import { useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { FlatList, ListRenderItemInfo } from "react-native";
import { ActivityIndicator, RefreshControl, View } from "react-native";
import { Text } from "react-native";

export default function PreviewHistoryList() {
  const {
    data,
    isLoading,
    refetch,
    isStale,
    isFetchingNextPage,
    hasNextPage,
    fetchNextPage
  } = useSearchRecipeViewingHistory();
  const { primary } = colors;
  const { t } = useTranslation("menu");
  const [history, setHistory] = useState<IRecipeViewingHistoryResponse[]>([]);

  const fetchData = useCallback(() => {
    if (isStale) {
      refetch();
    }
  }, [isStale]);

  useFocusEffect(fetchData);

  const handleRefreshing = useCallback(async () => {
    await refetch();
  }, [refetch]);

  const handleLoadMore = useCallback(() => {
    if (!isFetchingNextPage && hasNextPage) {
      fetchNextPage();
    }
  }, [isFetchingNextPage, hasNextPage]);

  const renderItem = useCallback(
    ({ item }: ListRenderItemInfo<IRecipeViewingHistoryResponse>) => {
      const { id, authorId, recipeImgUrl, title, authorDisplayName, voteDiff, vote } =
        item;

      return (
        <MiniRecipe
          key={id}
          id={id}
          authorId={authorId}
          recipeImgUrl={recipeImgUrl}
          title={title}
          authorDisplayName={authorDisplayName}
          voteDiff={voteDiff}
          vote={vote}
        />
      );
    },
    []
  );

  useEffect(() => {
    if (data?.pages) {
      setHistory(filterUniqueItems(data.pages));
    }
  }, [data]);

  if (isLoading) {
    return (
      <View className='flex-center h-[140px]'>
        <ActivityIndicator
          size='large'
          color={primary}
        />
      </View>
    );
  }

  return (
    <FlatList
      horizontal
      className={`py-5 ps-4 ${history.length === 0 ? "w-[100vw]" : ""}`}
      data={history}
      onEndReached={handleLoadMore}
      onEndReachedThreshold={0.1}
      ItemSeparatorComponent={() => <View className='w-[20px]' />}
      showsHorizontalScrollIndicator={false}
      ListEmptyComponent={() => (
        <View className='flex-center'>
          <Text className='text-center font-light text-lg text-gray-500'>
            {t("noHistory")}
          </Text>
        </View>
      )}
      renderItem={renderItem}
      refreshControl={
        <RefreshControl
          refreshing={isLoading}
          tintColor={primary}
          onRefresh={handleRefreshing}
        />
      }
    />
  );
}
