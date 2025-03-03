import { Image } from "expo-image";
import { LogoIcon } from "@/components/common/SVG";
import Filter from "@/components/screen/community/Filter";
import { Text, View, TouchableWithoutFeedback, Pressable } from "react-native";
import { selectUser } from "@/slices/user.slice";
import Protected from "@/components/Protected";
import { ROLE } from "@/slices/auth.slice";
import { useTranslation } from "react-i18next";
import { useCallback } from "react";
import { router } from "expo-router";

type HeaderProps = {
  isRefreshing: boolean;
  handleFilter: (key: string) => void;
  filterSelected: string;
  handleCreateRecipe: () => void;
};

function Header({
  isRefreshing,
  handleFilter,
  filterSelected,
  handleCreateRecipe
}: HeaderProps) {
  const { t } = useTranslation("community");
  const { avatarUrl, displayName, accountId } = selectUser();

  const goToProfile = useCallback(() => {
    router.push({
      pathname: "/(protected)/user/[id]",
      params: { id: accountId ?? "" }
    });
  }, [router, accountId]);

  return (
    <View className='mb-8 gap-8'>
      <View className='flex-center'>
        <LogoIcon
          isActive={isRefreshing}
          width={60}
          height={60}
        />
      </View>

      <Protected excludedRoles={[ROLE.GUEST]}>
        <View className='flex-start mt-2 flex-row px-6'>
          <View className='w-full flex-row items-center gap-3'>
            <Pressable onPress={goToProfile}>
              <Image
                cachePolicy={"disk"}
                source={
                  avatarUrl
                    ? { uri: avatarUrl }
                    : require("../../../assets/images/avatar.png")
                }
                style={{
                  width: 40,
                  height: 40,
                  borderRadius: 8,
                  backgroundColor: "#FFC529"
                }}
              />
            </Pressable>
            <View className='grow gap-2'>
              <Text
                onPress={goToProfile}
                className={`paragraph-bold text-black_white`}
              >
                {displayName}
              </Text>
              <TouchableWithoutFeedback onPress={handleCreateRecipe}>
                <View className='flex rounded-2xl border border-gray-400 px-4 py-3 dark:border-gray-200'>
                  <Text className='text-black_white'>{t("headerCreateRecipe")}</Text>
                </View>
              </TouchableWithoutFeedback>
            </View>
          </View>
        </View>
      </Protected>

      <Filter
        handleSelect={handleFilter}
        filterSelected={filterSelected}
      />
    </View>
  );
}

export default Header;
