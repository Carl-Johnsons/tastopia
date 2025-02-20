import { colors } from "@/constants/colors";
import { ArrowBackIcon } from "@/constants/icons";
import useColorizer from "@/hooks/useColorizer";
import { router } from "expo-router";
import { useTranslation } from "react-i18next";
import { Text, View } from "react-native";

const DeletedHeader = () => {
  const { t } = useTranslation("menu");

  const { c } = useColorizer();
  const { black, white } = colors;

  return (
    <View className='mb-6 px-4 pt-2'>
      <View className='flex-row justify-between'>
        <ArrowBackIcon
          width={28}
          height={28}
          color={c(black.DEFAULT, white.DEFAULT)}
          onPress={router.back}
        />

        <View className='absolute left-1/2 -translate-x-1/2 items-center'>
          <Text className='text-black_white h3-semibold'>{t("deleted")}</Text>
        </View>
      </View>
    </View>
  );
};

export default DeletedHeader;
