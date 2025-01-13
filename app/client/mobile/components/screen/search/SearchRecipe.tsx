import React, { memo, useCallback, useEffect, useRef, useState } from "react";
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
import { Filter } from "@/components/common/SVG";
import { globalStyles } from "@/components/common/GlobalStyles";
import useDebounce from "@/hooks/useDebounce";
import { AntDesign } from "@expo/vector-icons";
import useDarkMode from "@/hooks/useDarkMode";
import { Image } from "expo-image";
import { useSearchRecipes } from "@/api/search";
import { router, useFocusEffect } from "expo-router";
import Recipe from "@/components/common/Recipe";
import { filterUniqueItems } from "@/utils/dataFilter";
import {
  addKeyword,
  selectSearchKeyword,
  selectSearchTagCodes
} from "@/slices/searchRecipe.slice";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import { useTranslation } from "react-i18next";

type SearchUserProps = {
  onFocus: boolean;
  setOnFocus: React.Dispatch<React.SetStateAction<boolean>>;
};

const SearchRecipe = ({ onFocus, setOnFocus }: SearchUserProps) => {
  const { t } = useTranslation("search");
  const dispatch = useAppDispatch();
  const tagCodes = useAppSelector(selectSearchTagCodes);
  const searchKeyword = useAppSelector(selectSearchKeyword);

  const [searchValue, setSearchValue] = useState<string>(searchKeyword);
  const [searchResults, setSearchResults] = useState<SearchRecipeType[]>();

  const textInputRef = useRef<TextInput>(null);
  const isDarkMode = useDarkMode();
  const debouncedValue = useDebounce(searchValue, 800);

  const {
    data,
    isFetched,
    isRefetching,
    hasNextPage,
    isFetchingNextPage,
    isLoading: isSearching,
    refetch,
    fetchNextPage
  } = useSearchRecipes(debouncedValue, tagCodes);

  const isDoneSearching =
    searchValue !== "" &&
    debouncedValue !== "" &&
    isFetched &&
    !isRefetching &&
    !isSearching &&
    !isFetchingNextPage;

  const handleFilter = () => {
    router.navigate("/(modals)/SearchFilter");
  };

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
    dispatch(addKeyword(""));
    setSearchResults([]);
    setOnFocus(false);
    Keyboard.dismiss();
  }, [dispatch, setOnFocus]);

  const handleSearch = useCallback(
    (text: string) => {
      setSearchValue(text);
      dispatch(addKeyword(text));
    },
    [dispatch]
  );

  const onRefresh = useCallback(async () => {
    await refetch();
  }, [refetch]);

  const handleLoadMore = useCallback(() => {
    if (hasNextPage && !isFetchingNextPage) {
      fetchNextPage();
    }
  }, [hasNextPage, isFetchingNextPage, fetchNextPage]);

  useEffect(() => {
    if (data?.pages && searchKeyword !== "" && searchValue !== "") {
      const uniqueData = filterUniqueItems(data.pages);
      setSearchResults(uniqueData);
    }
  }, [data]);

  useFocusEffect(
    useCallback(() => {
      if (searchKeyword !== "" && searchValue !== "") {
        refetch();
      }

      return () => {};
    }, [searchKeyword])
  );

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
              placeholder={t("placeholder.recipe")}
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
      {searchValue !== "" && (
        <View className='mt-6 pb-[200px]'>
          <Text className='h3-bold mb-2'>{t("searchResultTitle.recipe")}</Text>

          <FlatList
            data={searchResults}
            keyExtractor={item => item.id}
            refreshControl={
              <RefreshControl
                refreshing={isRefetching}
                tintColor={globalStyles.color.primary}
                onRefresh={onRefresh}
              />
            }
            onEndReached={handleLoadMore}
            onEndReachedThreshold={0.1}
            showsVerticalScrollIndicator={false}
            renderItem={({ item, index }) => (
              <>
                <Recipe {...item} />
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
                  <Text className='paragraph-medium text-center'>{t("notFound")}</Text>
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

export default memo(SearchRecipe);
