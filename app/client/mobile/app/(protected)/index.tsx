import { Image } from "expo-image";
import { globalStyles } from "@/components/common/GlobalStyles";
import Recipe from "@/components/common/Recipe";
import { LogoIcon } from "@/components/common/SVG";
import Filter from "@/components/screen/community/Filter";
import i18next from "i18next";
import React, { useEffect, useState } from "react";
import { Text, View, RefreshControl, SafeAreaView, FlatList } from "react-native";
import Header from "@/components/screen/community/Header";
import { selectAccessToken } from "@/slices/auth.slice";
import { transformPlatformURI } from "@/utils/functions";

const Community = () => {
  const [skip, setSkip] = useState<number>(0);
  const [isLoading, setIsLoading] = useState(false);
  const [isRefreshing, setIsRefreshing] = useState(false);
  const [recipes, setRecipes] = useState<RecipeType[]>([]);
  const [filterSelected, setFilterSelected] = useState<string>("All");
  const [hasNextPage, setHasNextPage] = useState<boolean>();
  const accessToken = selectAccessToken();

  const fetchFeed = async (isFetchMore: boolean) => {
    if (!isLoading) {
      setIsLoading(true);
    }

    if (isFetchMore && !hasNextPage) {
      console.log("can't fetch more");
      return;
    }

    console.log("skip", skip);
    console.log("total recipes", recipes?.length);

    const url = transformPlatformURI("http://localhost:5000/api/recipe/get-recipe-feed");

    const headers = {
      "Content-Type": "application/json"
    };

    const body = JSON.stringify({
      skip: parseInt(skip.toString()),
      tagValues: [filterSelected]
    });

    try {
      const response = await fetch(url, {
        method: "POST",
        headers: headers,
        body: body
      });

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(`HTTP error! status: ${response.status}, message: ${errorText}`);
      }

      const data = await response.json();

      if (isFetchMore) {
        setRecipes(prev => {
          console.log("fetch more", body);
          const existingIds = new Set(prev.map(recipe => recipe.id));
          const newRecipes = data.paginatedData.filter(
            (newRecipe: RecipeType) => !existingIds.has(newRecipe.id)
          );

          return [...prev, ...newRecipes];
        });
      } else {
        setRecipes(data.paginatedData);
      }

      setHasNextPage(data.metadata.hasNextPage);
    } catch (error) {
      console.error("Error fetching recipe feed:", error);
      throw error;
    } finally {
      setIsLoading(false);
      setIsRefreshing(false);
    }
  };

  const handleCreateRecipe = () => {
    console.log("show modal create recipe");
  };

  const handleFilter = (key: string) => {
    setFilterSelected(key);
    setSkip(0);
    setRecipes([]);
  };

  const toggleLanguage = () => {
    const currentLang = i18next.language;
    const newLang = currentLang === "en" ? "vi" : "en";
    i18next.changeLanguage(newLang);
  };

  const onRefresh = async () => {
    setIsRefreshing(true);
    setSkip(0);
    setRecipes([]);
  };

  const handleLoadMore = () => {
    if (!isLoading && !isRefreshing) {
      setSkip(prev => prev + 1);
    }
  };

  useEffect(() => {
    fetchFeed(false);
  }, [filterSelected]);

  useEffect(() => {
    if (skip > 0) {
      fetchFeed(true);
    }
  }, [skip]);

  useEffect(() => {
    if (isRefreshing) {
      fetchFeed(false);
      setIsRefreshing(false);
    }
  }, [isRefreshing]);

  return (
    <SafeAreaView
      style={{
        backgroundColor: globalStyles.color.light,
        height: "100%"
      }}
    >
      {recipes?.length > 0 && (
        <FlatList
          style={{ paddingHorizontal: 16 }}
          data={recipes}
          keyExtractor={item => item.id.toString()}
          refreshControl={
            <RefreshControl
              refreshing={isRefreshing}
              tintColor={"#fff"}
              onRefresh={onRefresh}
            />
          }
          onEndReached={handleLoadMore}
          onEndReachedThreshold={0.1}
          ListHeaderComponent={Header({
            isRefreshing,
            handleFilter,
            filterSelected,
            handleCreateRecipe
          })}
          renderItem={({ item, index }) => (
            <>
              <Recipe {...item} />
              {index !== recipes.length - 1 && (
                <View className='my-4 h-[1px] w-full bg-gray-300' />
              )}
            </>
          )}
          ListEmptyComponent={() => (
            <View className='flex-center h-[70%] gap-2'>
              <Image
                source={require("../../assets/icons/noResult.png")}
                style={{ width: 130, height: 130 }}
              />
              <Text className='paragraph-medium text-center'>
                No recipes found! {"\n"}Time to create your own masterpiece!
              </Text>
            </View>
          )}
        />
      )}
    </SafeAreaView>
  );
};

export default Community;
