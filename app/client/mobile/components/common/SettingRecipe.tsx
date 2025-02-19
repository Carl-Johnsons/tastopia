import {
  Alert,
  FlatList,
  Text,
  TouchableHighlight,
  TouchableWithoutFeedback,
  View
} from "react-native";
import { forwardRef, ReactNode, useState } from "react";
import BottomSheet, {
  BottomSheetBackdrop,
  BottomSheetTextInput,
  BottomSheetView
} from "@gorhom/bottom-sheet";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { AntDesign, MaterialIcons, Octicons } from "@expo/vector-icons";
import { router } from "expo-router";
import { useTranslation } from "react-i18next";
import useIsOwner from "@/hooks/auth/useIsOwner";
import {
  useDeleteOwnRecipe,
  useRecipesFeed,
  useReportRecipe,
  useReportRecipeCommentReason
} from "@/api/recipe";
import {
  InfiniteData,
  QueryObserverResult,
  RefetchOptions,
  RefetchQueryFilters
} from "react-query";
import { useProtectedExclude } from "@/hooks/auth/useProtected";
import { ROLE } from "@/slices/auth.slice";
import { ItemCard } from "../SettingModal";
import { ArrowBackIcon, CheckCircleIcon } from "@/constants/icons";
import i18n from "@/i18n/i18next";
import { REPORT_TYPE } from "@/constants/settings";
import Loading from "./Loading";
import { RecipeResponse } from "@/types/recipe";

type Props = {
  id: string;
  authorId: string;
  refetch?: <TPageData>(
    options?: (RefetchOptions & RefetchQueryFilters<TPageData>) | undefined
  ) => Promise<QueryObserverResult<InfiniteData<RecipeResponse>, unknown>>;
};

enum Settings {
  "INITIAL",
  "REPORT"
}

type SettingState = Settings;

const SettingRecipe = forwardRef<BottomSheet, Props>((props, ref) => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const { t } = useTranslation("component");
  const { refetch } = useRecipesFeed("All");
  const [currentSetting, setCurrentSetting] = useState<SettingState>(Settings.INITIAL);
  const isCreatedByCurrentUser = useIsOwner(props.authorId);
  const { mutateAsync: deleteOwnRecipe, isLoading: isDeletingOwnRecipe } =
    useDeleteOwnRecipe();

  const changeSetting = (setting: SettingState) => {
    switch (setting) {
      case Settings.INITIAL:
        setCurrentSetting(Settings.INITIAL);
        break;
      case Settings.REPORT:
        setCurrentSetting(Settings.REPORT);
        break;
      default:
        setCurrentSetting(Settings.INITIAL);
    }
  };

  const closeModal = () => {
    changeSetting(Settings.INITIAL);
  };

  const onPressEdit = useProtectedExclude(() => {
    router.push({
      pathname: "/(protected)/community/update-recipe",
      params: { id: props.id, authorId: props.authorId }
    });
    closeModal();
  }, [ROLE.GUEST]);

  const onPressDelete = useProtectedExclude(() => {
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
                    closeModal();
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
  }, [ROLE.GUEST]);

  const onPressReport = useProtectedExclude(() => {
    changeSetting(Settings.REPORT);
  }, [ROLE.GUEST]);

  return (
    <BottomSheet
      ref={ref}
      index={-1}
      enablePanDownToClose={true}
      keyboardBehavior='interactive'
      keyboardBlurBehavior='restore'
      android_keyboardInputMode='adjustPan'
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
          {currentSetting === Settings.INITIAL && (
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
          )}

          {currentSetting === Settings.REPORT && (
            <ReportSetting
              recipeId={props.id}
              changeSetting={changeSetting}
              closeModal={closeModal}
            />
          )}
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

type Report = {
  reasonCodes: Set<string>;
  additionalDetails: string;
};

type ReportSettingProps = {
  recipeId: string;
  changeSetting: (setting: SettingState) => void;
  closeModal: () => void;
};

const ReportSetting = ({ recipeId, changeSetting, closeModal }: ReportSettingProps) => {
  const { c } = useColorizer();
  const { black, white, primary, gray } = colors;
  const { t } = useTranslation("report");
  const currentLanguage = i18n.languages[0];
  const [report, setReport] = useState<Report>({
    reasonCodes: new Set(),
    additionalDetails: ""
  });

  const { mutateAsync: reportMutate, isLoading: isReporting } = useReportRecipe();
  const { data: reportRecipeReasons, isLoading } = useReportRecipeCommentReason(
    currentLanguage,
    REPORT_TYPE.RECIPE
  );
  const handleChangeText = (additionalDetails: string) => {
    setReport(prev => {
      return {
        ...prev,
        additionalDetails
      };
    });
  };

  const handleSubmit = () => {
    if (report.reasonCodes.size <= 0) {
      Alert.alert(t("required"));
      return;
    }

    if (!isLoading && !isReporting) {
      reportMutate(
        {
          recipeId: recipeId,
          reasonCodes: [...report.reasonCodes],
          additionalDetails: report.additionalDetails
        },
        {
          onSuccess: async _data => {
            Alert.alert(t("reportSuccessfully"));
            closeModal();
          },
          onError: async error => {
            Alert.alert(t("reportFail"));
          }
        }
      );
    }
  };

  const handleSelectReason = (reasonCode: string) => {
    setReport(prev => {
      const newReasonCodes = new Set(prev.reasonCodes);

      if (newReasonCodes.has(reasonCode)) {
        newReasonCodes.delete(reasonCode);
      } else {
        newReasonCodes.add(reasonCode);
      }

      return {
        reasonCodes: newReasonCodes,
        additionalDetails: prev.additionalDetails
      };
    });
  };

  if (isLoading)
    return (
      <View className='min-h-[100px]'>
        <Loading />
      </View>
    );

  return (
    <View className='min-h-[500px]'>
      <View className='relative mb-4 flex-row justify-between px-5'>
        <TouchableWithoutFeedback onPress={() => changeSetting(Settings.INITIAL)}>
          <View>
            <ArrowBackIcon
              color={c(black.DEFAULT, white.DEFAULT)}
              width={22}
              height={22}
            />
          </View>
        </TouchableWithoutFeedback>

        <View className='absolute left-1/2 -translate-x-1/3 items-center'>
          <Text className='text-black_white font-semibold text-xl'>{t("report")}</Text>
        </View>

        <TouchableWithoutFeedback onPress={() => handleSubmit()}>
          <View>
            <Text className='font-semibold text-xl text-primary'>{t("send")}</Text>
          </View>
        </TouchableWithoutFeedback>
      </View>

      <View>
        <FlatList
          data={reportRecipeReasons}
          keyExtractor={item => item.code}
          renderItem={({ item }) => (
            <ItemCard
              title={item.content}
              className='justify-between'
              additionalIcon={
                report.reasonCodes.has(item.code) ? CheckCircleIcon : undefined
              }
              onPress={() => handleSelectReason(item.code)}
            />
          )}
          ListFooterComponent={
            <View className='mt-2 px-5'>
              <BottomSheetTextInput
                value={report.additionalDetails}
                onChangeText={handleChangeText}
                style={{
                  borderBottomWidth: 2,
                  borderBottomColor: primary,
                  color: c(black.DEFAULT, white.DEFAULT)
                }}
                placeholderTextColor={gray[500]}
                placeholder={t("additionalDetails")}
              />
            </View>
          }
        />
      </View>
    </View>
  );
};

export default SettingRecipe;
