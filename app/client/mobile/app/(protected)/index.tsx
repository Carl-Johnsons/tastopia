import { globalStyles } from "@/components/common/GlobalStyles";
import Input from "@/components/common/Input";
import Recipe from "@/components/common/Recipe";
import { LogoIcon } from "@/components/common/SVG";
import i18next from "i18next";
import React, { useState } from "react";
import {
  Text,
  View,
  Button,
  ScrollView,
  RefreshControl,
  Image,
  TouchableWithoutFeedback,SafeAreaView,
} from "react-native";

const Community = () => {
  const [isLoading, setIsLoading] = useState(false);

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

  const handleCreateRecipe = () => {
    console.log("show modal create recipe");
  };
  return (
    <SafeAreaView style={{backgroundColor: globalStyles.color.light}}>
      <ScrollView
        refreshControl={
          <RefreshControl
            progressViewOffset={-10000}
            refreshing={isLoading}
            tintColor={"#fff"}
            onRefresh={onRefresh}
            
          />
        }
      >
        <View className='gap-8 px-4 size-full'>
          <View className='flex-center'>
            <LogoIcon
              isActive={isLoading}
              width={60}
              height={60}
            />
          </View>

          <View className='flex-row px-6 flex-start'>
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
            <Recipe
              id='1221123'
              title='Chicken Hawaiian'
              description='Chicken, Cheese and pineapple'
              imageUrl='YOUR_IMAGE_URL'
              username='vuong'
              avatar='link of the image'
              votes={777}
              comments={777}
              onPress={() => {
                console.log("click recipe");
              }}
            />
            <Recipe
              id='1221123'
              title='Chicken Hawaiian'
              description='Chicken, Cheese and pineapple'
              imageUrl='YOUR_IMAGE_URL'
              username='vuong'
              avatar='link of the image'
              votes={777}
              comments={777}
              onPress={() => {
                console.log("click recipe");
              }}
            />
            <Recipe
              id='1221123'
              title='Chicken Hawaiian'
              description='Chicken, Cheese and pineapple'
              imageUrl='YOUR_IMAGE_URL'
              username='vuong'
              avatar='link of the image'
              votes={777}
              comments={777}
              onPress={() => {
                console.log("click recipe");
              }}
            />
          </View>

          <Button
            title={"Toggle language"}
            onPress={() => toggleLanguage()}
          ></Button>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

export default Community;
