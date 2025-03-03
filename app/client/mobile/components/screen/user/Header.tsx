import { useFollowUnfollowUser } from "@/api/user";
import Button from "@/components/Button";
import PreviewImage from "@/components/common/PreviewImage";
import { colors } from "@/constants/colors";
import { ArrowBackIcon, DotIcon } from "@/constants/icons";
import useIsOwner from "@/hooks/auth/useIsOwner";
import { useProtectedExclude } from "@/hooks/auth/useProtected";
import { ROLE } from "@/slices/auth.slice";
import { formatDate } from "@/utils/format-date";
import { Feather } from "@expo/vector-icons";
import { ImageBackground } from "expo-image";
import { LinearGradient } from "expo-linear-gradient";
import { router } from "expo-router";
import { useCallback, useState } from "react";
import { useTranslation } from "react-i18next";
import { View, Text, Alert, TouchableWithoutFeedback } from "react-native";

type HeaderProps = {
  accountId: string;
  displayName: string;
  avatarUrl: string;
  backgroundUrl: string;
  totalRecipe: number | undefined;
  totalFollower?: number;
  accountUsername: string;
  bio: string;
  createdAt: string;
  isCurrentUser: boolean;
  isFollowing: boolean;
  handleTouchMenu: () => void;
  handleTouchFollowerCount: () => void;
};

export default function Header({
  accountId,
  displayName,
  avatarUrl,
  backgroundUrl,
  totalRecipe,
  totalFollower,
  accountUsername,
  bio,
  createdAt,
  isCurrentUser,
  isFollowing,
  handleTouchMenu,
  handleTouchFollowerCount
}: HeaderProps) {
  const { t } = useTranslation("profile");
  const { black, white } = colors;
  const followerCounts = totalFollower || 0;
  const [followed, setIsFollowed] = useState<boolean>(isFollowing);
  const { mutateAsync: followUser, isLoading } = useFollowUnfollowUser();
  const isOwnedByCurrentUser = useIsOwner(accountId);

  const goToUpdateProfile = useCallback(() => {
    router.push("/(protected)/menu/profile/updateProfile");
  }, [router]);

  const handleFollowUnFollow = useProtectedExclude(() => {
    if (!isLoading) {
      followUser(
        { accountId },
        {
          onSuccess: async data => {
            setIsFollowed(data.isFollowing);
          },
          onError: async error => {
            console.log("Follow unfollow error", JSON.stringify(error, null, 2));
            Alert.alert(t("followUnfollowError"));
          }
        }
      );
    }
  }, [ROLE.GUEST]);

  return (
    <View>
      <ImageBackground
        imageStyle={{ opacity: 10 }}
        source={backgroundUrl}
      >
        <LinearGradient colors={["#00000040", black[200] as string]}>
          <View className='flex gap-2 px-4 pb-4 pt-2'>
            <View className='flex-row justify-between'>
              <ArrowBackIcon
                width={28}
                height={28}
                color={white.DEFAULT}
                onPress={router.back}
              />
              {!isOwnedByCurrentUser && (
                <TouchableWithoutFeedback onPress={handleTouchMenu}>
                  <View>
                    <Feather
                      name='more-horizontal'
                      size={28}
                      color={white.DEFAULT}
                    />
                  </View>
                </TouchableWithoutFeedback>
              )}
            </View>

            <View className='relative flex-row items-center justify-between'>
              <View className='flex gap-y-5'>
                <View className='bg-white_black flex aspect-square w-[100px] items-center justify-center rounded-full'>
                  <PreviewImage
                    imgUrl={avatarUrl}
                    className='size-[90px] rounded-full border-2 border-white bg-[#FFC529]'
                    defaultImage={require("../../../assets/images/avatar.png")}
                  />
                </View>

                <Text className='w-[70vw] font-semibold text-2xl text-white'>
                  {displayName}
                </Text>

                <View>
                  {!!totalRecipe && (
                    <Text className='font-secondary-roman text-lg text-white'>
                      {totalRecipe}{" "}
                      {totalRecipe && totalRecipe % 2 === 0 && totalRecipe !== 0
                        ? t("recipes")
                        : t("recipe")}
                    </Text>
                  )}

                  <Text
                    onPress={isOwnedByCurrentUser ? handleTouchFollowerCount : undefined}
                    className='font-secondary-roman text-lg text-white'
                  >
                    {followerCounts}{" "}
                    {followerCounts % 2 === 0 && followerCounts !== 0
                      ? t("follower")
                      : t("followers")}
                  </Text>

                  <View className='flex-row items-center gap-1'>
                    <Text className='font-secondary-roman text-lg text-gray-200'>
                      @{accountUsername}
                    </Text>
                    <DotIcon
                      width={4}
                      height={4}
                      color={white.DEFAULT}
                    />
                    <Text className='font-secondary-roman text-lg text-gray-200'>
                      {formatDate(createdAt)}
                    </Text>
                  </View>
                  {bio && (
                    <Text className='font-secondary-roman text-lg text-gray-200'>
                      {bio}
                    </Text>
                  )}
                </View>
              </View>

              <View className='absolute right-0'>
                {isCurrentUser ? (
                  <Button
                    className='rounded-full border border-white px-4 py-3'
                    onPress={goToUpdateProfile}
                  >
                    <Text className='font-sans text-white'>{t("update")}</Text>
                  </Button>
                ) : (
                  <Button
                    className='rounded-full border border-white px-4 py-3'
                    onPress={handleFollowUnFollow}
                  >
                    <Text className='font-sans text-white'>
                      {followed ? t("unfollow") : t("follow")}
                    </Text>
                  </Button>
                )}
              </View>
            </View>
          </View>
        </LinearGradient>
      </ImageBackground>
    </View>
  );
}
