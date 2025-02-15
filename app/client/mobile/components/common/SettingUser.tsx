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
import { Octicons } from "@expo/vector-icons";
import { router } from "expo-router";
import { useTranslation } from "react-i18next";
import useIsOwner from "@/hooks/auth/useIsOwner";
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
import Loading from "./Loading";
import { LANGUAGES } from "@/constants/languages";
import { useReportUser, useReportUserReason } from "@/api/user";

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

const SettingUser = forwardRef<BottomSheet, Props>((props, ref) => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const { t } = useTranslation("component");
  const [currentSetting, setCurrentSetting] = useState<SettingState>(Settings.INITIAL);
  const isCreatedByCurrentUser = useIsOwner(props.authorId);

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
              {!isCreatedByCurrentUser && (
                <BottomSheetItem
                  title={t("settingUser.reportUser")}
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
              accountId={props.authorId}
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
  accountId: string;
  changeSetting: (setting: SettingState) => void;
  closeModal: () => void;
};

const ReportSetting = ({ accountId, changeSetting, closeModal }: ReportSettingProps) => {
  const { c } = useColorizer();
  const { black, white, primary } = colors;
  const { t } = useTranslation("report");
  const currentLanguage = i18n.languages[0];
  const [report, setReport] = useState<Report>({
    reasonCodes: new Set(),
    additionalDetails: ""
  });

  const { mutateAsync: reportMutate, isLoading: isReporting } = useReportUser();
  const { data: reportRecipeReasons, isLoading } = useReportUserReason(
    currentLanguage === "vi" ? LANGUAGES.VIETNAMESE : LANGUAGES.ENGLISH
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
          accountId: accountId,
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
    <View className='min-h-[400px]'>
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
                placeholder={t("additionalDetails")}
              />
            </View>
          }
        />
      </View>
    </View>
  );
};

export default SettingUser;
