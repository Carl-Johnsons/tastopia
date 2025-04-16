import uuid from "react-native-uuid";
import { useSearchTags } from "@/api/search";
import { globalStyles } from "@/components/common/GlobalStyles";
import SelectedTag from "@/components/screen/search/SelectedTag";
import useDarkMode from "@/hooks/useDarkMode";
import useDebounce from "@/hooks/useDebounce";
import { filterUniqueItems } from "@/utils/dataFilter";
import { AntDesign } from "@expo/vector-icons";
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
  Alert,
  FlatList,
  Keyboard,
  RefreshControl,
  StatusBar,
  Text,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View
} from "react-native";
import Tag from "./Tag";
import { selectLanguageSetting } from "@/slices/setting.slice";
import { SETTING_VALUE } from "@/constants/settings";

type TagListProps = {
  selectedTags: SelectedTag[];
  setSelectedTags: Dispatch<SetStateAction<SelectedTag[]>>;
};

const TagList = ({ selectedTags, setSelectedTags }: TagListProps) => {
  const { t } = useTranslation("search");
  const language = selectLanguageSetting();
  const currentLanguage = language === SETTING_VALUE.LANGUAGE.VIETNAMESE ? "vi" : "en";

  const [searchValue, setSearchValue] = useState<string>("");
  const [searchResults, setSearchResults] = useState<TagType[]>();

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
    if (selectedTags.find(tag => tag.value === searchValue.trim())) {
      Alert.alert(t("tagAdded"));
      return;
    } else {
      setSelectedTags(prev => [
        ...prev,
        { id: uuid.v4(), code: "NEW-TAG", value: searchValue.trim() }
      ]);
    }
  }, [searchValue, setSelectedTags]);

  const handleRemoveTag = useCallback(
    (code: string) => {
      setSelectedTags(prev =>
        prev.filter(t => {
          return t.code !== code;
        })
      );
    },
    [searchValue, setSelectedTags]
  );

  useEffect(() => {
    if (data?.pages) {
      const uniqueData = filterUniqueItems(data.pages);
      setSearchResults(uniqueData);
    }
  }, [data]);

  return (
    <View className={`bg-white_black100 size-full flex-col`}>
      <View>
        <View className='flex-row items-center gap-5'>
          <TouchableWithoutFeedback onPress={() => handleFocus(true)}>
            <View className='relative h-[40px] flex-1 flex-row items-center gap-2 border-b-[0.5px] border-gray-300'>
              <TextInput
                autoCapitalize='none'
                ref={textInputRef}
                className='text-black_white h-full w-[100%]'
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
                  id={tag.id}
                  code={tag.code}
                  value={tag.value}
                  onRemove={() => handleRemoveTag(tag.code)}
                />
              );
            })}
          </View>
        </View>

        {/* Result section */}
        {searchValue !== "" && (
          <View className='mt-3 pb-[100px]'>
            <FlatList
              scrollEnabled={false}
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
                    value={item[currentLanguage]}
                    imageUrl={item.imageUrl}
                    selectedTags={selectedTags}
                    setSelectedTags={setSelectedTags}
                  />
                </View>
              )}
              ListEmptyComponent={() => {
                return isDoneSearching && searchResults?.length === 0 ? (
                  <View className='items-center gap-2'>
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
