import {
  IRecipeViewingHistoryResponse,
  useSearchRecipeViewingHistory
} from "@/api/tracking";
import MiniRecipe from "@/components/common/MiniRecipe";
import { colors } from "@/constants/colors";
import useHydrateData from "@/hooks/useHydrateData";
import { useFocusEffect } from "expo-router";
import { useCallback, useState } from "react";
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
  const [history, setHistory] = useState<IRecipeViewingHistoryResponse[]>();
  useHydrateData({ source: data, setter: setHistory });

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

  if (isLoading || !history) {
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
    <View className='pt-5'>
      <FlatList
        horizontal
        className='ps-4'
        contentContainerStyle={{
          paddingRight: 30,
          height: 200,
          justifyContent: "center"
        }}
        data={history}
        onEndReached={handleLoadMore}
        onEndReachedThreshold={0.1}
        ItemSeparatorComponent={() => <View className='w-[20px]' />}
        showsHorizontalScrollIndicator={false}
        ListEmptyComponent={() => (
          <View className='mt-[30px] w-[100vw]'>
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
    </View>
  );
}
