import { useState } from "react";
import AntDesign from "@expo/vector-icons/AntDesign";
import { SafeAreaView } from "react-native-safe-area-context";
import { globalStyles } from "@/components/common/GlobalStyles";
import SearchUser from "@/components/screen/search/SearchUser";
import SearchRecipe from "@/components/screen/search/SearchRecipe";
import {
  Text,
  TouchableWithoutFeedback,
  Keyboard,
  TouchableOpacity,
  View
} from "react-native";
import useDarkMode from "@/hooks/useDarkMode";

const Search = () => {
  const isDarkMode = useDarkMode();
  const [onFocus, setOnFocus] = useState<boolean>(false);
  const [isSearchingUser, setIsSearchingUser] = useState(true);

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
          backgroundColor: isDarkMode
            ? globalStyles.color.dark
            : globalStyles.color.light,
          height: "100%"
        }}
      >
        <View className='flex-center'>
          <TouchableWithoutFeedback onPress={handleChangeSearch}>
            <View className='flex-center flex-row gap-2'>
              <Text className='base-medium text-black_white text-center'>
                {isSearchingUser ? "Search Users" : "Search Recipe"}
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
