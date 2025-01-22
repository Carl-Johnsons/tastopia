import Button from "@/components/Button";
import { colors } from "@/constants/colors";
import { CloseIcon } from "@/constants/icons";
import useColorizer from "@/hooks/useColorizer";
import { router } from "expo-router";
import { useTranslation } from "react-i18next";
import { Text, View } from "react-native";

export default function Header() {
  const { t } = useTranslation("updateProfile");
  const { c } = useColorizer();
  const { black, white } = colors;
  
  return (
    <View className='h-[7.4vh] bg-white_black flex-row items-center justify-between px-6'>
      <CloseIcon
        color={c(black.DEFAULT, white.DEFAULT)}
        width={28}
        height={28}
        onPress={router.back}
      />
      <Text className='text-black_white font-medium text-xl'>{t("updateProfile")}</Text>
      <Button>
        <Text className='font-medium text-xl text-primary'>{t("save")}</Text>
      </Button>
    </View>
  );
}
