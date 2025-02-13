import { Alert, StyleSheet, Text, TouchableWithoutFeedback, View } from "react-native";
import { useState } from "react";
import { Image } from "expo-image";
import { abbreviateNumber } from "@/utils/format";
import { useFollowUnfollowUser } from "@/api/user";
import { useTranslation } from "react-i18next";

type UserProps = {
  id: string;
  avtUrl: string;
  displayName: string;
  username: string;
  numberOfRecipe: number;
  isFollowing: boolean;
  invalidateSearch: () => void;
};

const User = ({
  id,
  avtUrl,
  displayName,
  username,
  numberOfRecipe,
  isFollowing,
  invalidateSearch
}: UserProps) => {
  const { t } = useTranslation("search");

  const [followed, setIsFollowed] = useState<boolean>(isFollowing);
  const { mutateAsync: followUser, isLoading } = useFollowUnfollowUser();

  const handleFollowUnFollow = () => {
    if (!isLoading) {
      followUser(
        { accountId: id },
        {
          onSuccess: async data => {
            invalidateSearch();
            setIsFollowed(data.isFollowing);
          },
          onError: async error => {
            console.log("Follow unfollow error", JSON.stringify(error, null, 2));
            Alert.alert(t("followUnfollowError"));
          }
        }
      );
    }
  };

  return (
    <TouchableWithoutFeedback
      key={id}
      onPress={() => {
        console.log("press user card");
      }}
    >
      <View className='flex-row items-start justify-between gap-16'>
        <View className='flex-1 flex-row gap-2'>
          <Image
            source={avtUrl}
            style={{
              width: 36,
              height: 36,
              borderRadius: 100
            }}
          />
          <View>
            <Text
              numberOfLines={1}
              ellipsizeMode='tail'
              className='base-medium text-black_white'
            >
              {displayName}
            </Text>
            <Text
              numberOfLines={1}
              ellipsizeMode='tail'
              className='paragraph-medium text-black_white'
            >
              @{username}
            </Text>
            <Text className='body-regular text-black_white'>
              {abbreviateNumber(numberOfRecipe)}{" "}
              {numberOfRecipe === 1 ? "recipe" : "recipes"}
            </Text>
          </View>
        </View>
        <View>
          <TouchableWithoutFeedback onPress={handleFollowUnFollow}>
            <View className='rounded-3xl bg-primary px-6 py-2'>
              <Text className='paragraph-bold text-white_black text-center'>
                {followed ? t("unfollow") : t("follow")}
              </Text>
            </View>
          </TouchableWithoutFeedback>
        </View>
      </View>
    </TouchableWithoutFeedback>
  );
};

export default User;

const styles = StyleSheet.create({});
