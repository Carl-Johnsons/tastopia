import {
  FlatList,
  Platform,
  RefreshControl,
  SafeAreaView,
  StyleSheet,
  Text,
  View
} from "react-native";
import { TabView } from "@rneui/themed";
import { useCommentsByAuthorId } from "@/api/recipe";
import { useCallback, useEffect, useState } from "react";
import { filterUniqueItems } from "@/utils/dataFilter";
import Empty from "../community/Empty";

type CommentsTabProps = {
  accountId: string;
};

const CommentsTab = ({ accountId }: CommentsTabProps) => {
  const [comments, setComments] = useState<IComment[]>([]);
  const { data, fetchNextPage, hasNextPage, isFetchingNextPage, refetch, isRefetching } =
    useCommentsByAuthorId(accountId);

  const onRefresh = () => {
    refetch();
  };

  const handleLoadMore = () => {
    if (!isFetchingNextPage && hasNextPage) {
      fetchNextPage();
    }
  };

  const renderItem = useCallback(
    ({ item, index }: { item: IComment; index: number }) => (
      <View
        className='px-4'
        testID='comment'
      >
        <Text>{item.content}</Text>
        {index !== comments.length - 1 && (
          <View className='my-4 h-[1px] w-full bg-gray-300' />
        )}
      </View>
    ),
    [comments.length]
  );

  const keyExtractor = useCallback((item: IComment) => item.id.toString(), []);

  useEffect(() => {
    if (data?.pages) {
      const uniqueData = filterUniqueItems(data.pages);
      setComments(uniqueData);
    }
  }, [data]);

  return (
    <TabView.Item style={{ width: "100%", height: "100%", flex: 1, marginTop: 16 }}>
      <SafeAreaView
        style={{
          width: "100%",
          height: "100%",
          alignItems: "center",
          flex: 1
        }}
      >
        <FlatList
          removeClippedSubviews
          data={comments}
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
          ListEmptyComponent={() => (
            <View style={{ flex: 1, marginTop: 50 }}>
              <Empty type='emptyComment' />
            </View>
          )}
          contentContainerStyle={{
            paddingBottom: Platform.select({ ios: 240 })
          }}
        />
      </SafeAreaView>
    </TabView.Item>
  );
};

export default CommentsTab;

const styles = StyleSheet.create({});
