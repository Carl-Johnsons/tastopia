import { memo } from "react";
import { View, Text, Image } from "react-native";
import { useTranslation } from "react-i18next";

type EmptyProps = {
  type?:
    | "empty"
    | "emptyRecipe"
    | "emptyComment"
    | "emptyBookmark"
    | "emptyNotification"
    | "emptyDeleted"
    | "emptyRecipeViewingHistory"
    | "emptyFollowing"
    | "emptyFollowingSearch"
    | "emptyFollower"
    | "emptyFollowerSearch";
};

const Empty = memo(({ type = "empty" }: EmptyProps) => {
  const { t } = useTranslation("component");

  return (
    <View className='flex-center h-[70%] gap-2'>
      <Image
        source={require("../../../assets/icons/noResult.png")}
        style={{ width: 130, height: 130 }}
      />
      <Text className='paragraph-medium text-black_white text-center'>{t(type)}</Text>
    </View>
  );
});

export default Empty;
