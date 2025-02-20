import { useDeleteSearchRecipeHistory, useDeleteSearchUserHistory } from "@/api/search";
import { colors } from "@/constants/colors";
import { CloseIcon } from "@/constants/icons";
import useColorizer from "@/hooks/useColorizer";
import { FontAwesome6 } from "@expo/vector-icons";
import { useTranslation } from "react-i18next";
import { Alert, Text, TouchableWithoutFeedback, View } from "react-native";
import { useQueryClient } from "react-query";

type SearchHistoryProps = {
  item: string;
  type: "user" | "recipe";
  handleSelectHistory: (item: string) => void;
};

const useDeleteHistory = (type: "user" | "recipe") => {
  if (type === "recipe") {
    return useDeleteSearchRecipeHistory().mutateAsync;
  }
  return useDeleteSearchUserHistory().mutateAsync;
};

const SearchHistory = ({ item, type, handleSelectHistory }: SearchHistoryProps) => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const queryClient = useQueryClient();
  const { t } = useTranslation("search");
  const deleteHistory = useDeleteHistory(type);

  const handleRemoveHistory = async () => {
    await deleteHistory(
      {
        keyword: item
      },
      {
        onSuccess: () => {
          queryClient.invalidateQueries({
            queryKey: [
              `${type === "recipe" ? "recipeSearchHistory" : "userSearchHistory"}`
            ]
          });
        },
        onError: async _error => {
          Alert.alert(t("deleteHistoryFail"));
        }
      }
    );
  };

  return (
    <TouchableWithoutFeedback onPress={() => handleSelectHistory(item)}>
      <View className='flex flex-row justify-between'>
        <View className='flex-row items-center gap-4'>
          <FontAwesome6
            name='clock-rotate-left'
            size={18}
            color={c(black.DEFAULT, white.DEFAULT)}
          />
          <Text
            className='text-black_white max-w-[90%] text-lg'
            numberOfLines={1}
          >
            {item}
          </Text>
        </View>

        <TouchableWithoutFeedback onPress={handleRemoveHistory}>
          <View>
            <CloseIcon
              color={c(black.DEFAULT, white.DEFAULT)}
              width={22}
              height={22}
            />
          </View>
        </TouchableWithoutFeedback>
      </View>
    </TouchableWithoutFeedback>
  );
};

export default SearchHistory;
