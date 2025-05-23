import React, { useCallback, useEffect, useRef, useState } from "react";
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
import Feather from "@expo/vector-icons/Feather";
import { globalStyles } from "@/components/common/GlobalStyles";
import useDebounce from "@/hooks/useDebounce";
import { AntDesign } from "@expo/vector-icons";
import useDarkMode from "@/hooks/useDarkMode";
import User from "@/components/common/User";
import { Image } from "expo-image";
import {
  createUserSearchUserKeyword,
  useSearchUserHistory,
  useSearchUsers
} from "@/api/search";
import { filterUniqueItems } from "@/utils/dataFilter";
import { useTranslation } from "react-i18next";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";
import { useQueryClient } from "react-query";
import uuid from "react-native-uuid";
import SearchHistory from "./SearchHistory";
import { useErrorHandler } from "@/hooks/useErrorHandler";

type SearchUserProps = {
  onFocus: boolean;
  setOnFocus: React.Dispatch<React.SetStateAction<boolean>>;
};

const SearchUser = ({ onFocus, setOnFocus }: SearchUserProps) => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const { t } = useTranslation("search");
  const { handleError } = useErrorHandler();

  const queryClient = useQueryClient();
  const [searchValue, setSearchValue] = useState<string>("");
  const [searchResults, setSearchResults] = useState<SearchUserResultType[]>();

  const textInputRef = useRef<TextInput>(null);
  const isDarkMode = useDarkMode();
  const debouncedValue = useDebounce(searchValue, 800);

  const { data: searchUserHistoryData, isLoading: isLoadingSearchUserHistory } =
    useSearchUserHistory();
  const { mutateAsync: createSearchHistory } = createUserSearchUserKeyword();
  const {
    data,
    isFetched,
    hasNextPage,
    isRefetching,
    isFetchingNextPage,
    isLoading: isSearching,
    refetch,
    fetchNextPage
  } = useSearchUsers(debouncedValue);

  const isDoneSearching =
    searchValue !== "" &&
    debouncedValue !== "" &&
    isFetched &&
    !isRefetching &&
    !isSearching &&
    !isFetchingNextPage;

  const handleFocus = useCallback(
    (isFocus: boolean) => {
      textInputRef.current?.focus();
      setOnFocus(true);
      if (!isFocus) {
        Keyboard.dismiss();
      }
    },
    [setOnFocus]
  );

  const handleCancel = useCallback(() => {
    setSearchValue("");
    setSearchResults([]);
    setOnFocus(false);
    Keyboard.dismiss();
  }, [setOnFocus]);

  const handleSearch = useCallback((text: string) => {
    setSearchValue(text);
    setSearchResults(undefined);
  }, []);

  const onRefresh = useCallback(async () => {
    await refetch();
  }, [refetch]);

  const handleLoadMore = useCallback(() => {
    if (hasNextPage && !isFetchingNextPage) {
      fetchNextPage();
    }
  }, [hasNextPage, isFetchingNextPage, fetchNextPage]);

  const invalidateSearch = () => {
    queryClient.invalidateQueries({ queryKey: ["searchUsers", debouncedValue] });
  };

  const handleSelectSearchHistory = (item: string) => {
    setSearchValue(item);
  };

  const handleSelectSearchResult = () => {
    createSearchHistory(
      { keyword: searchValue },
      {
        onError: error => handleError(error)
      }
    );
  };

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
              color={c(black.DEFAULT, white.DEFAULT)}
            />
            <TextInput
              testID='search-user-input'
              autoCapitalize='none'
              ref={textInputRef}
              className='h-full w-[86%]'
              style={{ color: c(black.DEFAULT, white.DEFAULT) }}
              value={searchValue}
              onPress={() => handleFocus(true)}
              onChangeText={handleSearch}
              placeholder={t("placeholder.user")}
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
      </View>

      {/* History section */}
      {!isLoadingSearchUserHistory &&
        searchUserHistoryData?.value &&
        searchUserHistoryData?.value.length > 0 &&
        searchValue === "" && (
          <FlatList
            data={searchUserHistoryData.value.slice(0, 10)}
            keyExtractor={_item => uuid.v4()}
            scrollEnabled={false}
            showsVerticalScrollIndicator={false}
            style={{
              marginTop: 20,
              marginLeft: 10
            }}
            contentContainerStyle={{
              gap: 10
            }}
            renderItem={item => {
              return (
                <SearchHistory
                  type='user'
                  item={item.item}
                  handleSelectHistory={handleSelectSearchHistory}
                />
              );
            }}
          />
        )}

      {/* Result section */}
      {searchValue !== "" && (
        <View className='mt-6 pb-[40px]'>
          <FlatList
            data={searchResults}
            keyExtractor={item => item.username}
            refreshControl={
              <RefreshControl
                refreshing={isRefetching}
                tintColor={globalStyles.color.primary}
                onRefresh={onRefresh}
              />
            }
            onEndReached={handleLoadMore}
            onEndReachedThreshold={0.5}
            initialNumToRender={10}
            maxToRenderPerBatch={10}
            windowSize={11}
            removeClippedSubviews
            showsVerticalScrollIndicator={false}
            contentContainerStyle={{ paddingBottom: 120 }}
            ListHeaderComponent={() => {
              return (
                <Text className='h3-bold text-black_white mb-2'>
                  {t("searchResultTitle.user")}
                </Text>
              );
            }}
            renderItem={({ item, index }) => (
              <>
                <User
                  {...item}
                  invalidateSearch={invalidateSearch}
                  handleSelectSearchResult={handleSelectSearchResult}
                />
                {searchResults !== undefined && index !== searchResults.length - 1 && (
                  <View className='my-4 h-[1px] w-full bg-gray-300' />
                )}
              </>
            )}
            ListEmptyComponent={() => {
              return isDoneSearching && searchResults?.length === 0 ? (
                <View className='flex-center mt-10 gap-2'>
                  <Image
                    source={require("../../../assets/icons/noResult.png")}
                    style={{ width: 130, height: 130 }}
                  />
                  <Text className='paragraph-medium text-black_white text-center'>
                    {t("notFound")}
                  </Text>
                </View>
              ) : (
                <View></View>
              );
            }}
          />
        </View>
      )}
    </View>
  );
};

export default SearchUser;
