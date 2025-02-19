import { useTranslation } from "react-i18next";
import { Text, View } from "react-native";

export default function Header() {
  const { t } = useTranslation("menu");

  return (
    <View className='flex-row items-center justify-between px-4'>
      <Text className='text-black_white font-semibold text-3xl'>{t("history")}</Text>
    </View>
  );
}
