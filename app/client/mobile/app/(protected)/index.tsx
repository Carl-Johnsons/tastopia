import { globalStyles } from "@/components/common/GlobalStyles";
import Recipe from "@/components/common/Recipe";
import { LogoIcon } from "@/components/common/SVG";
import Filter from "@/components/screen/community/Filter";
import i18next from "i18next";
import React, { Fragment, useEffect, useState } from "react";
import {
  Text,
  View,
  ScrollView,
  RefreshControl,
  Image,
  TouchableWithoutFeedback,
  SafeAreaView
} from "react-native";

const Community = () => {
  const [skip, setSkip] = useState<number>(0);
  const [isLoading, setIsLoading] = useState(false);
  const [isRefreshing, setIsRefreshing] = useState(false);
  const [recipes, setRecipes] = useState<RecipeType[]>([]);
  const [filterSelected, setFilterSelected] = useState<string>("All");
  const [hasNextPage, setHasNextPage] = useState<boolean>();

  const fetchFeed = async (isFetchMore: boolean) => {
    if (!isLoading) {
      setIsLoading(true);
    }

    if (isFetchMore && !hasNextPage) {
      console.log("can't fetch more");
      return;
    }

    const url = "http://localhost:5005/api/recipe/get-recipe-feed";

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
    await fetchFeed(false);
    setIsRefreshing(false);
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

  return (
    <SafeAreaView style={{ backgroundColor: globalStyles.color.light, height: "100%" }}>
      <ScrollView
        refreshControl={
          <RefreshControl
            refreshing={isRefreshing}
            tintColor={"#fff"}
            onRefresh={onRefresh}
          />
        }
        onScroll={({ nativeEvent }) => {
          const { layoutMeasurement, contentOffset, contentSize } = nativeEvent;
          const isEndReached =
            layoutMeasurement.height + contentOffset.y >= contentSize.height - 20;
          if (isEndReached && !isLoading && !isRefreshing) {
            handleLoadMore();
          }
        }}
        scrollEventThrottle={400}
      >
        <View className='gap-8 px-4 pt-2 size-full'>
          <View className='flex-center'>
            <LogoIcon
              isActive={isRefreshing}
              width={60}
              height={60}
            />
          </View>

          <Filter handleSelect={handleFilter} />

          {/* Check user exist right there */}
          <View className='flex-row px-6 mt-2 flex-start'>
            <View className='flex-row gap-3'>
              <Image
                source={require("../../assets/images/avatar.png")}
                className='size-[50px] rounded-full'
              />
              <View className='gap-2'>
                <Text className='paragraph-bold'>Vuong</Text>
                <TouchableWithoutFeedback onPress={handleCreateRecipe}>
                  <View className='rounded-2xl border-[1px] border-gray-600 px-4 py-3'>
                    <Text className='text-gray-600'>
                      What's cooking? Share your recipe
                    </Text>
                  </View>
                </TouchableWithoutFeedback>
              </View>
            </View>
          </View>

          <View className='gap-4'>
            {recipes && recipes.length > 0 ? (
              recipes.map((recipe, index) => {
                return (
                  <Fragment key={recipe.id}>
                    <Recipe
                      id={recipe.id}
                      authorId={recipe.authorId}
                      title={recipe.title}
                      description={recipe.description}
                      authorDisplayName={recipe.authorDisplayName}
                      authorAvtUrl={recipe.authorAvtUrl}
                      voteDiff={recipe.voteDiff}
                      numberOfComment={recipe.numberOfComment}
                    />
                    {index !== recipes.length - 1 && (
                      <View className='h-[1px] w-full bg-gray-300' />
                    )}
                  </Fragment>
                );
              })
            ) : (
              <View className='flex-center h-[70%] gap-2'>
                <Image
                  source={require("../../assets/icons/noResult.png")}
                  style={{ width: 130, height: 130 }}
                />
                <Text className='text-center paragraph-medium'>
                  No recipes found! {"\n"}Time to create your own masterpiece!
                </Text>
              </View>
            )}
          </View>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

export default Community;
