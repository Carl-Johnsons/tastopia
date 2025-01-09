import React, { useEffect, useRef, useState } from "react";
import {
  Text,
  View,
  TextInput,
  TouchableWithoutFeedback,
  Keyboard,
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
import User from "@/components/common/User";
import { Image } from "expo-image";
import { useSearchUsers } from "@/api/search";
import Tag from "@/components/common/Tag";
import { filterUniqueItems } from "@/utils/dataFilter";

type SearchUserProps = {
  onFocus: boolean;
  setOnFocus: React.Dispatch<React.SetStateAction<boolean>>;
};

const SearchUser = ({ onFocus, setOnFocus }: SearchUserProps) => {
  const [searchValue, setSearchValue] = useState<string>("");
  const [isRefreshing, setIsRefreshing] = useState(false);
  const [searchResults, setSearchResults] = useState<SearchUserResultType[]>();

  const textInputRef = useRef<TextInput>(null);
  const isDarkMode = useDarkMode();
  const debouncedValue = useDebounce(searchValue, 800);

  const {
    data,
    isFetched,
    hasNextPage,
    isFetchingNextPage,
    isLoading: isSearching,
    refetch,
    fetchNextPage
  } = useSearchUsers(debouncedValue);

  const [doneSearching, setDoneSearching] = useState(!isSearching);
  const shouldShowNoResults = isFetched && doneSearching;

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
    setDoneSearching(false);
    setIsRefreshing(false);
    Keyboard.dismiss();
  };

  const handleSearch = (text: string) => {
    setSearchValue(text);
  };

  const onRefresh = async () => {
    setIsRefreshing(true);
    await refetch();
    setIsRefreshing(false);
  };

  const handleLoadMore = () => {
    if (hasNextPage && !isFetchingNextPage) {
      fetchNextPage();
    }
  };

  const handleOnPressTag = () => {};

  useEffect(() => {
    if (data?.pages) {
      const uniqueData = filterUniqueItems(data.pages);
      setSearchResults(uniqueData);
    }
  }, [data]);

  return (
    <View>
      {/* Search input */}
      <View className='mt-4 flex-row items-center gap-5'>
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
              className='h-full w-[86%]'
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

      {/* <Tag handleOnPress={handleOnPressTag} /> */}

      {/* Result section */}
      <View className='mt-6 pb-[200px]'>
        {searchResults !== undefined && searchResults.length > 0 && doneSearching && (
          <Text className='h3-bold mb-2'>Users</Text>
        )}

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
          showsVerticalScrollIndicator={false}
          renderItem={({ item, index }) => (
            <>
              <User {...item} />
              {searchResults !== undefined && index !== searchResults.length - 1 && (
                <View className='my-4 h-[1px] w-full bg-gray-300' />
              )}
            </>
          )}
          ListEmptyComponent={() => {
            return shouldShowNoResults ? (
              <View className='flex-center mt-10 gap-2'>
                <Image
                  source={require("../../../assets/icons/noResult.png")}
                  style={{ width: 130, height: 130 }}
                />
                <Text className='paragraph-medium text-center'>No users found!</Text>
              </View>
            ) : (
              <View></View>
            );
          }}
        />
      </View>
    </View>
  );
};

export default SearchUser;
