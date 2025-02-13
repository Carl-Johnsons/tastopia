import { useGetUserByAccountId } from "@/api/user";
import NotFound from "@/app/+not-found";
import { globalStyles } from "@/components/common/GlobalStyles";
import Header from "@/components/screen/user/Header";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { useLocalSearchParams } from "expo-router";
import { ActivityIndicator, View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";

const Profile = () => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const { id: accountId } = useLocalSearchParams();

  const {
    data: accountDetailData,
    isLoading: isLoadingAccountDetail,
    refetch: refetchAccountDetail,
    isRefetching: isRefetchingAccountDetail
  } = useGetUserByAccountId(accountId as string);

  if (isLoadingAccountDetail) {
    return (
      <SafeAreaView
        style={{ backgroundColor: c(white.DEFAULT, black[100]), height: "100%" }}
      >
        <View className='flex-center size-full'>
          <ActivityIndicator
            color={globalStyles.color.primary}
            size={"large"}
          />
        </View>
      </SafeAreaView>
    );
  }

  if (!accountDetailData || !accountDetailData.isAccountActive) {
    return <NotFound />;
  }

  return (
    <SafeAreaView
      style={{ backgroundColor: c(white.DEFAULT, black[100]), height: "100%" }}
    >
      <Header
        displayName={accountDetailData.displayName}
        avatarUrl={accountDetailData.avatarUrl}
        backgroundUrl={accountDetailData.backgroundUrl}
        totalRecipe={accountDetailData.totalRecipe}
        totalFollower={accountDetailData.totalFollower}
        accountUsername={accountDetailData.accountUsername}
        bio={accountDetailData.bio ?? ""}
      />
    </SafeAreaView>
  );
};

export default Profile;
