import { Alert, Platform, Switch, Text, View } from "react-native";
import Button from "./Button";
import {
  Dispatch,
  FC,
  RefObject,
  SetStateAction,
  forwardRef,
  useCallback,
  useMemo,
  useState
} from "react";
import { SvgProps } from "react-native-svg";
import {
  BaseUserIcon,
  CheckCircleIcon,
  CloseIcon,
  DocumentIcon,
  LangIcon,
  MoonIcon,
  NotificationIcon,
  VietnamFlagIcon,
  UnitedKingdomFlagIcon,
  ArrowBackIcon
} from "@/constants/icons";
import { selectUser } from "@/slices/user.slice";
import { colors } from "@/constants/colors";
import {
  saveSettingData,
  selectDarkModeSetting,
  selectLanguageSetting,
  selectNotificationCommentSetting,
  selectNotificationFollowSetting,
  selectNotificationVoteSetting
} from "@/slices/setting.slice";
import {
  getBooleanValueFromSetting,
  getSettingFromBooleanValue
} from "@/utils/converter";
import { useDispatch } from "react-redux";
import Animated from "react-native-reanimated";
import Protected from "./Protected";
import { ROLE, saveAuthData, selectRole } from "@/slices/auth.slice";
import {
  AddIdentifierParams,
  IDENTIFIER_TYPE,
  ModifyIdentifierParams,
  UpdateSettingParams,
  useRequestUpdateIdentifier,
  useUnlink,
  useUpdateSetting
} from "@/api/user";
import {
  BottomSheetBackdrop,
  BottomSheetModal,
  BottomSheetView,
  BottomSheetBackdropProps,
  useBottomSheetModal
} from "@gorhom/bottom-sheet";

import { BottomSheetMethods } from "@gorhom/bottom-sheet/lib/typescript/types";
import { useTranslation } from "react-i18next";
import { useColorScheme } from "nativewind";
import useColorizer from "@/hooks/useColorizer";
import { router, useRouter } from "expo-router";
import { SETTING_KEY, SETTING_VALUE } from "@/constants/settings";
import { useQueryClient } from "react-query";
import { Feather } from "@expo/vector-icons";
import { useAppDispatch } from "@/store/hooks";
import { useErrorHandler } from "@/hooks/useErrorHandler";
import useSyncUser from "@/hooks/user/useSyncUser";

type SettingModalProps = {
  ref: RefObject<BottomSheetMethods>;
  onClose: () => void;
};

enum Settings {
  "ACCOUNT",
  "LANGUAGE",
  "DARK MODE",
  "NOTIFICATION",
  "TERM OF SERVICES"
}

const CATEGORY = [
  "account.title",
  "language.title",
  "darkMode",
  "notification.title",
  "termOfServices.title"
];

const ICON = [BaseUserIcon, LangIcon, MoonIcon, NotificationIcon, DocumentIcon];

type SettingState = Settings | "initial";

const SettingModal = forwardRef<BottomSheetModal, SettingModalProps>((_props, ref) => {
  const { t } = useTranslation("settingModal");
  const [currentSetting, setCurrentSetting] = useState<SettingState>("initial");
  const [title, setTitle] = useState<string>("setting");
  const { dismiss } = useBottomSheetModal();
  const { c } = useColorizer();
  const { black, white } = colors;

  const changeSetting = useCallback((setting: Settings | "initial") => {
    switch (setting) {
      case "initial":
        setCurrentSetting("initial");
        setTitle(t("setting"));
        break;

      case Settings.ACCOUNT:
        setCurrentSetting(Settings.ACCOUNT);
        setTitle(CATEGORY[Settings.ACCOUNT]);
        break;

      case Settings.LANGUAGE:
        setCurrentSetting(Settings.LANGUAGE);
        setTitle(CATEGORY[Settings.LANGUAGE]);
        break;

      case Settings["DARK MODE"]:
        break;

      case Settings.NOTIFICATION:
        setCurrentSetting(Settings.NOTIFICATION);
        setTitle(CATEGORY[Settings.NOTIFICATION]);
        break;

      case Settings["TERM OF SERVICES"]:
        setCurrentSetting(Settings["TERM OF SERVICES"]);
        setTitle(CATEGORY[Settings["TERM OF SERVICES"]]);
        router.push("/(public)/(modals)/termOfServices");
        closeModal();
        break;
      default:
        throw new Error(`Unhandled setting: ${setting}`);
    }
  }, []);

  const closeModal = useCallback(() => {
    setCurrentSetting("initial");
    setTitle("Setting");
    dismiss();
  }, [dismiss]);

  const renderBackdrop = useCallback(
    (props: BottomSheetBackdropProps) => (
      <BottomSheetBackdrop
        {...props}
        disappearsOnIndex={-1}
        appearsOnIndex={0}
        pressBehavior='close'
        onPress={closeModal}
      />
    ),
    []
  );

  return (
    <BottomSheetModal
      ref={ref}
      backdropComponent={renderBackdrop}
      handleIndicatorStyle={{
        backgroundColor: c(colors.black.DEFAULT, colors.white.DEFAULT),
        display: "none"
      }}
      backgroundStyle={{
        backgroundColor: c(colors.white.DEFAULT, colors.black[100])
      }}
      enablePanDownToClose={true}
      enableContentPanningGesture={
        currentSetting === Settings["TERM OF SERVICES"] ? false : true
      }
    >
      <BottomSheetView>
        <Animated.View
          //layout={transition}
          className='w-full rounded-t-lg bg-white dark:bg-black-100'
        >
          {/* Header */}
          <View className='relative flex-row items-center justify-center px-5 pb-4'>
            <View className='absolute left-5 top-1'>
              {currentSetting === "initial" ? (
                <Button onPress={closeModal}>
                  <CloseIcon
                    color={c(black.DEFAULT, white.DEFAULT)}
                    width={22}
                    height={22}
                  />
                </Button>
              ) : (
                <Button onPress={() => changeSetting("initial")}>
                  <ArrowBackIcon
                    color={c(black.DEFAULT, white.DEFAULT)}
                    width={22}
                    height={22}
                  />
                </Button>
              )}
            </View>
            <Text className='text-black_white font-semibold text-2xl'>{t(title)}</Text>
            <View />
          </View>

          {/* State */}
          {currentSetting === "initial" && <Main changeSetting={changeSetting} />}
          {currentSetting === Settings.ACCOUNT && <AccountSetting />}
          {currentSetting === Settings.LANGUAGE && <LanguageSetting />}
          {currentSetting === Settings.NOTIFICATION && <NotificationSetting />}
        </Animated.View>
      </BottomSheetView>
    </BottomSheetModal>
  );
});

const Main = ({
  changeSetting
}: {
  changeSetting: (index: Settings | "initial") => void;
}) => {
  const { t } = useTranslation("settingModal");
  const role = selectRole();
  const dispatch = useDispatch();
  const { mutateAsync: updateSetting } = useUpdateSetting();
  const darkMode = selectDarkModeSetting();
  const languague = selectLanguageSetting();
  const { toggleColorScheme } = useColorScheme();

  const setDarkMode: Dispatch<SetStateAction<boolean>> = async value => {
    const oldValue = JSON.stringify(darkMode) as SETTING_VALUE.BOOLEAN;
    const newValue = getSettingFromBooleanValue(value as boolean);

    dispatch(
      saveSettingData({
        [SETTING_KEY.DARK_MODE]: newValue
      })
    );

    const data: UpdateSettingParams = {
      settings: [
        {
          key: SETTING_KEY.DARK_MODE,
          value: newValue
        }
      ]
    };

    if (role === ROLE.GUEST) {
      return toggleColorScheme();
    }

    await updateSetting(
      { ...data },
      {
        onError: () => {
          dispatch(
            saveSettingData({
              [SETTING_KEY.DARK_MODE]: oldValue
            })
          );

          Alert.alert("Error", "An error has occurred. Please try again later.");
        }
      }
    );
  };

  return (
    <>
      {CATEGORY.map((item, index) => {
        if (index === Settings["LANGUAGE"]) {
          return (
            <ItemCard
              key={item + index}
              title={t(item)}
              icon={ICON[index]}
              additionalIcon={
                languague === SETTING_VALUE.LANGUAGE.VIETNAMESE
                  ? VietnamFlagIcon
                  : UnitedKingdomFlagIcon
              }
              className='justify-between'
              onPress={() => changeSetting(index as Settings)}
            />
          );
        }

        if (index === Settings["DARK MODE"]) {
          return (
            <ItemCard
              key={item + index}
              title={t(item)}
              icon={ICON[index]}
              switchOptions={{
                isEnabled: getBooleanValueFromSetting(darkMode),
                onChange: setDarkMode
              }}
              className='justify-between'
            />
          );
        }

        return (
          <Protected
            key={item + index}
            excludedRoles={
              index === Settings.ACCOUNT || index === Settings.NOTIFICATION
                ? [ROLE.GUEST]
                : []
            }
          >
            <ItemCard
              title={t(item)}
              icon={ICON[index]}
              onPress={() => changeSetting(index as Settings)}
            />
          </Protected>
        );
      })}
    </>
  );
};

const AccountSetting = () => {
  const { t } = useTranslation("settingModal", { keyPrefix: "account" });
  const { t: tA } = useTranslation("settingModal", { keyPrefix: "account.updateAlert" });
  const { t: tUnlink } = useTranslation("settingModal", {
    keyPrefix: "account.unlinkAlert"
  });
  const { t: tLink } = useTranslation("settingModal", {
    keyPrefix: "account.linkAlert"
  });
  const { accountPhoneNumber, accountEmail } = selectUser();
  const isHaveBothIdentifier = useMemo(
    () => !!accountEmail && !!accountPhoneNumber,
    [accountEmail, accountPhoneNumber]
  );

  const { mutateAsync: unlinkIdentifier } = useUnlink();
  const { mutateAsync: requestUpdateIdentifier } = useRequestUpdateIdentifier();

  const { dismiss } = useBottomSheetModal();
  const router = useRouter();
  const dispatch = useAppDispatch();

  const { handleError } = useErrorHandler();
  const { fetch: fetchUser } = useSyncUser();

  const link = useCallback(async ({ type }: AddIdentifierParams) => {
    Alert.alert(
      tLink("title"),
      type === IDENTIFIER_TYPE.EMAIL
        ? tLink("emailDescription")
        : tLink("phoneDescription"),
      [
        { text: tLink("cancel"), style: "cancel" },
        {
          text: tLink("link"),
          onPress: () => {
            dispatch(
              saveAuthData({
                addIdentifierData: { type },
                resetAddIdentifierForm: true
              })
            );
            dismiss();
            router.push("/(protected)/add-identifier");
          }
        }
      ]
    );
  }, []);

  const unlink = useCallback(
    async ({ data, type }: ModifyIdentifierParams) => {
      Alert.alert(
        tUnlink("title"),
        type === IDENTIFIER_TYPE.EMAIL
          ? tUnlink("emailDescription")
          : tUnlink("phoneDescription"),
        [
          { text: tUnlink("cancel"), style: "cancel" },
          {
            text: tUnlink("unlink"),
            onPress: async () => {
              unlinkIdentifier(
                { type, data },
                {
                  onSuccess: () => {
                    Alert.alert(tUnlink("success.title"), tUnlink("success.description"));
                    fetchUser();
                  },
                  onError: err => handleError(err)
                }
              );
            }
          }
        ]
      );
    },
    [fetchUser, handleError]
  );

  const updateUserInfo = useCallback(async ({ data, type }: ModifyIdentifierParams) => {
    Alert.alert(tA("title"), tA("description"), [
      { text: tA("cancel"), style: "cancel" },
      {
        text: tA("update"),
        onPress: async () => {
          await requestUpdateIdentifier(
            { type, data },
            {
              onError: err => handleError(err)
            }
          );

          dispatch(
            saveAuthData({
              modifyIdentifierData: { type, data },
              resetModifyIdentifierForm: true
            })
          );
          dismiss();
          router.push("/(protected)/verify-identifier");
        }
      }
    ]);
  }, []);

  const Item = ({
    title,
    value,
    onPressUnlink,
    onPressUpdate,
    onPressLink,
    isShowTrash
  }: {
    title: string;
    value?: string | null;
    isShowTrash?: boolean;
    onPressUnlink?: () => void;
    onPressUpdate?: () => void;
    onPressLink?: () => void;
  }) => {
    const { c } = useColorizer();
    const { black, white } = colors;

    return (
      <View className={`flex-row items-center justify-between gap-3 px-4 py-6`}>
        <Text className='text-black_white'>{title}</Text>
        <View className='flex-row items-center gap-2'>
          <Text className='text-black_white'>{value ? value : ""}</Text>
          {value ? (
            <View className='flex-row gap-3'>
              {value && (
                <>
                  <Button
                    className='rounded-full bg-primary p-1'
                    onPress={() => {
                      onPressUpdate?.();
                      console.log("goto update page");
                    }}
                  >
                    <Feather
                      name='edit-2'
                      size={14}
                      color={c(white.DEFAULT, black.DEFAULT)}
                    />
                  </Button>
                  {isShowTrash && (
                    <Button
                      className='rounded-full bg-primary p-1'
                      onPress={onPressUnlink}
                    >
                      <Feather
                        name='trash-2'
                        size={14}
                        color={c(white.DEFAULT, black.DEFAULT)}
                      />
                    </Button>
                  )}
                </>
              )}
            </View>
          ) : (
            <Button
              onPress={onPressLink}
              className='flex-center aspect-square w-[20px] rounded-full border border-gray-400'
            >
              <Feather
                name='plus'
                size={14}
                color={colors.primary}
              />
            </Button>
          )}
        </View>
      </View>
    );
  };

  return (
    <>
      <Item
        title={t("email")}
        value={accountEmail}
        isShowTrash={isHaveBothIdentifier}
        onPressLink={() => {
          link({
            type: IDENTIFIER_TYPE.EMAIL
          });
        }}
        onPressUnlink={
          !!accountEmail
            ? () => {
                unlink({
                  type: IDENTIFIER_TYPE.EMAIL,
                  data: {
                    identifier: accountEmail
                  }
                });
              }
            : undefined
        }
        onPressUpdate={
          !!accountEmail
            ? () => {
                updateUserInfo({
                  type: IDENTIFIER_TYPE.EMAIL,
                  data: { identifier: accountEmail }
                });
              }
            : undefined
        }
      />
      <Item
        title={t("phone")}
        value={accountPhoneNumber}
        isShowTrash={isHaveBothIdentifier}
        onPressLink={() => {
          link({
            type: IDENTIFIER_TYPE.PHONE_NUMBER
          });
        }}
        onPressUnlink={
          !!accountPhoneNumber
            ? () => {
                unlink({
                  type: IDENTIFIER_TYPE.PHONE_NUMBER,
                  data: {
                    identifier: accountPhoneNumber
                  }
                });
              }
            : undefined
        }
        onPressUpdate={
          !!accountPhoneNumber
            ? () => {
                updateUserInfo({
                  type: IDENTIFIER_TYPE.PHONE_NUMBER,
                  data: { identifier: accountPhoneNumber }
                });
              }
            : undefined
        }
      />
    </>
  );
};

const LanguageSetting = () => {
  const { t } = useTranslation("settingModal", { keyPrefix: "language" });
  const role = selectRole();
  const dispatch = useDispatch();
  const { mutateAsync: updateSetting } = useUpdateSetting();
  const languague = selectLanguageSetting();
  const queryClient = useQueryClient();

  const setLanguage = async (value: SETTING_VALUE.LANGUAGE) => {
    const oldValue = JSON.stringify(value) as SETTING_VALUE.LANGUAGE;

    if (role === ROLE.GUEST) {
      return dispatch(
        saveSettingData({
          [SETTING_KEY.LANGUAGE]: value
        })
      );
    }

    const data: UpdateSettingParams = {
      settings: [
        {
          key: SETTING_KEY.LANGUAGE,
          value: value
        }
      ]
    };

    await updateSetting(
      { ...data },
      {
        onSuccess: async () => {
          dispatch(
            saveSettingData({
              [SETTING_KEY.LANGUAGE]: value
            })
          );

          await queryClient.invalidateQueries({ queryKey: ["getNotification"] });
          console.log("Invalidated notification");
        },
        onError: () => {
          dispatch(
            saveSettingData({
              [SETTING_KEY.LANGUAGE]: oldValue
            })
          );

          Alert.alert("Error", "An error has occurred. Please try again later.");
        }
      }
    );
  };

  return (
    <>
      <ItemCard
        title={t("vietnamese")}
        icon={VietnamFlagIcon}
        className='justify-between'
        additionalIcon={
          languague === SETTING_VALUE.LANGUAGE.VIETNAMESE ? CheckCircleIcon : undefined
        }
        onPress={() => {
          setLanguage(SETTING_VALUE.LANGUAGE.VIETNAMESE);
        }}
      />
      <ItemCard
        title={t("english")}
        icon={UnitedKingdomFlagIcon}
        className='justify-between'
        additionalIcon={
          languague === SETTING_VALUE.LANGUAGE.ENGLISH ? CheckCircleIcon : undefined
        }
        onPress={() => {
          setLanguage(SETTING_VALUE.LANGUAGE.ENGLISH);
        }}
      />
    </>
  );
};

const NotificationSetting = () => {
  const { t } = useTranslation("settingModal", { keyPrefix: "notification" });
  const dispatch = useDispatch();
  const { mutateAsync: updateSetting } = useUpdateSetting();
  const notificationComment = selectNotificationCommentSetting();
  const notificationVote = selectNotificationVoteSetting();
  const notificationFollow = selectNotificationFollowSetting();

  const setValue = async (key: SETTING_KEY, value: SetStateAction<boolean>) => {
    let oldValue: null | string = null;
    const newValue = getSettingFromBooleanValue(value as boolean);

    switch (key) {
      case SETTING_KEY.NOTIFICATION_COMMENT:
        oldValue = JSON.stringify(notificationComment);
        break;
      case SETTING_KEY.NOTIFICATION_VOTE:
        oldValue = JSON.stringify(notificationVote);
        break;
      case SETTING_KEY.NOTIFICATION_FOLLOW:
        oldValue = JSON.stringify(notificationFollow);
        break;
      default:
        throw new Error("Key must be one of the three notification setting types.");
    }

    dispatch(
      saveSettingData({
        [key]: newValue
      })
    );

    const data: UpdateSettingParams = {
      settings: [
        {
          key,
          value: newValue
        }
      ]
    };

    await updateSetting(
      { ...data },
      {
        onError: () => {
          dispatch(
            saveSettingData({
              [key]: oldValue
            })
          );

          Alert.alert("Error", "An error has occurred. Please try again later.");
        }
      }
    );
  };

  return (
    <>
      <ItemCard
        title={t("comment")}
        className='justify-between'
        switchOptions={{
          isEnabled: getBooleanValueFromSetting(notificationComment),
          onChange: value => {
            setValue(SETTING_KEY.NOTIFICATION_COMMENT, value);
          }
        }}
      />
      <ItemCard
        title={t("vote")}
        className='justify-between'
        switchOptions={{
          isEnabled: getBooleanValueFromSetting(notificationVote),
          onChange: value => {
            setValue(SETTING_KEY.NOTIFICATION_VOTE, value);
          }
        }}
      />
      <ItemCard
        title={t("follow")}
        className='justify-between'
        switchOptions={{
          isEnabled: getBooleanValueFromSetting(notificationFollow),
          onChange: value => {
            setValue(SETTING_KEY.NOTIFICATION_FOLLOW, value);
          }
        }}
      />
    </>
  );
};

type ItemCardProps = {
  className?: string;
  /** Additional class name for the main content's wrapper. */
  mainContentClassName?: string;
  onPress?: () => void;
  /** The svg icon. */
  icon?: FC<SvgProps>;
  /** The card's title. */
  title: string;
  /** Whether the text is on the front of the icon. */
  reversed?: boolean;
  /** Also include a switch for changing boolean value. */
  switchOptions?: SwitchOptions;
  additionalIcon?: FC<SvgProps>;
};

type SwitchOptions = {
  isEnabled: boolean;
  onChange: Dispatch<SetStateAction<boolean>>;
};

export const ItemCard = ({
  icon: Icon,
  additionalIcon: AdditionalIcon,
  title,
  className,
  mainContentClassName,
  onPress,
  reversed,
  switchOptions
}: ItemCardProps) => {
  const { c } = useColorizer();

  const mainContent = [
    Icon && (
      <Icon
        color={c(colors.black.DEFAULT, colors.white.DEFAULT)}
        key='icon'
        width={22}
        height={22}
      />
    ),
    <Text
      key='text'
      className='text-black_white font-sans text-lg max-sm:max-w-[300px]'
    >
      {title}
    </Text>
  ];

  const renderSwitch = useCallback(() => {
    const { isEnabled, onChange } = switchOptions as SwitchOptions;
    const { primary, gray, white, black } = colors;
    const { c } = useColorizer();

    const thumbColor = c(
      Platform.select({
        ios: white.DEFAULT,
        android: black.DEFAULT
      }),
      Platform.select({
        ios: black.DEFAULT,
        android: white.DEFAULT
      })
    );

    const inactiveTrackColor = c(
      Platform.select({
        ios: gray[200],
        android: gray[200]
      }),
      Platform.select({
        ios: black.DEFAULT,
        android: white.DEFAULT
      })
    );

    return (
      <View className='flex w-[10%] items-center'>
        <Switch
          trackColor={{ false: `${inactiveTrackColor}`, true: `${primary}` }}
          thumbColor={isEnabled ? `${thumbColor}` : `${thumbColor}`}
          onValueChange={onChange}
          value={isEnabled}
        />
      </View>
    );
  }, [switchOptions, colors]);

  return (
    <Button
      className={`flex-row items-center gap-3 px-5 ${className}`}
      onPress={onPress}
    >
      <View className={`flex-row items-center gap-3 py-4 ${mainContentClassName}`}>
        {reversed ? mainContent.reverse() : mainContent}
      </View>
      {switchOptions && renderSwitch()}
      {AdditionalIcon && (
        <View className='flex w-[10%] items-center'>
          <AdditionalIcon
            width={22}
            height={22}
          />
        </View>
      )}
    </Button>
  );
};

export default SettingModal;
