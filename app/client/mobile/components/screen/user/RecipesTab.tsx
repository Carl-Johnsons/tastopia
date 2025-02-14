import {
  FlatList,
  Platform,
  RefreshControl,
  SafeAreaView,
  StyleSheet,
  View
} from "react-native";
import { TabView } from "@rneui/themed";
import { useRecipesFeedByAuthorId } from "@/api/recipe";
import {
  Dispatch,
  RefObject,
  SetStateAction,
  useCallback,
  useEffect,
  useState
} from "react";
import Recipe from "@/components/common/Recipe";
import { filterUniqueItems } from "@/utils/dataFilter";
import Empty from "../community/Empty";
import { BottomSheetMethods } from "@gorhom/bottom-sheet/lib/typescript/types";

type RecipesTabProps = {
  accountId: string;
  bottomSheetRef: RefObject<BottomSheetMethods>;
  setCurrentRecipeId: Dispatch<SetStateAction<string>>;
  setCurrentAuthorId: Dispatch<SetStateAction<string>>;
};

const RecipesTab = ({
  accountId,
  bottomSheetRef,
  setCurrentAuthorId,
  setCurrentRecipeId
}: RecipesTabProps) => {
  const [recipes, setRecipes] = useState<RecipeType[]>([]);

  const { data, fetchNextPage, hasNextPage, isFetchingNextPage, refetch, isRefetching } =
    useRecipesFeedByAuthorId(accountId);

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
          ListEmptyComponent={() => (
            <View style={{ flex: 1, marginTop: 50 }}>
              <Empty type='emptyRecipe' />
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

export default RecipesTab;

const styles = StyleSheet.create({});
