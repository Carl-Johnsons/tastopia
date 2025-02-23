import Button from "@/components/Button";
import LogoutButton from "@/components/LogoutButton";
import MenuBg from "@/components/MenuBg";
import SettingModal from "@/components/SettingModal";
import { SavedIcon, SettingIcon, TrashIcon, UserIcon } from "@/constants/icons";
import { FC, useCallback, useRef } from "react";
import { StatusBar, Text, View, useWindowDimensions } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { SvgProps } from "react-native-svg";
import { BottomSheetModal } from "@gorhom/bottom-sheet";
import { useTranslation } from "react-i18next";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { router } from "expo-router";
import UserCard from "@/components/screen/menu/UserCard";
import { selectUser } from "@/slices/user.slice";
import History from "@/components/screen/menu/History";

const Menu = () => {
  const ITEM_TITLE = ["profile", "saved", "deleted", "settings"];
  const ITEM_ICON = [UserIcon, SavedIcon, TrashIcon, SettingIcon];
  const { height } = useWindowDimensions();
  const settingModalRef = useRef<BottomSheetModal>(null);
  const { c } = useColorizer();
  const { black, white } = colors;
  const { accountId } = selectUser();

  const openSettingModal = useCallback(() => {
    settingModalRef.current?.present();
  }, [settingModalRef]);

  const closeSettingModal = useCallback(() => {
    settingModalRef.current?.close();
  }, [settingModalRef]);

  const goToProfile = useCallback(() => {
    router.push({
      pathname: "/(protected)/user/[id]",
      params: { id: accountId ?? "" }
    });
  }, [router]);

  const navigationCallbacks = [
    goToProfile,
    useCallback(() => {
      router.push("/(protected)/menu/bookmark");
    }, []),
    useCallback(() => {
      router.push("/(protected)/menu/deleted-recipe");
    }, []),
    openSettingModal
  ];

  return (
    <>
      <SafeAreaView
        style={{ backgroundColor: c(white.DEFAULT, black[100]), height: "100%" }}
      >
        <View>
          <MenuBg />
          <View className='bg-white_dark-100 absolute top-[8.6vh] flex w-full gap-y-2'>
            <View className='px-4'>
              <UserCard onPress={goToProfile} />
            </View>
            <History />
            <View className='px-4 py-7'>
              <View
                className='flex-row flex-wrap gap-4'
                style={{ columnGap: 12 }}
              >
                {ITEM_TITLE.map((item, index) => (
                  <ItemCard
                    key={item + index}
                    title={item}
                    icon={ITEM_ICON[index]}
                    className='basis-[40%]'
                    onPress={navigationCallbacks[index]}
                  />
                ))}
              </View>
            </View>
            <View className='px-4'>
              <LogoutButton />
            </View>
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
      <Text className='text-black_white font-medium text-xl'>{t(title)}</Text>
    </Button>
  );
};

export default Menu;
