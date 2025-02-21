import {
  IRecipeViewingHistoryResponse,
  useSearchRecipeViewingHistory
} from "@/api/tracking";
import Recipe from "@/components/common/Recipe";
import SettingRecipe from "@/components/common/SettingRecipe";
import { colors } from "@/constants/colors";
import BottomSheet from "@gorhom/bottom-sheet/lib/typescript/components/bottomSheet/BottomSheet";
import { useFocusEffect } from "expo-router";
import { useCallback, useEffect, useRef, useState } from "react";
import { FlatList, ListRenderItemInfo } from "react-native";
import { ActivityIndicator, RefreshControl, View } from "react-native";
import Empty from "../community/Empty";
import { useAppDispatch } from "@/store/hooks";
import { saveHistoryData } from "@/slices/history.slice";
import useHydrateData from "@/hooks/useHydrateData";

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

  const bottomSheetRef = useRef<BottomSheet>(null);
  const [currentRecipeId, setCurrentRecipeId] = useState("");
  const [currentAuthorId, setCurrentAuthorId] = useState("");

  const renderItem = useCallback(
    ({
      item,
      index
    }: {
      item: ListRenderItemInfo<IRecipeViewingHistoryResponse>;
      index: number;
    }) => {
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
    [history?.length]
  );

  const dispatch = useAppDispatch();
  useEffect(() => {
    dispatch(saveHistoryData({ isLoading }));
  }, [isLoading]);

  if (isLoading && !!history) {
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
      <SettingRecipe
        id={currentRecipeId}
        authorId={currentAuthorId}
        ref={bottomSheetRef}
      />
    </>
  );
}
