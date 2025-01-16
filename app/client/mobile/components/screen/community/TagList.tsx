import { useSearchTags } from "@/api/search";
import { globalStyles } from "@/components/common/GlobalStyles";
import Ingredient from "@/components/screen/search/Ingredient";
import SelectedTag from "@/components/screen/search/SelectedTag";
import useDarkMode from "@/hooks/useDarkMode";
import useDebounce from "@/hooks/useDebounce";
import {
  clearTagValue,
  removeTagValue,
  selectSearchTags
} from "@/slices/searchRecipe.slice";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import { filterUniqueItems } from "@/utils/dataFilter";
import { AntDesign, Feather } from "@expo/vector-icons";
import { router } from "expo-router";
import {
  Dispatch,
  SetStateAction,
  useCallback,
  useEffect,
  useRef,
  useState
} from "react";
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
  Alert
} from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import Tag from "./Tag";

type TagListProps = {
  selectedTags: SelectedTag[];
  setSelectedTags: Dispatch<SetStateAction<SelectedTag[]>>;
};

const TagList = ({ selectedTags, setSelectedTags }: TagListProps) => {
  const { t } = useTranslation("search");

  const [searchValue, setSearchValue] = useState<string>("");
  const [searchResults, setSearchResults] = useState<SearchTagType[]>();

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
  }, []);

  const onRefresh = useCallback(async () => {
    await refetch();
  }, [refetch]);

  const handleLoadMore = useCallback(() => {
    if (hasNextPage && !isFetchingNextPage) {
      fetchNextPage();
    }
  }, [hasNextPage, isFetchingNextPage, fetchNextPage]);

  const handleCreateNew = useCallback(() => {
    setSelectedTags(prev => [...prev, { code: "NEW-TAG", value: searchValue }]);
  }, []);

  const handleRemoveTag = useCallback((code: string) => {
    setSelectedTags(prev => prev.filter(t => t.code !== code));
  }, []);

  useEffect(() => {
    if (data?.pages) {
      const uniqueData = filterUniqueItems(data.pages);
      setSearchResults(uniqueData);
    }
  }, [data]);

  return (
    <View className={`bg-white_black size-full flex-col`}>
      <View>
        <View className='flex-row items-center gap-5'>
          <TouchableWithoutFeedback onPress={() => handleFocus(true)}>
            <View className='relative h-[40px] flex-1 flex-row items-center gap-2 border-b-[0.5px] border-gray-300'>
              <TextInput
                autoFocus
                autoCapitalize='none'
                ref={textInputRef}
                className='h-full w-[100%]'
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
            {/* <Text className='body-semibold mb-2'>
              {t("searchResultTitle.ingredient")}
            </Text> */}

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
                  <Tag
                    code={item.code}
                    value={item.value}
                    imageUrl={item.imageUrl}
                    selectedTags={selectedTags}
                    setSelectedTags={setSelectedTags}
                  />
                </View>
              )}
              ListEmptyComponent={() => {
                return isDoneSearching && searchResults?.length === 0 ? (
                  <View className='items-center gap-2'>
                    {/* <Image
                        source={require("../../../assets/icons/noResult.png")}
                        style={{ width: 130, height: 130 }}
                      />
                      <Text className='paragraph-medium text-center'>{t("notFound")}</Text> */}
                    <TouchableWithoutFeedback onPress={handleCreateNew}>
                      <View className='rounded-3xl bg-primary px-5 py-2'>
                        <Text className='body-semibold text-white_black'>
                          {t("addNew")}
                        </Text>
                      </View>
                    </TouchableWithoutFeedback>
                  </View>
                ) : (
                  <></>
                );
              }}
            />
          </View>
        )}
      </View>
    </View>
  );
};

export default TagList;
