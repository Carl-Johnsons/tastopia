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

const Search = () => {
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
        // TODO: fix padding search result
        style={{
          marginTop: 10,
          marginBottom: 90,
          paddingHorizontal: 16,
          backgroundColor: globalStyles.color.light,
          height: "100%"
        }}
      >
        <TouchableOpacity
          onPress={handleChangeSearch}
          activeOpacity={1}
          style={{ backgroundColor: "transparent" }}
        >
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
        </TouchableOpacity>
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
