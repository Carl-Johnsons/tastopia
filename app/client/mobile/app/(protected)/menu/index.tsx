import Button from "@/components/Button";
import LogoutButton from "@/components/LogoutButton";
import MenuBg from "@/components/MenuBg";
import SettingModal from "@/components/SettingModal";
import { SavedIcon, SettingIcon, VipIcon, UserIcon } from "@/constants/icons";
import { selectUser } from "@/slices/user.slice";
import { Avatar } from "@rneui/base";
import { FC, useCallback, useRef } from "react";
import { Platform, Text, View, useWindowDimensions } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { SvgProps } from "react-native-svg";
import { BottomSheetModal } from "@gorhom/bottom-sheet";
import { useTranslation } from "react-i18next";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";

const Menu = () => {
  const ITEM_TITLE = ["profile", "saved", "premium", "settings"];
  const ITEM_ICON = [UserIcon, SavedIcon, VipIcon, SettingIcon];
  const { height } = useWindowDimensions();
  const settingModalRef = useRef<BottomSheetModal>(null);
  const { c } = useColorizer();
  const { black, white } = colors;

  const openSettingModal = useCallback(() => {
    settingModalRef.current?.present();
  }, []);

  const closeSettingModal = () => {
    settingModalRef?.current?.close();
  };

  return (
    <>
      <SafeAreaView
        style={{ backgroundColor: c(white.DEFAULT, black[100]), height: "100%" }}
      >
        <View>
          <MenuBg />
          <View className='bg-white_dark-100 absolute top-[8.6vh] flex w-full gap-y-5 px-4'>
            <UserCard />
            <History />
            <View style={{ paddingBlock: 0.06 * height }}>
              <View
                className='flex-row flex-wrap gap-6'
                style={{ columnGap: 8 }}
              >
                {ITEM_TITLE.map((item, index) => (
                  <ItemCard
                    key={item + index}
                    title={item}
                    icon={ITEM_ICON[index]}
                    className='basis-[40%]'
                    onPress={index === 3 ? openSettingModal : undefined}
                  />
                ))}
              </View>
            </View>
            <LogoutButton />
          </View>
        </View>
      </SafeAreaView>
      <SettingModal
        ref={settingModalRef}
        onClose={closeSettingModal}
      />
    </>
  );
};

type ItemCardProps = {
  /** The svg icon. */
  icon: FC<SvgProps>;
  /** The card's title. */
  title: string;
  className: string;
  onPress?: () => void;
};

const ItemCard = ({ icon: Icon, title, className, onPress }: ItemCardProps) => {
  const { t } = useTranslation("menu");

  return (
    <Button
      className={`shrink grow flex-row items-center gap-3 rounded-lg border border-gray-300 px-4 py-6 ${className}`}
      onPress={onPress}
    >
      <Icon
        width={28}
        height={28}
      />
      <Text className='text-md text-black_white font-medium'>{t(title)}</Text>
    </Button>
  );
};

const History = () => {
  const { height } = useWindowDimensions();
  const { t } = useTranslation("menu");

  return (
    <>
      <View className='flex-row justify-between'>
        <Text className='text-black_white font-semibold text-4xl'>{t("history")}</Text>
        <Button className='flex justify-center rounded-full border border-gray-200 px-4 py-1'>
          <Text className='text-black_white font-sans'>{t("viewAll")}</Text>
        </Button>
      </View>
      <View
        className='flex justify-center'
        style={{ height: 0.1 * height }}
      >
        <Text className='text-center font-light text-gray-400'>{t("noHistory")}</Text>
      </View>
    </>
  );
};

const UserCard = () => {
  const { displayName, avatarUrl } = selectUser();

  return (
    <View
      style={Platform.select({
        ios: {
          shadowOffset: { width: 0, height: 2 },
          shadowOpacity: 0.25,
          shadowRadius: 50
        },
        android: { elevation: 20 }
      })}
      className='flex-row items-center gap-3 rounded-lg bg-white px-4 py-1 dark:bg-black-300'
    >
      <Avatar
        size={90}
        rounded
        source={
          avatarUrl ? { uri: avatarUrl } : require("../../../assets/images/avatar.png")
        }
        containerStyle={avatarUrl && { backgroundColor: "#FFC529" }}
      />
      <Text className='text-black_white font-semibold text-xl'>{displayName}</Text>
    </View>
  );
};

export default Menu;
