import Recipe from "@/components/common/Recipe";
import { useCallback, useEffect, useRef, useState } from "react";
import Empty from "@/components/screen/community/Empty";
import Header from "@/components/screen/community/Header";
import {
  View,
  RefreshControl,
  SafeAreaView,
  FlatList,
  Appearance,
  StatusBar,
  Alert
} from "react-native";
import { filterUniqueItems } from "@/utils/dataFilter";
import { router } from "expo-router";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";
import { useRecipesFeed } from "@/api/recipe";
import BottomSheet from "@gorhom/bottom-sheet";
import SettingRecipe from "@/components/common/SettingRecipe";
import { ROLE, selectRole } from "@/slices/auth.slice";
import { usePushNotification } from "@/hooks";
import { selectSetting } from "@/slices/setting.slice";
import { changeLanguage } from "@/utils/language";
import { getBooleanValueFromSetting } from "@/utils/converter";
import { RecipeType } from "@/types/recipe";
import { selectIsActiveUser } from "@/slices/user.slice";
import { useTranslation } from "react-i18next";

const Community = () => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const { t } = useTranslation("component");
  const isActiveUser = selectIsActiveUser();

  const isFirstRender = useRef(true);
  const bottomSheetRef = useRef<BottomSheet>(null);
  const [currentRecipeId, setCurrentRecipeId] = useState("");
  const [currentAuthorId, setCurrentAuthorId] = useState("");
  const [recipes, setRecipes] = useState<RecipeType[]>([]);
  const [filterSelected, setFilterSelected] = useState<string>("All");
  const role = selectRole();
  const { registerForPushNotificationAsync } = usePushNotification();

  const {
    data,
    fetchNextPage,
    hasNextPage,
    isFetchingNextPage,
    refetch,
    isRefetching,
    isLoading
  } = useRecipesFeed(filterSelected);

  const handleCreateRecipe = () => {
    router.push("/(protected)/community/create-recipe");
  };

  const handleFilter = (key: string) => {
    setFilterSelected(key);
  };

  const onRefresh = () => {
    refetch();
  };

  const handleReachEnd = () => {
    if (isFirstRender.current) {
      isFirstRender.current = false;
      return;
    }

    if (!isActiveUser) {
      Alert.alert(t("notLoggedIn"), t("loadMoreRecipe"), [
        {
          text: t("settingUser.ok"),
          onPress: () => {
            router.push("/login");
          }
        },
        {
          text: t("settingUser.cancel")
        }
      ]);
    }

    if (!isFetchingNextPage && hasNextPage) {
      fetchNextPage();
    }
  };

  const { LANGUAGE, DARK_MODE } = selectSetting();

  const loadSettings = useCallback(() => {
    changeLanguage(LANGUAGE);
    Appearance.setColorScheme(getBooleanValueFromSetting(DARK_MODE) ? "dark" : "light");
  }, [LANGUAGE, DARK_MODE]);

  useEffect(() => {
    loadSettings();
  }, [loadSettings]);

  useEffect(() => {
    if (role !== ROLE.GUEST) {
      registerForPushNotificationAsync();
    }
  }, [role]);

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
      style={{ backgroundColor: c(white.DEFAULT, black[100]), height: "100%" }}
    >
      <StatusBar backgroundColor={c(white.DEFAULT, black[100])} />
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
        onEndReached={handleReachEnd}
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
        authorId={currentAuthorId}
        ref={bottomSheetRef}
      />
    </SafeAreaView>
  );
};

export default Community;
