import { colors } from "@/constants/colors";
import { ArrowBackIcon } from "@/constants/icons";
import useColorizer from "@/hooks/useColorizer";
import { router } from "expo-router";
import { useTranslation } from "react-i18next";
import { Text, View } from "react-native";

export default function Header() {
  const { t } = useTranslation("notification");
  const { c } = useColorizer();
  const { black, white } = colors;

  return (
    <View className='pt-2'>
      <View className='relative flex-row items-center justify-between px-4'>
        <ArrowBackIcon
          color={c(black.DEFAULT, white.DEFAULT)}
          width={28}
          height={28}
          onPress={router.back}
        />
        <Text className='text-black_white font-medium text-2xl'>{t("notification")}</Text>
        <View className='w-[28px]' />
      </View>
    </View>
  );
}
