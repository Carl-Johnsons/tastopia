import React, { useEffect, useRef, useState } from "react";
import {
  Text,
  View,
  TextInput,
  TouchableWithoutFeedback,
  Keyboard,
  Alert,
  Button,
  TouchableOpacity,
  Touchable
} from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import Feather from "@expo/vector-icons/Feather";
import { Filter } from "@/components/common/SVG";
import { globalStyles } from "@/components/common/GlobalStyles";
import useDebounce from "@/hooks/useDebounce";
import { AntDesign } from "@expo/vector-icons";
import useDarkMode from "@/hooks/useDarkMode";

const Search = () => {
  const [searchValue, setSearchValue] = useState<string>("");
  const [isSearching, setIsSearching] = useState<boolean>(false);
  const [searchResults, setSearchResults] = useState<SearchUserResultProps[]>();
  const [doneSearching, setDoneSearching] = useState<boolean>(false);
  const [onFocus, setOnFocus] = useState<boolean>(false);

  const textInputRef = useRef<TextInput>(null);

  const isDarkMode = useDarkMode();

  const debouncedValue = useDebounce(searchValue, 800);
  const handleFilter = () => {};

  const handleFocus = (isFocus: boolean) => {
    textInputRef.current?.focus();
    setOnFocus(true);
    if (!isFocus) {
      Keyboard.dismiss();
    }
  };

  const handleCancel = () => {
    setOnFocus(false);
    setSearchValue("");
    setSearchResults([]);
    Keyboard.dismiss();
  };

  const handleSearch = (text: string) => {
    setSearchValue(text);
  };

  useEffect(() => {
    if (!debouncedValue.trim()) {
      setSearchResults([]);
      return;
    }

    const fetchSearchResults = async () => {
      setIsSearching(true);
      try {
        await new Promise(resolve => setTimeout(resolve, 500));
      } catch (error) {
        Alert.alert("Error", "Something went wrong while searching.");
      } finally {
        setIsSearching(false);
        setDoneSearching(true);
      }
    };

    fetchSearchResults();
  }, [debouncedValue]);

  useEffect(() => {
    setDoneSearching(false);
  }, [searchValue]);

  const handleFollowUnFollow = () => {};

  console.log("isSearching", isSearching);

  return (
    <TouchableWithoutFeedback onPress={() => handleFocus(false)}>
      <SafeAreaView>
        <View>
          <Text className='base-medium text-center'>Search Users</Text>

          <View className='mt-4 flex-row items-center gap-5 px-6'>
            <TouchableWithoutFeedback onPress={() => handleFocus(true)}>
              <View className='relative h-[40px] flex-1 flex-row items-center gap-2 rounded-3xl border-[0.5px] border-gray-300 px-3'>
                <Feather
                  name='search'
                  size={20}
                  color='black'
                />
                <TextInput
                  ref={textInputRef}
                  className='h-full flex-1'
                  value={searchValue}
                  onPress={() => handleFocus(true)}
                  onChangeText={handleSearch}
                  placeholder='Enter name or username'
                  placeholderTextColor={globalStyles.color.gray400}
                />
                {onFocus && searchValue && isSearching && (
                  <View className='absolute right-3 top-3.5'>
                    <AntDesign
                      className='animate-spin'
                      name='loading2'
                      size={16}
                      color={isDarkMode ? "#A9A9A9" : "#6B7280"}
                    />
                  </View>
                )}
                {onFocus && searchValue && !isSearching && (
                  <View className='absolute right-3 top-3.5'>
                    <TouchableOpacity
                      onPress={handleCancel}
                      activeOpacity={1}
                    >
                      <AntDesign
                        name='closecircleo'
                        size={16}
                        color={isDarkMode ? "#A9A9A9" : "#6B7280"}
                      />
                    </TouchableOpacity>
                  </View>
                )}
              </View>
            </TouchableWithoutFeedback>
            <TouchableWithoutFeedback onPress={handleFilter}>
              <Filter />
            </TouchableWithoutFeedback>
          </View>
        </View>
      </SafeAreaView>
    </TouchableWithoutFeedback>
  );
};

export default Search;
