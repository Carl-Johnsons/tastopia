import React, { useEffect, useRef, useState } from "react";
import {
  Text,
  View,
  TextInput,
  TouchableWithoutFeedback,
  Keyboard,
  Alert,
  TouchableOpacity,
  FlatList,
  RefreshControl
} from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import Feather from "@expo/vector-icons/Feather";
import { Filter } from "@/components/common/SVG";
import { globalStyles } from "@/components/common/GlobalStyles";
import useDebounce from "@/hooks/useDebounce";
import { AntDesign } from "@expo/vector-icons";
import useDarkMode from "@/hooks/useDarkMode";
import { getAPIUrl } from "@/utils/fetch";
import User from "@/components/common/User";
import { Image } from "expo-image";
import { selectAccessToken } from "@/slices/auth.slice";

const Search = () => {
  const [skip, setSkip] = useState<number>(0);
  const [onFocus, setOnFocus] = useState<boolean>(false);
  const [hasNextPage, setHasNextPage] = useState<boolean>();
  const [searchValue, setSearchValue] = useState<string>("");
  const [isSearching, setIsSearching] = useState<boolean>(false);
  const [isRefreshing, setIsRefreshing] = useState(false);
  const [searchResults, setSearchResults] = useState<SearchUserResultProps[]>([]);
  const [doneSearching, setDoneSearching] = useState<boolean>(false);

  const textInputRef = useRef<TextInput>(null);

  const isDarkMode = useDarkMode();

  const debouncedValue = useDebounce(searchValue, 800);
  const accessToken = selectAccessToken();

  const fetchSearchResults = async (isFetchMore: boolean) => {
    if (isFetchMore && !hasNextPage) return;

    setIsSearching(true);
    const url = getAPIUrl(5003, "api/user/search");
    const headers = {
      "Content-Type": "application/json",
      Authorization: `Bearer ${accessToken}`
    };

    console.log("keyword", searchValue);
    console.log("skip", skip);
    const body = JSON.stringify({
      keyword: debouncedValue,
      skip: parseInt(skip.toString())
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

      console.log("result", data.paginatedData);

      setSearchResults(data.paginatedData);
      setHasNextPage(data.metadata.hasNextPage);
    } catch (error) {
      console.log("error", error);
    } finally {
      setIsSearching(false);
      setDoneSearching(true);
    }
  };

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
    setDoneSearching(false);
    setSearchValue("");
    setSearchResults([]);
    Keyboard.dismiss();
  };

  const handleSearch = (text: string) => {
    setSearchValue(text);
  };

  const onRefresh = () => {};

  const handleLoadMore = () => {};

  useEffect(() => {
    if (!debouncedValue.trim()) {
      setSearchResults([]);
      return;
    }

    fetchSearchResults(false);
  }, [debouncedValue]);

  useEffect(() => {
    setDoneSearching(false);
  }, [searchValue]);

  const handleFollowUnFollow = () => {};

  return (
    <TouchableWithoutFeedback onPress={() => handleFocus(false)}>
      <SafeAreaView
        style={{
          paddingHorizontal: 16,
          backgroundColor: globalStyles.color.light,
          height: "100%"
        }}
      >
        <View>
          <Text className='text-center base-medium'>Search Users</Text>

          {/* Search input */}
          <View className='flex-row items-center gap-5 mt-4'>
            <TouchableWithoutFeedback onPress={() => handleFocus(true)}>
              <View className='relative h-[40px] flex-1 flex-row items-center gap-2 rounded-3xl border-[0.5px] border-gray-300 px-3'>
                <Feather
                  name='search'
                  size={20}
                  color='black'
                />
                <TextInput
                  autoCapitalize='none'
                  ref={textInputRef}
                  className='flex-1 h-full'
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

          {/* Result section */}
          <View className='mt-6'>
            <Text className='mb-2 h3-bold'>Users</Text>

            <FlatList
              data={searchResults}
              keyExtractor={item => item.username}
              refreshControl={
                <RefreshControl
                  refreshing={isRefreshing}
                  tintColor={"#fff"}
                  onRefresh={onRefresh}
                />
              }
              onEndReached={handleLoadMore}
              onEndReachedThreshold={0.1}
              renderItem={({ item, index }) => (
                <>
                  <User {...item} />
                  {index !== searchResults.length - 1 && (
                    <View className='my-4 h-[1px] w-full bg-gray-300' />
                  )}
                </>
              )}
              ListEmptyComponent={() => {
                return doneSearching ? (
                  <View className='gap-2 flex-center'>
                    <Image
                      source={require("../../../assets/icons/noResult.png")}
                      style={{ width: 130, height: 130 }}
                    />
                    <Text className='text-center paragraph-medium'>No users found!</Text>
                  </View>
                ) : (
                  <View></View>
                );
              }}
            />
            {/* <User
              id='asdf'
              avtUrl='https://imgcdn.stablediffusionweb.com/2024/3/4/97bf4ec3-8c92-4f5a-992c-18bc59e35bd5.jpg'
              displayName='Vuong'
              username='quoczuong'
              numberOfRecipe={1}
              isFollowing={true}
            /> */}
          </View>
        </View>
      </SafeAreaView>
    </TouchableWithoutFeedback>
  );
};

export default Search;
