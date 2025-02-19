import { useTranslation } from "react-i18next";
import { Pressable, Text, View } from "react-native";
import PreviewHistoryList from "./PreviewHistoryList";
import { ROLE, selectRole } from "@/slices/auth.slice";
import { router } from "expo-router";

export default function History() {
  const { t } = useTranslation("menu");
  const role = selectRole();

  return (
    <View>
      <View className='flex-row items-center justify-between px-4'>
        <Text className='text-black_white font-semibold text-3xl'>{t("history")}</Text>
        {role === ROLE.GUEST ? (
          <View />
        ) : (
          <Pressable onPress={() => {router.push("/(protected)/menu/history")}} className='flex justify-center rounded-full border border-gray-200 px-4 py-1'>
            <Text className='text-black_white font-sans text-lg'>{t("viewAll")}</Text>
          </Pressable>
        )}
      </View>
      <View className='flex justify-center'>
        {role === ROLE.GUEST ? (
          <View className='flex-center h-[140px]'>
            <Text className='text-center font-light text-lg text-gray-500'>
              {t("loginToViewHistory")}
            </Text>
          </View>
        ) : (
          <PreviewHistoryList />
        )}
      </View>
    </View>
  );
}
