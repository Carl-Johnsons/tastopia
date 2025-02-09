import { useTranslation } from "react-i18next";
import { Text, View, Image } from "react-native";

const NotFound = () => {
  const { t } = useTranslation("component");
  return (
    <View className='flex-center bg-white_black100 flex-1 gap-2'>
      <Image
        source={require("../assets/icons/noResult.png")}
        style={{ width: 130, height: 130 }}
      />
      <Text className='paragraph-medium text-black_white text-center'>
        {t("notfound")}
      </Text>
    </View>
  );
};

export default NotFound;
