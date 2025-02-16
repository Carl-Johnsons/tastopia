import { FlatList, Platform, RefreshControl, SafeAreaView, View } from "react-native";
import { TabView } from "@rneui/themed";
import { useCommentsByAuthorId } from "@/api/recipe";
import {
  Dispatch,
  RefObject,
  SetStateAction,
  useCallback,
  useEffect,
  useState
} from "react";
import { filterUniqueItems } from "@/utils/dataFilter";
import Empty from "../community/Empty";
import Comment from "./Comment";
import { BottomSheetMethods } from "@gorhom/bottom-sheet/lib/typescript/types";

type CommentsTabProps = {
  accountId: string;
  bottomSheetCommentRef: RefObject<BottomSheetMethods>;
  setCurrentComment: Dispatch<SetStateAction<CommentCustomType>>;
  setCurrentCommentAuthorId: Dispatch<SetStateAction<string>>;
};

const CommentsTab = ({
  accountId,
  bottomSheetCommentRef,
  setCurrentComment,
  setCurrentCommentAuthorId
}: CommentsTabProps) => {
  const [comments, setComments] = useState<IAccountRecipeCommentResponse[]>([]);
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
    ({ item, index }: { item: IAccountRecipeCommentResponse; index: number }) => (
      <View
        className='w-full'
        testID='comment'
      >
        <Comment
          {...item}
          bottomSheetCommentRef={bottomSheetCommentRef}
          setCurrentComment={setCurrentComment}
          setCurrentCommentAuthorId={setCurrentCommentAuthorId}
        />
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
    <TabView.Item
      style={{
        width: "100%",
        height: "100%",
        flex: 1,
        marginTop: 16,
        paddingHorizontal: 12
      }}
    >
      <SafeAreaView
        style={{
          width: "100%",
          height: "100%",
          alignItems: "center",
          flex: 1
        }}
      >
        <FlatList
          showsVerticalScrollIndicator={false}
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
