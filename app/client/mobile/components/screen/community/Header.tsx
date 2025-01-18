import { Image } from "expo-image";
import { LogoIcon } from "@/components/common/SVG";
import Filter from "@/components/screen/community/Filter";
import { Text, View, TouchableWithoutFeedback } from "react-native";
import { selectUser } from "@/slices/user.slice";
import Protected from "@/components/Protected";
import { ROLE } from "@/slices/auth.slice";
import { useTranslation } from "react-i18next";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";
import useDarkMode from "@/hooks/useDarkMode";

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

      <Filter
        handleSelect={handleFilter}
        filterSelected={filterSelected}
      />
      <Protected excludedRoles={[ROLE.GUEST]}>
        <View className='flex-start mt-2 flex-row px-6'>
          <View className='flex-row gap-3'>
            <Image
              cachePolicy={"disk"}
              source={
                avatarUrl
                  ? { uri: avatarUrl }
                  : require("../../../assets/images/avatar.png")
              }
              style={{ width: 50, height: 50, borderRadius: 100 }}
            />
            <View className='gap-2'>
              <Text className={`paragraph-bold text-black_white`}>{displayName}</Text>
              <TouchableWithoutFeedback onPress={handleCreateRecipe}>
                <View className='min-w-[250px] rounded-2xl border-[1px] border-gray-600 px-4 py-3'>
                  <Text className='text-black_white'>{t("headerCreateRecipe")}</Text>
                </View>
              </TouchableWithoutFeedback>
            </View>
          </View>
        </View>
      </Protected>
    </View>
  );
}

export default Header;
