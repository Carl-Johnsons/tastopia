import { Image } from "expo-image";
import { LogoIcon } from "@/components/common/SVG";
import Filter from "@/components/screen/community/Filter";
import { Text, View, TouchableWithoutFeedback } from "react-native";
import { selectUser } from "@/slices/user.slice";
import Protected from "@/components/Protected";
import { ROLE } from "@/slices/auth.slice";
import { useTranslation } from "react-i18next";

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
  const { avatarUrl, displayName } = selectUser();

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
          <View className='items-center flex-row gap-3'>
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
            <View className='gap-2 grow'>
              <Text className='font-bold text-black_white'>{displayName}</Text>
              <TouchableWithoutFeedback onPress={handleCreateRecipe}>
                <View className='w-full rounded-2xl border border-gray-400 dark:border-gray-200 px-4 py-3'>
                  <Text className='text-gray-600'>{t("headerCreateRecipe")}</Text>
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
