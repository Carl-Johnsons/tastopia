import { useState } from "react";
import AntDesign from "@expo/vector-icons/AntDesign";
import { SafeAreaView } from "react-native-safe-area-context";
import { globalStyles } from "@/components/common/GlobalStyles";
import SearchUser from "@/components/screen/search/SearchUser";
import SearchRecipe from "@/components/screen/search/SearchRecipe";
import { Text, TouchableWithoutFeedback, Keyboard, View, StatusBar } from "react-native";
import { useTranslation } from "react-i18next";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";

const Search = () => {
  const { c } = useColorizer();
  const { black, white } = colors;

  const { t } = useTranslation("search");
  const [onFocus, setOnFocus] = useState<boolean>(false);
  const [isSearchingUser, setIsSearchingUser] = useState(false);

  const handleFocus = (isFocus: boolean) => {
    setOnFocus(true);
    if (!isFocus) {
      Keyboard.dismiss();
    }
  };

  const handleChangeSearch = () => {
    setIsSearchingUser(prev => !prev);
  };

  return (
    <TouchableWithoutFeedback onPress={() => handleFocus(false)}>
      <SafeAreaView
        style={{
          marginBottom: 90,
          paddingHorizontal: 16,
          backgroundColor: c(white.DEFAULT, black[100]),
          height: "100%"
        }}
      >
        <StatusBar backgroundColor={c(white.DEFAULT, black[100])} />
        <View className='flex-center pt-2'>
          <TouchableWithoutFeedback onPress={handleChangeSearch}>
            <View className='flex-center flex-row gap-2'>
              <Text className='base-medium text-black_white text-center'>
                {isSearchingUser ? t("header.user") : t("header.recipe")}
              </Text>
              <AntDesign
                name='caretdown'
                size={10}
                color={globalStyles.color.gray400}
              />
            </View>
          </TouchableWithoutFeedback>
        </View>
        {isSearchingUser ? (
          <SearchUser
            onFocus={onFocus}
            setOnFocus={setOnFocus}
          />
        ) : (
          <SearchRecipe
            onFocus={onFocus}
            setOnFocus={setOnFocus}
          />
        )}
      </SafeAreaView>
    </TouchableWithoutFeedback>
  );
};

export default Search;
