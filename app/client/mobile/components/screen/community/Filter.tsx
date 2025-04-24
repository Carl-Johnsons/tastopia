import { useSearchTagsCommunity } from "@/api/search";
import { SETTING_VALUE } from "@/constants/settings";
import { selectLanguageSetting } from "@/slices/setting.slice";
import { Image } from "expo-image";
import { memo, useEffect, useMemo } from "react";
import { Text, TouchableWithoutFeedback, View } from "react-native";

const defaultFilterData = [
  {
    en: "All",
    vi: "Tất cả",
    imageUrl: require("../../../../mobile/assets/images/all.webp")
  },
  {
    en: "Noodles",
    vi: "Mì",
    imageUrl: require("../../../../mobile/assets/images/noodles.webp")
  },
  {
    en: "Spice",
    vi: "Món cay",
    imageUrl: require("../../../../mobile/assets/images/spice.webp")
  },
  {
    en: "BBQ",
    vi: "Nướng",
    imageUrl: require("../../../../mobile/assets/images/bbq.webp")
  },
  {
    en: "Seafood",
    vi: "Hải sản",
    imageUrl: require("../../../../mobile/assets/images/seafood.webp")
  }
] as TagType[];

type FilterProps = { filterSelected: string; handleSelect: (key: string) => void };

const sortAndLimitTags = (tags?: any[]) => {
  if (!tags || tags.length === 0) return [];

  const allTag = tags.find(tag => tag.en === "All");
  const otherTags = tags.filter(tag => tag.en !== "All");
  const sortedTags = allTag ? [allTag, ...otherTags] : otherTags;
  return sortedTags.slice(0, 5);
};

const Filter = ({ filterSelected, handleSelect }: FilterProps) => {
  const language = selectLanguageSetting();
  const currentLanguage = language === SETTING_VALUE.LANGUAGE.VIETNAMESE ? "vi" : "en";
  const { data, isLoading, refetch } = useSearchTagsCommunity("", ["ALL"], "DishType");
  const filterData = data?.pages[0]?.paginatedData;

  const sortedTags = useMemo(() => {
    return (filterData?.length ?? 0) > 0
      ? sortAndLimitTags(filterData)
      : defaultFilterData;
  }, [data?.pages]);

  const handleSelectItem = (value: string) => {
    handleSelect(value);
  };

  useEffect(() => {
    refetch();
  }, [refetch]);

  return (
    !isLoading && (
      <View className='w-max-[400px] flex-center flex-row flex-wrap gap-3'>
        {sortedTags.map(item => {
          const valueBasedOnLanguage = currentLanguage === "vi" ? item.vi : item.en;
          const isSelected = filterSelected === item.vi || filterSelected === item.en;

          return (
            <TouchableWithoutFeedback
              onPress={() => handleSelectItem(item.en)}
              key={valueBasedOnLanguage}
            >
              <View
                className={`flex-center flex-row gap-2 rounded-full border-2 border-primary px-2 py-1 pr-4 ${isSelected ? "bg-primary" : "bg-white_black"}`}
              >
                <View
                  className={`rounded-full p-[0.5px] ${isSelected ? "bg-white" : ""}`}
                >
                  <Image
                    source={item.imageUrl}
                    style={[{ width: 50, height: 50, borderRadius: 50 }]}
                    cachePolicy={"disk"}
                    contentFit='cover'
                    transition={200}
                  />
                </View>
                <Text
                  className={`body-medium text-center ${isSelected ? "text-white" : "text-black_white"}`}
                >
                  {valueBasedOnLanguage}
                </Text>
              </View>
            </TouchableWithoutFeedback>
          );
        })}
      </View>
    )
  );
};

export default memo(Filter);
