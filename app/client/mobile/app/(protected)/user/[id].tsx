import { useGetUserByAccountId } from "@/api/user";
import NotFound from "@/app/+not-found";
import { globalStyles } from "@/components/common/GlobalStyles";
import SettingComment from "@/components/common/SettingComment";
import SettingRecipe from "@/components/common/SettingRecipe";
import Body from "@/components/screen/user/Body";
import Header from "@/components/screen/user/Header";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import BottomSheet from "@gorhom/bottom-sheet";
import { useLocalSearchParams } from "expo-router";
import { useRef, useState } from "react";
import { ActivityIndicator, View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";

const Profile = () => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const { id: accountId } = useLocalSearchParams();

  const bottomSheetRef = useRef<BottomSheet>(null);
  const bottomSheetCommentRef = useRef<BottomSheet>(null);
  const [currentRecipeId, setCurrentRecipeId] = useState("");
  const [currentAuthorId, setCurrentAuthorId] = useState("");
  const [currentComment, setCurrentComment] = useState<CommentCustomType>({
    id: "",
    content: ""
  });
  const [currentCommentAuthorId, setCurrentCommentAuthorId] = useState("");
  const { data: accountDetailData, isLoading: isLoadingAccountDetail } =
    useGetUserByAccountId(accountId as string);

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
        accountId={accountDetailData.accountId}
        displayName={accountDetailData.displayName}
        avatarUrl={accountDetailData.avatarUrl}
        backgroundUrl={accountDetailData.backgroundUrl}
        totalRecipe={accountDetailData.totalRecipe}
        totalFollower={accountDetailData.totalFollower}
        accountUsername={accountDetailData.accountUsername}
        bio={accountDetailData.bio ?? ""}
        createdAt={accountDetailData.createdAt}
        isCurrentUser={accountDetailData.isCurrentUser}
        isFollowing={accountDetailData.isFollowing}
      />
      <Body
        accountId={accountDetailData.accountId}
        bottomSheetRef={bottomSheetRef}
        bottomSheetCommentRef={bottomSheetCommentRef}
        setCurrentRecipeId={setCurrentRecipeId}
        setCurrentAuthorId={setCurrentAuthorId}
        setCurrentComment={setCurrentComment}
        setCurrentCommentAuthorId={setCurrentCommentAuthorId}
      />
      <SettingRecipe
        id={currentRecipeId}
        authorId={currentAuthorId}
        ref={bottomSheetRef}
      />
      {currentComment?.id && currentComment?.content && (
        <SettingComment
          id={currentComment.id}
          recipeId={currentRecipeId}
          content={currentComment.content}
          authorId={currentCommentAuthorId}
          ref={bottomSheetCommentRef}
        />
      )}
    </SafeAreaView>
  );
};

export default Profile;
