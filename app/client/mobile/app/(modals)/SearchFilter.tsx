import { useSearchTags } from "@/api/search";
import { globalStyles } from "@/components/common/GlobalStyles";
import Ingredient from "@/components/screen/search/Ingredient";
import SelectedTag from "@/components/screen/search/SelectedTag";
import { colors } from "@/constants/colors";
import { SETTING_VALUE } from "@/constants/settings";
import useColorizer from "@/hooks/useColorizer";
import useDarkMode from "@/hooks/useDarkMode";
import useDebounce from "@/hooks/useDebounce";
import {
  clearTagValue,
  removeTagValue,
  selectSearchTags
} from "@/slices/searchRecipe.slice";
import { selectLanguageSetting } from "@/slices/setting.slice";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import { filterUniqueItems } from "@/utils/dataFilter";
import { AntDesign, Feather } from "@expo/vector-icons";
import { router } from "expo-router";
import { useCallback, useEffect, useRef, useState } from "react";
import { useTranslation } from "react-i18next";
import {
  FlatList,
  Keyboard,
  RefreshControl,
  StatusBar,
  Text,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View,
  Image,
  Alert,
  SafeAreaView
} from "react-native";

const SearchFilter = () => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const language = selectLanguageSetting();
  const currentLanguage = language === SETTING_VALUE.LANGUAGE.VIETNAMESE ? "vi" : "en";

  const { t } = useTranslation("search");
  const dispatch = useAppDispatch();
  const selectedTagsStore = useAppSelector(selectSearchTags);

  const [searchValue, setSearchValue] = useState<string>("");
  const [searchResults, setSearchResults] = useState<TagType[]>();
  const [selectedTags, setSelectedTags] = useState<SelectedTag[]>(selectedTagsStore);

  const textInputRef = useRef<TextInput>(null);
  const isDarkMode = useDarkMode();
  const debouncedValue = useDebounce(searchValue, 800);

  const handleFocus = (isFocus: boolean) => {
    textInputRef.current?.focus();
    if (!isFocus) {
      Keyboard.dismiss();
    }
  };

  const {
    data,
    isFetched,
    isRefetching,
    hasNextPage,
    isFetchingNextPage,
    isLoading: isSearching,
    refetch,
    fetchNextPage
  } = useSearchTags(debouncedValue, ["ALL"], "Ingredient");

  const isDoneSearching =
    searchValue !== "" &&
    debouncedValue !== "" &&
    isFetched &&
    !isRefetching &&
    !isSearching &&
    !isFetchingNextPage;

  const handleSearch = (text: string) => {
    setSearchValue(text);
  };

  const handleCancel = useCallback(() => {
    setSearchValue("");
    setSearchResults([]);
    Keyboard.dismiss();
  }, [dispatch]);

  const onRefresh = useCallback(async () => {
    await refetch();
  }, [refetch]);

  const handleLoadMore = useCallback(() => {
    if (hasNextPage && !isFetchingNextPage) {
      fetchNextPage();
    }
  }, [hasNextPage, isFetchingNextPage, fetchNextPage]);

  const handleRemoveTag = useCallback((code: string) => {
    dispatch(removeTagValue(code));
    setSelectedTags(prev => prev.filter(t => t.code !== code));
  }, []);

  const handleCloseModal = () => {
    selectedTags.length > 0
      ? Alert.alert("Cancel Filter", "Your selected ingredients will be dismissed", [
          {
            text: "Cancel"
          },
          {
            text: "OK",
            onPress: () => {
              setSelectedTags([]);
              dispatch(clearTagValue());
              router.back();
            }
          }
        ])
      : router.back();
  };

  const handleBackToSearchScreen = () => {
    router.back();
  };

  useEffect(() => {
    if (data?.pages) {
      const uniqueData = filterUniqueItems(data.pages);
      setSearchResults(uniqueData);
    }
  }, [data]);

  return (
    <SafeAreaView style={{ backgroundColor: c(white.DEFAULT, black[100]) }}>
      <View className={`size-full`}>
        <View
          style={{ marginTop: StatusBar.currentHeight }}
          className='flex-between mb-4 h-[60px] flex-row items-center border-b-[0.6px] border-gray-400 px-6'
        >
          <TouchableWithoutFeedback onPress={handleCloseModal}>
            <View>
              <AntDesign
                name='close'
                size={20}
                color={c(black.DEFAULT, white.DEFAULT)}
              />
            </View>
          </TouchableWithoutFeedback>

          <View className='items-center'>
            <Text className='text-black_white paragraph-medium'>
              {t("header.filter")}
            </Text>
          </View>

          <TouchableWithoutFeedback onPress={handleBackToSearchScreen}>
            <View className='items-center'>
              <Text className='paragraph-medium text-primary'>{t("filter.action")}</Text>
            </View>
          </TouchableWithoutFeedback>
        </View>

        <View className='mt-2 px-6'>
          <Text className='body-semibold text-black_white'>{t("filter.title")}</Text>

          <View className='mt-4 flex-row items-center gap-5'>
            <TouchableWithoutFeedback onPress={() => handleFocus(true)}>
              <View className='relative h-[40px] flex-1 flex-row items-center gap-2 rounded-3xl border-[0.5px] border-gray-300 px-3'>
                <Feather
                  name='search'
                  size={20}
                  color={c(black.DEFAULT, white.DEFAULT)}
                />
                <TextInput
                  autoFocus
                  autoCapitalize='none'
                  ref={textInputRef}
                  className='h-full w-[86%]'
                  style={{ color: c(black.DEFAULT, white.DEFAULT) }}
                  value={searchValue}
                  onPress={() => handleFocus(true)}
                  onChangeText={handleSearch}
                  placeholder={t("placeholder.filter")}
                  placeholderTextColor={globalStyles.color.gray400}
                />
                {searchValue && isSearching && (
                  <View className='absolute right-3 top-3.5'>
                    <AntDesign
                      className='animate-spin'
                      name='loading2'
                      size={16}
                      color={isDarkMode ? "#A9A9A9" : "#6B7280"}
                    />
                  </View>
                )}
                {searchValue && !isSearching && (
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

          {/* Result section */}
          {searchValue !== "" && (
            <View className='mt-3 pb-[200px]'>
              <Text className='h3-bold text-black_white mb-2'>
                {t("searchResultTitle.ingredient")}
              </Text>

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
                numColumns={2}
                columnWrapperStyle={{
                  justifyContent: "space-between",
                  paddingHorizontal: 0
                }}
                renderItem={({ item, index }) => (
                  <View className={`mb-4 w-[48%] ${index % 2 === 0 ? "mr-[4%]" : ""}`}>
                    <Ingredient
                      code={item.code}
                      value={item[currentLanguage]}
                      imageUrl={item.imageUrl}
                      setSelectedTags={setSelectedTags}
                    />
                  </View>
                )}
                ListEmptyComponent={() => {
                  return isDoneSearching && searchResults?.length === 0 ? (
                    <View className='flex-center mt-10 gap-2'>
                      <Image
                        source={require("../../assets/icons/noResult.png")}
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
      </View>
    </SafeAreaView>
  );
};

export default SearchFilter;
