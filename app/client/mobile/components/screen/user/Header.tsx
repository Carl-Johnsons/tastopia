import Button from "@/components/Button";
import PreviewImage from "@/components/common/PreviewImage";
import { colors } from "@/constants/colors";
import { ArrowBackIcon, DotIcon, ShareIcon } from "@/constants/icons";
import { ImageBackground } from "expo-image";
import { LinearGradient } from "expo-linear-gradient";
import { router } from "expo-router";
import { useCallback } from "react";
import { useTranslation } from "react-i18next";
import { View, Text } from "react-native";

type HeaderProps = {
  displayName: string;
  avatarUrl: string;
  backgroundUrl: string;
  totalRecipe: number | undefined;
  totalFollower?: number;
  accountUsername: string;
  bio: string;
};

export default function Header({
  displayName,
  avatarUrl,
  backgroundUrl,
  totalRecipe,
  totalFollower,
  accountUsername,
  bio
}: HeaderProps) {
  const { t } = useTranslation("profile");
  const { black, white } = colors;
  const followerCounts = totalFollower || 0;
  const joinDate = "1 Jan 2025";

  const goToUpdateProfile = useCallback(() => {
    router.push("/(protected)/menu/profile/updateProfile");
  }, [router]);

  return (
    <View>
      <ImageBackground
        imageStyle={{ opacity: 10 }}
        source={backgroundUrl}
      >
        <LinearGradient colors={["#00000040", black.DEFAULT]}>
          <View className='flex gap-2 px-4 pb-4 pt-2'>
            <View className='flex-row justify-between'>
              <ArrowBackIcon
                width={28}
                height={28}
                color={white.DEFAULT}
                onPress={router.back}
              />
              <ShareIcon
                width={28}
                height={28}
                color={white.DEFAULT}
              />
            </View>

            <View className='flex-row items-center justify-between'>
              <View className='flex gap-y-5'>
                <View className='bg-white_black flex aspect-square w-[100px] items-center justify-center rounded-full'>
                  <PreviewImage
                    imgUrl={avatarUrl}
                    className='size-[90px] rounded-full border-2 border-white bg-[#FFC529]'
                    defaultImage={require("../../../assets/images/avatar.png")}
                  />
                </View>

                <Text className='font-semibold text-2xl text-white'>{displayName}</Text>

                <View>
                  {totalRecipe && totalRecipe > 0 && (
                    <Text className='font-secondary-roman text-lg text-white'>
                      {totalRecipe}{" "}
                      {totalRecipe % 2 === 0 && totalRecipe !== 0
                        ? t("recipes")
                        : t("recipe")}
                    </Text>
                  )}

                  <Text className='font-secondary-roman text-lg text-white'>
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
                      {joinDate}
                    </Text>
                  </View>
                  {bio && (
                    <Text className='font-secondary-roman text-lg text-gray-200'>
                      {bio}
                    </Text>
                  )}
                </View>
              </View>

              <Button
                className='rounded-full border border-white px-4 py-3'
                onPress={goToUpdateProfile}
              >
                <Text className='font-sans text-white'>{t("update")}</Text>
              </Button>
            </View>
          </View>
        </LinearGradient>
      </ImageBackground>
    </View>
  );
}
