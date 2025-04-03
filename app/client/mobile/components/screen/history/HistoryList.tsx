import {
  IRecipeViewingHistoryResponse,
  useSearchRecipeViewingHistory
} from "@/api/tracking";
import Recipe from "@/components/common/Recipe";
import { colors } from "@/constants/colors";
import { useFocusEffect } from "expo-router";
import { useCallback, useEffect, useState } from "react";
import { FlatList, ListRenderItemInfo } from "react-native";
import { ActivityIndicator, RefreshControl, View } from "react-native";
import Empty from "../community/Empty";
import { useAppDispatch } from "@/store/hooks";
import { saveHistoryData } from "@/slices/history.slice";
import useHydrateData from "@/hooks/useHydrateData";
import { useHistoryContext } from "./HistoryProvider";

type HistoryListProps = {
  keyword: string;
};

export default function HistoryList({ keyword }: HistoryListProps) {
  const { data, isLoading, refetch, isStale, fetchNextPage } =
    useSearchRecipeViewingHistory(keyword);
  const { primary } = colors;
  const [history, setHistory] = useState<IRecipeViewingHistoryResponse[]>();
  useHydrateData({ source: data, setter: setHistory });

  const fetchData = useCallback(() => {
    if (isStale) {
      refetch();
    }
  }, [isStale]);

  useFocusEffect(fetchData);

  const { bottomSheetRef } = useHistoryContext();
  const dispatch = useAppDispatch();

  const setCurrentRecipeId = useCallback(
    (id: string) => {
      dispatch(saveHistoryData({ currentRecipeId: id }));
    },
    [dispatch]
  );

  const setCurrentAuthorId = useCallback(
    (id: string) => {
      dispatch(saveHistoryData({ currentAuthorId: id }));
    },
    [dispatch]
  );

  const renderItem = useCallback(
    ({ item, index }: ListRenderItemInfo<IRecipeViewingHistoryResponse>) => {
      if (!history) return null;

      return (
        <View testID='recipe'>
          <Recipe
            {...item}
            setCurrentRecipeId={setCurrentRecipeId}
            setCurrentAuthorId={setCurrentAuthorId}
            bottomSheetRef={bottomSheetRef}
          />
          {index !== history.length - 1 && (
            <View className='my-4 h-[1px] w-full bg-gray-300' />
          )}
        </View>
      );
    },
    [history, bottomSheetRef]
  );

  useEffect(() => {
    dispatch(saveHistoryData({ isLoading }));
  }, [isLoading]);

  if (isLoading || !history) {
    return (
      <View className='flex-center h-[50%]'>
        <ActivityIndicator
          size='large'
          color={primary}
        />
      </View>
    );
  }

  return (
    <>
      <FlatList
        data={history}
        className='h-full'
        onEndReached={() => fetchNextPage()}
        onEndReachedThreshold={0.1}
        ItemSeparatorComponent={() => <View className='w-[20px]' />}
        showsHorizontalScrollIndicator={false}
        ListEmptyComponent={() => (
          <View className='h-[100vh]'>
            <Empty type='emptyRecipeViewingHistory' />
          </View>
        )}
        renderItem={renderItem}
        refreshControl={
          <RefreshControl
            refreshing={isLoading}
            tintColor={primary}
            onRefresh={refetch}
          />
        }
      />
    </>
  );
}
