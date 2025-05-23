import { useGetUserByAccountId } from "@/api/user";
import NotFound from "@/app/+not-found";
import LoadingFullScreen from "@/components/common/LoadingFullScreen";
import SettingComment from "@/components/common/SettingComment";
import SettingRecipe from "@/components/common/SettingRecipe";
import SettingUser from "@/components/common/SettingUser";
import FollowModal from "@/components/screen/profile/FollowModal";
import Body from "@/components/screen/user/Body";
import Header from "@/components/screen/user/Header";
import { colors } from "@/constants/colors";
import useBottomSheetModal from "@/hooks/useBottomSheetModal";
import useColorizer from "@/hooks/useColorizer";
import BottomSheet from "@gorhom/bottom-sheet";
import { useFocusEffect, useLocalSearchParams } from "expo-router";
import { useCallback, useRef, useState } from "react";
import { StatusBar } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";

const Profile = () => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const { id: accountId } = useLocalSearchParams();

  const bottomSheetRef = useRef<BottomSheet>(null);
  const bottomSheetCommentRef = useRef<BottomSheet>(null);
  const bottomSheetUserRef = useRef<BottomSheet>(null);
  const { ref: followModalRef, openModal: openFollowModal } = useBottomSheetModal();
  const [currentRecipeId, setCurrentRecipeId] = useState("");
  const [currentAuthorId, setCurrentAuthorId] = useState("");
  const [currentComment, setCurrentComment] = useState<CommentCustomType>({
    id: "",
    content: ""
  });
  const [currentCommentAuthorId, setCurrentCommentAuthorId] = useState("");
  const {
    data: accountDetailData,
    isLoading: isLoadingAccountDetail,
    isStale,
    refetch: refectAccountDetailData
  } = useGetUserByAccountId(accountId as string);

  const fetchData = useCallback(() => {
    if (isStale) {
      refectAccountDetailData();
    }
  }, [isStale]);

  const handleTouchMenu = () => {
    bottomSheetUserRef.current?.expand();
  };

  const handleTouchFollowerCount = useCallback(() => {
    openFollowModal();
  }, [openFollowModal]);

  useFocusEffect(fetchData);

  if (isLoadingAccountDetail) {
    return <LoadingFullScreen />;
  }

  if (
    (!accountDetailData || !accountDetailData.isAccountActive) &&
    !isLoadingAccountDetail
  ) {
    return <NotFound />;
  }

  return (
    <SafeAreaView
      style={{ backgroundColor: c(white.DEFAULT, black[100]), height: "100%" }}
    >
      <StatusBar backgroundColor={c(white.DEFAULT, black[100])} />
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
        handleTouchMenu={handleTouchMenu}
        handleTouchFollowerCount={handleTouchFollowerCount}
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
      <SettingUser
        id={currentRecipeId}
        authorId={accountId as string}
        ref={bottomSheetUserRef}
      />
      <FollowModal ref={followModalRef} />
    </SafeAreaView>
  );
};

export default Profile;
