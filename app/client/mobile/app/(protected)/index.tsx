import { globalStyles } from "@/components/common/GlobalStyles";
import Input from "@/components/common/Input";
import Recipe from "@/components/common/Recipe";
import { LogoIcon } from "@/components/common/SVG";
import Filter from "@/components/screen/community/Filter";
import i18next from "i18next";
import React, { useEffect, useState } from "react";
import {
  Text,
  View,
  Button,
  ScrollView,
  RefreshControl,
  Image,
  TouchableWithoutFeedback,
  SafeAreaView
} from "react-native";

const Community = () => {
  const [skip, setSkip] = useState<number>(0);
  const [isLoading, setIsLoading] = useState(false);
  const [filterSelected, setFilterSelected] = useState<string>("All");
  const [recipes, setRecipes] = useState<RecipeType[]>();

  const toggleLanguage = () => {
    const currentLang = i18next.language;
    const newLang = currentLang === "en" ? "vi" : "en";
    i18next.changeLanguage(newLang);
  };

  const onRefresh = () => {
    setIsLoading(true);

    setTimeout(() => {
      setIsLoading(false);
    }, 2000);
  };

  useEffect(() => {
    async function getRecipeFeed() {
      const url = "http://localhost:5005/api/recipe/get-recipe-feed";

      const headers = {
        "Content-Type": "application/json"
      };

      const body = JSON.stringify({
        skip: skip.toString(),
        tagValues: filterSelected
      });

      try {
        const response = await fetch(url, {
          method: "GET",
          headers: headers,
          body: body
        });

        if (!response.ok) {
          const errorText = await response.text();
          throw new Error(
            `HTTP error! status: ${response.status}, message: ${errorText}`
          );
        }

        const data = await response.json();

        console.log("data.paginatedData", data.paginatedData);

        setRecipes(prev => [prev, data.paginatedData]);
      } catch (error) {
        console.error("Error fetching recipe feed:", error);
        throw error;
      }
    }

    // getRecipeFeed();
  });

  const handleCreateRecipe = () => {
    console.log("show modal create recipe");
  };

  const handleFilter = (key: string) => {
    console.log(key);
  };
  return (
    <SafeAreaView style={{ backgroundColor: globalStyles.color.light }}>
      <ScrollView
        refreshControl={
          <RefreshControl
            refreshing={isLoading}
            tintColor={"#fff"}
            onRefresh={onRefresh}
          />
        }
      >
        <View className='gap-8 px-4 pt-2 size-full'>
          <View className='flex-center'>
            <LogoIcon
              isActive={isLoading}
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
            {recipes?.map(recipe => {
              return (
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
              );
            })}

            <View className='h-[1px] w-full bg-gray-300' />
          </View>

          <View className='bottom'></View>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

export default Community;
