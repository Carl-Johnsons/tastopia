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
import { useTranslation } from "react-i18next";
import useIsOwner from "@/hooks/auth/useIsOwner";
import {
  useDeleteComment,
  useReportComment,
  useReportRecipeCommentReason,
  useUpdateComment
} from "@/api/recipe";
import { useQueryClient } from "react-query";
import { useProtectedExclude } from "@/hooks/auth/useProtected";
import { ROLE } from "@/slices/auth.slice";
import { ItemCard } from "../SettingModal";
import { ArrowBackIcon, CheckCircleIcon } from "@/constants/icons";
import i18n from "@/i18n/i18next";
import { REPORT_TYPE } from "@/constants/settings";
import Loading from "./Loading";

type Props = {
  id: string;
  recipeId?: string;
  authorId: string;
  content: string;
  deleteComment?: (commentId: string) => void;
  updateComment?: (commentId: string, content: string) => void;
};

enum Settings {
  "INITIAL",
  "REPORT",
  "UPDATE"
}

type SettingState = Settings;

const SettingComment = forwardRef<BottomSheet, Props>((props, ref) => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const { t } = useTranslation("component", { keyPrefix: "settingComment" });
  const queryClient = useQueryClient();
  const [currentSetting, setCurrentSetting] = useState<SettingState>(Settings.INITIAL);
  const isCreatedByCurrentUser = useIsOwner(props.authorId);
  const { mutateAsync: deleteComment, isLoading: isDeletingComment } = useDeleteComment();

  const changeSetting = (setting: SettingState) => {
    switch (setting) {
      case Settings.INITIAL:
        setCurrentSetting(Settings.INITIAL);
        break;
      case Settings.REPORT:
        setCurrentSetting(Settings.REPORT);
        break;
      case Settings.UPDATE:
        setCurrentSetting(Settings.UPDATE);
        break;
      default:
        setCurrentSetting(Settings.INITIAL);
    }
  };

  const closeModal = () => {
    changeSetting(Settings.INITIAL);
  };

  const onPressEdit = useProtectedExclude(() => {
    changeSetting(Settings.UPDATE);
  }, [ROLE.GUEST]);

  const onPressDelete = useProtectedExclude(() => {
    if (!isDeletingComment) {
      Alert.alert(t("confirmDeleteCommentTitle"), t("confirmDeleteCommentDescription"), [
        {
          text: t("cancel")
        },
        {
          text: t("ok"),
          onPress: () => {
            deleteComment(
              { commentId: props.id },
              {
                onSuccess: async data => {
                  queryClient.invalidateQueries({
                    queryKey: ["comments", props.recipeId]
                  });
                  queryClient.invalidateQueries({
                    queryKey: ["commentsByAuthorId", props.authorId]
                  });
                  if (props.deleteComment) {
                    props.deleteComment(props.id);
                  }
                  closeModal();
                  Alert.alert(t("deleteCommentSuccessfully"));
                },
                onError: error => {
                  Alert.alert(t("deleteCommentFail"));
                }
              }
            );
          }
        }
      ]);
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
                  title={t("editComment")}
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
                  title={t("deleteComment")}
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
                  title={t("reportComment")}
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
              commentId={props.id}
              changeSetting={changeSetting}
              closeModal={closeModal}
            />
          )}

          {currentSetting === Settings.UPDATE && (
            <UpdateSetting
              authorId={props.authorId}
              recipeId={props.recipeId ?? ""}
              commentId={props.id}
              content={props.content}
              closeModal={closeModal}
              changeSetting={changeSetting}
              updateComment={props.updateComment}
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
  commentId: string;
  changeSetting: (setting: SettingState) => void;
  closeModal: () => void;
};

const ReportSetting = ({ commentId, changeSetting, closeModal }: ReportSettingProps) => {
  const { c } = useColorizer();
  const { black, white, primary, gray } = colors;
  const { t } = useTranslation("report");
  const currentLanguage = i18n.languages[0];
  const [report, setReport] = useState<Report>({
    reasonCodes: new Set(),
    additionalDetails: ""
  });

  const { mutateAsync: reportMutate, isLoading: isReporting } = useReportComment();
  const { data: reportCommentReasons, isLoading } = useReportRecipeCommentReason(
    currentLanguage,
    REPORT_TYPE.COMMENT
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
          commentId: commentId,
          reasonCodes: [...report.reasonCodes],
          additionalDetails: report.additionalDetails
        },
        {
          onSuccess: async _data => {
            Alert.alert(t("reportSuccessfully"));
            closeModal();
          },
          onError: async _error => {
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
          data={reportCommentReasons}
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
                placeholderTextColor={gray[500]}
              />
            </View>
          }
        />
      </View>
    </View>
  );
};

type UpdateSettingProps = {
  authorId: string;
  recipeId: string;
  commentId: string;
  content: string;
  closeModal: () => void;
  changeSetting: (setting: SettingState) => void;
  updateComment?: (commentId: string, content: string) => void;
};

const UpdateSetting = ({
  authorId,
  recipeId,
  commentId,
  content: defaultContent,
  changeSetting,
  closeModal,
  updateComment
}: UpdateSettingProps) => {
  const { c } = useColorizer();
  const { black, white, primary, gray } = colors;
  const { t } = useTranslation("report", { keyPrefix: "update" });
  const [content, setContent] = useState(defaultContent);
  const queryClient = useQueryClient();
  const { mutateAsync: updateMutate, isLoading: isUpdating } = useUpdateComment();

  const handleChangeText = (content: string) => {
    setContent(content);
  };

  const handleSubmit = () => {
    if (!content) {
      Alert.alert(t("required"));
      return;
    }

    if (content.length > 500) {
      Alert.alert(t("commentLimit"));
      return;
    }

    if (!isUpdating) {
      updateMutate(
        {
          commentId,
          content
        },
        {
          onSuccess: async _data => {
            Alert.alert(t("updateSuccessfully"));
            queryClient.invalidateQueries({
              queryKey: ["commentsByAuthorId", authorId]
            });
            if (updateComment) {
              updateComment(commentId, content);
            }
            closeModal();
          },
          onError: async _error => {
            Alert.alert(t("updateFail"));
          }
        }
      );
    }
  };

  return (
    <View>
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
          <Text className='text-black_white font-semibold text-xl'>{t("title")}</Text>
        </View>

        <TouchableWithoutFeedback onPress={() => handleSubmit()}>
          <View>
            <Text className='font-semibold text-xl text-primary'>{t("update")}</Text>
          </View>
        </TouchableWithoutFeedback>
      </View>

      <View>
        <View className='mt-2 px-5'>
          <BottomSheetTextInput
            value={content}
            multiline
            onChangeText={handleChangeText}
            style={{
              borderBottomWidth: 2,
              borderBottomColor: primary,
              color: c(black.DEFAULT, white.DEFAULT)
            }}
            placeholderTextColor={gray[500]}
            placeholder={t("placeholder")}
          />
        </View>
      </View>
    </View>
  );
};

export default SettingComment;
