import { Alert, Text, TouchableHighlight, View } from "react-native";
import { forwardRef, ReactNode } from "react";
import BottomSheet, { BottomSheetBackdrop, BottomSheetView } from "@gorhom/bottom-sheet";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { AntDesign, MaterialIcons, Octicons } from "@expo/vector-icons";
import { router } from "expo-router";
import { useTranslation } from "react-i18next";
import useIsOwner from "@/hooks/auth/useIsOwner";
import { useDeleteOwnRecipe, useRecipesFeed } from "@/api/recipe";
import {
  InfiniteData,
  QueryObserverResult,
  RefetchOptions,
  RefetchQueryFilters
} from "react-query";

type Props = {
  id: string;
  authorId: string;
  title: string;
  refetch?: <TPageData>(
    options?: (RefetchOptions & RefetchQueryFilters<TPageData>) | undefined
  ) => Promise<QueryObserverResult<InfiniteData<RecipeResponse>, unknown>>;
};

const SettingRecipe = forwardRef<BottomSheet, Props>((props, ref) => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const { t } = useTranslation("component");
  const { mutateAsync: deleteOwnRecipe, isLoading: isDeletingOwnRecipe } =
    useDeleteOwnRecipe();
  const { refetch } = useRecipesFeed("All");

  const isCreatedByCurrentUser = useIsOwner(props.authorId);

  const onPressEdit = () => {
    router.push({
      pathname: "/(protected)/community/update-recipe",
      params: { id: props.id, authorId: props.authorId }
    });
  };

  const onPressDelete = () => {
    if (!isDeletingOwnRecipe) {
      Alert.alert(
        t("settingRecipe.confirmDeleteRecipeTitle"),
        t("settingRecipe.confirmDeleteRecipeDescription"),
        [
          {
            text: t("cancel")
          },
          {
            text: t("ok"),
            onPress: () => {
              deleteOwnRecipe(
                { recipeId: props.id },
                {
                  onSuccess: async data => {
                    Alert.alert(t("settingRecipe.deleteRecipeSuccessfully"));
                    refetch();
                    router.navigate("/(protected)");
                  },
                  onError: error => {
                    Alert.alert(t("settingRecipe.deleteRecipeFail"));
                  }
                }
              );
            }
          }
        ]
      );
    }
  };

  const onPressShare = () => {};

  const onPressReport = () => {};
  return (
    <BottomSheet
      ref={ref}
      index={-1}
      enablePanDownToClose={true}
      handleIndicatorStyle={{
        backgroundColor: c(colors.black.DEFAULT, colors.white.DEFAULT)
      }}
      backgroundStyle={{
        backgroundColor: c(colors.white.DEFAULT, colors.black[100])
      }}
      backdropComponent={props => (
        <BottomSheetBackdrop
          {...props}
          disappearsOnIndex={-1}
          appearsOnIndex={0}
          pressBehavior='close'
        />
      )}
    >
      <BottomSheetView>
        <View className='mt-2 px-5 pb-4'>
          <View>
            {isCreatedByCurrentUser && (
              <BottomSheetItem
                title={t("settingRecipe.editRecipe")}
                icon={
                  <AntDesign
                    name='edit'
                    size={20}
                    color={c(black.DEFAULT, white.DEFAULT)}
                  />
                }
                onPress={onPressEdit}
              />
            )}
            {isCreatedByCurrentUser && (
              <BottomSheetItem
                title={t("settingRecipe.deleteRecipe")}
                icon={
                  <MaterialIcons
                    name='delete-outline'
                    size={24}
                    color={c(black.DEFAULT, white.DEFAULT)}
                  />
                }
                onPress={onPressDelete}
              />
            )}
            {/* <BottomSheetItem
              title={t("settingRecipe.share")}
              icon={
                <AntDesign
                  name='sharealt'
                  size={20}
                  color={c(black.DEFAULT, white.DEFAULT)}
                />
              }
              onPress={onPressShare}
            /> */}
            {!isCreatedByCurrentUser && (
              <BottomSheetItem
                title={t("settingRecipe.reportRecipe")}
                icon={
                  <Octicons
                    name='report'
                    size={20}
                    color={c(black.DEFAULT, white.DEFAULT)}
                  />
                }
                onPress={onPressReport}
              />
            )}
          </View>
        </View>
      </BottomSheetView>
    </BottomSheet>
  );
});

type BottomSheetItemProps = {
  title: string;
  description?: string;
  icon: ReactNode;
  onPress: () => void;
};

const BottomSheetItem = ({ title, description, icon, onPress }: BottomSheetItemProps) => {
  const { c } = useColorizer();

  return (
    <TouchableHighlight
      onPress={onPress}
      underlayColor={c(colors.gray[200], colors.gray[700])}
      style={{
        borderRadius: 8
      }}
    >
      <View className='flex-row items-center gap-4 px-4 py-3'>
        {icon}

        <View className='flex-col'>
          <Text className='text-black_white base-medium'>{title}</Text>
          {description && <Text className='text-black_white'>{description}</Text>}
        </View>
      </View>
    </TouchableHighlight>
  );
};

export default SettingRecipe;
