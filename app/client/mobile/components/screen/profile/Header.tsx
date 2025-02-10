import Button from "@/components/Button";
import { colors } from "@/constants/colors";
import { ArrowBackIcon, DotIcon, ShareIcon } from "@/constants/icons";
import { selectUser } from "@/slices/user.slice";
import { Avatar } from "@rneui/base";
import { ImageBackground } from "expo-image";
import { LinearGradient } from "expo-linear-gradient";
import { router } from "expo-router";
import { useCallback } from "react";
import { useTranslation } from "react-i18next";
import { View, Text } from "react-native";

export default function Header() {
  const { t } = useTranslation("profile");
  const { displayName, accountUsername, avatarUrl, totalFollower } = selectUser();
  const { backgroundUrl, bio } = selectUser();
  const { black, white } = colors;

  const followerCounts = totalFollower || 0;
  const joinDate = "1 Jan 2025";

  const goToUpdateProfile = useCallback(() => {
    router.push("/(protected)/menu/profile/updateProfile");
  }, [router]);

  return (
    <View className='h-[33vh]'>
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
                  <Avatar
                    size={90}
                    rounded
                    source={
                      avatarUrl
                        ? { uri: avatarUrl }
                        : require("../../../assets/images/avatar.png")
                    }
                    containerStyle={avatarUrl && { backgroundColor: "#FFC529" }}
                  />
                </View>

                <Text className='font-semibold text-xl text-white'>{displayName}</Text>

                <View>
                  <Text className='font-secondary-roman text-sm text-white'>
                    {followerCounts} follower
                    {followerCounts % 2 === 0 && followerCounts !== 0 ? "s" : ""}
                  </Text>

                  <View className='flex-row items-center gap-1'>
                    <Text className='font-secondary-roman text-sm text-gray-200'>
                      @{accountUsername}
                    </Text>
                    <DotIcon
                      width={4}
                      height={4}
                      color={white.DEFAULT}
                    />
                    <Text className='font-secondary-roman text-sm text-gray-200'>
                      {joinDate}
                    </Text>
                  </View>
                  {bio && (
                    <Text className='font-secondary-roman text-sm text-gray-200'>
                      {bio}
                    </Text>
                  )}
                </View>
              </View>

              <Button
                className='rounded-full border border-white p-2'
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
