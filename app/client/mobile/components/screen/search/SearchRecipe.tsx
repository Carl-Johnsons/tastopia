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
import { AntDesign, Feather } from "@expo/vector-icons";
import useDarkMode from "@/hooks/useDarkMode";
import { Image } from "expo-image";
import {
  createUserSearchRecipeKeyword,
  useSearchRecipeHistory,
  useSearchRecipes
} from "@/api/search";
import { router } from "expo-router";
import { filterUniqueItems } from "@/utils/dataFilter";
import { removeTagValue, selectSearchTagCodes, selectSearchTagValues, selectSearchTags } from "@/slices/searchRecipe.slice";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import { useTranslation } from "react-i18next";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";
import SearchRecipeResult from "../community/SearchRecipeResult";
import SearchHistory from "./SearchHistory";
import uuid from "react-native-uuid";
import { SearchRecipeType } from "@/types/recipe";
import { globalStyles } from "@/components/common/GlobalStyles";
import useDebounce from "@/hooks/useDebounce";
import SelectedTag from "./SelectedTag";
import { Filter } from "@/components/common/SVG";

type SearchUserProps = {
  onFocus: boolean;
  setOnFocus: React.Dispatch<React.SetStateAction<boolean>>;
};

type ResultSectionProps = {
  searchResults: SearchRecipeType[] | undefined;
  isRefetching: boolean;
  isDoneSearching: boolean;
  onRefresh: () => Promise<void>;
  handleLoadMore: () => Promise<void>;
  handleSelectSearchResult: () => void;
};

const ResultSection = memo(
  ({
    searchResults,
    isRefetching,
    isDoneSearching,
    onRefresh,
    handleLoadMore,
    handleSelectSearchResult
  }: ResultSectionProps) => {
    const { t } = useTranslation("search");

    return (
      <View className='mt-6 mb-[50px]'>
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
          contentContainerStyle={{ paddingBottom: 160 }}
          ListHeaderComponent={() => {
            return (
              <Text className='h3-bold text-black_white mb-2'>
                {t("searchResultTitle.recipe")}
              </Text>
            );
          }}
          renderItem={({ item, index }) => (
            <>
              <SearchRecipeResult
                {...item}
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
    );
  }
);

const SearchRecipe = ({ onFocus, setOnFocus }: SearchUserProps) => {
  const { c } = useColorizer();
  const { black, white } = colors;

  const { t } = useTranslation("search");
  const dispatch = useAppDispatch();
  const tagCodes = useAppSelector(selectSearchTagCodes);
  const tagValues = useAppSelector(selectSearchTagValues);

  const selectedTagsStore = useAppSelector(selectSearchTags);

  const [searchValue, setSearchValue] = useState<string>("");
  const [searchResults, setSearchResults] = useState<SearchRecipeType[]>();
  const [selectedTags, setSelectedTags] = useState<SelectedTag[]>(selectedTagsStore);

  const textInputRef = useRef<TextInput>(null);
  const isDarkMode = useDarkMode();
  const debouncedValue = useDebounce(searchValue, 800);

  const { mutateAsync: createSearchHistory } = createUserSearchRecipeKeyword();
  const { data: searchRecipeHistoryData, isLoading: isLoadingSearchRecipeHistory } =
    useSearchRecipeHistory();
  const {
    data,
    isFetched,
    isRefetching,
    hasNextPage,
    isFetchingNextPage,
    isLoading: isSearching,
    refetch,
    fetchNextPage
  } = useSearchRecipes(debouncedValue, tagCodes, tagValues);

  const isDoneSearching =
    ((searchValue !== "" && debouncedValue !== "") || tagCodes.length > 0) &&
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
    setOnFocus(false);
    Keyboard.dismiss();
    if (tagCodes.length <= 0) setSearchResults([]);
  }, [dispatch, setOnFocus]);

  const handleSearch = useCallback(
    (text: string) => {
      setSearchValue(text);
    },
    [dispatch]
  );

  const onRefresh = useCallback(async () => {
    await refetch();
  }, [refetch]);

  const handleLoadMore = useCallback(async () => {
    if (hasNextPage && !isFetchingNextPage) {
      fetchNextPage();
    }
  }, [hasNextPage, isFetchingNextPage, fetchNextPage]);

  const handleSelectSearchHistory = (item: string) => {
    setSearchValue(item);
  };

  const handleSelectSearchResult = () => {
    createSearchHistory({ keyword: searchValue });
  };

  useEffect(() => {
    if (data?.pages && (searchValue !== "" || tagCodes.length > 0)) {
      const uniqueData = filterUniqueItems(data.pages);
      setSearchResults(uniqueData);
      refetch();
    }
  }, [data, tagCodes]);

  // useFocusEffect(
  //   useCallback(() => {
  //     if (searchValue !== "" || tagCodes.length > 0) {
  //       refetch();
  //     }

  //     return () => {};
  //   }, [])
  // );

  const handleRemoveTag = useCallback((code: string) => {
    dispatch(removeTagValue(code));
    setSelectedTags(prev => prev.filter(t => t.code !== code));
  }, []);

  useEffect(() => {
    setSelectedTags(selectedTagsStore);
  }, [selectedTagsStore]);

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
              autoCapitalize='none'
              ref={textInputRef}
              className='h-full w-[86%]'
              style={{ color: c(black.DEFAULT, white.DEFAULT) }}
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

      {/* Selected tags */}
      <View>
        <View className='mt-4 flex-row flex-wrap'>
          {selectedTags.map((tag, index) => {
            return (
              <SelectedTag
                key={`${tag}-${index}`}
                code={tag.code}
                value={tag.value}
                onRemove={handleRemoveTag}
              />
            );
          })}
        </View>
      </View>
      {/* History section */}
      {!isLoadingSearchRecipeHistory && searchRecipeHistoryData && searchValue === "" && (
        <FlatList
          data={searchRecipeHistoryData.value.slice(0, 10)}
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
                type='recipe'
                item={item.item}
                handleSelectHistory={handleSelectSearchHistory}
              />
            );
          }}
        />
      )}

      {/* Result section */}
      {(searchValue !== "" || tagCodes.length > 0) && (
        <ResultSection
          searchResults={searchResults}
          isDoneSearching={isDoneSearching}
          isRefetching={isRefetching}
          onRefresh={onRefresh}
          handleLoadMore={handleLoadMore}
          handleSelectSearchResult={handleSelectSearchResult}
        />
      )}
    </View>
  );
};

export default memo(SearchRecipe);
