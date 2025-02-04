import { memo } from "react";
import { useTranslation } from "react-i18next";
import { View, Image, Text } from "react-native";

const PermissionDenied = memo(() => {
  const { t } = useTranslation("component");
  return (
    <View className='flex-center h-[70%] gap-2'>
      <Image
        source={require("../../assets/icons/noResult.png")}
        style={{ width: 130, height: 130 }}
      />
      <Text className='paragraph-medium text-black_white text-center'>
        {t("permissionDenied")}
      </Text>
    </View>
  );
});

export default PermissionDenied;
