import { useGetNotification, useSetViewedNotification } from "@/api/notification";
import { colors } from "@/constants/colors";
import {
  ArrowDownFillIcon,
  ArrowUpFillIcon,
  ChatBubbleFillIcon,
  UserIcon
} from "@/constants/icons";
import { ActivityIndicator, FlatList } from "react-native";
import { Avatar } from "@rneui/base";
import { enUS, vi } from "date-fns/locale";
import { formatDistanceToNow } from "date-fns";
import { INotificationsResponse } from "@/generated/interfaces/notification.interface";
import { Pressable, RefreshControl, StyleSheet, Text, View } from "react-native";
import { router, usePathname } from "expo-router";
import { saveNotificationData } from "@/slices/notification.slice";
import { useAppDispatch } from "@/store/hooks";
import { useCallback, useEffect, useMemo, useState } from "react";
import { useQueryClient } from "react-query";
import { useTranslation } from "react-i18next";
import Empty from "../community/Empty";
import i18n from "@/i18n/i18next";
import useHydrateData from "@/hooks/useHydrateData";
import { useErrorHandler } from "@/hooks/useErrorHandler";

export default function NotificationList() {
  const { data, isLoading, refetch, fetchNextPage } = useGetNotification();
  const { primary } = colors;
  const dispatch = useAppDispatch();
  const [notifications, setNotifications] = useState<INotificationsResponse[]>();
  useHydrateData({ source: data, setter: setNotifications });

  useEffect(() => {
    dispatch(
      saveNotificationData({
        unreadNotifications: data?.pages[0].metadata?.unreadNotifications ?? 0
      })
    );
  }, [data]);

  if (isLoading || !notifications) {
    return (
      <View className='flex-center h-[90%] w-full'>
        <ActivityIndicator
          size='large'
          color={primary}
        />
      </View>
    );
  }

  return (
    <View className='pb-[60px] pt-2'>
      <FlatList
        className='h-full'
        contentContainerStyle={{ paddingBottom: 25 }}
        data={notifications}
        onEndReached={() => fetchNextPage()}
        onEndReachedThreshold={0.1}
        ListEmptyComponent={() => (
          <View className='h-[100vh]'>
            <Empty type='emptyNotification' />
          </View>
        )}
        renderItem={({ item, index }) => (
          <Notification
            item={item}
            index={index}
          />
        )}
        refreshControl={
          <RefreshControl
            refreshing={isLoading}
            tintColor={primary}
            onRefresh={refetch}
          />
        }
      />
    </View>
  );
}

type INotificationJsonData = {
  redirectUri: string;
};

export const Notification = ({
  item,
  index
}: {
  item: INotificationsResponse;
  index: number;
}) => {
  const { message, imageUrl, jsonData, code, isViewed, id, createdAt } = item;
  const currentRouteName = usePathname();
  const { t } = useTranslation("component");
  const { primary } = colors;
  const { mutateAsync: setViewedNotification } = useSetViewedNotification();
  const queryClient = useQueryClient();
  const { handleError } = useErrorHandler();

  const handlePress = useCallback(async () => {
    if (!jsonData) return;

    const NOTIFICATION_ROUTE = "/(protected)/notification";
    const { redirectUri } = JSON.parse(jsonData) as INotificationJsonData;

    if (!isViewed) {
      await setViewedNotification(
        { notificationId: id },
        {
          onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: "getNotification" });
          },
          onError: error => handleError(error)
        }
      );
    }

    if (!redirectUri || redirectUri === NOTIFICATION_ROUTE) return;
    router.push(redirectUri as any);
  }, [jsonData, currentRouteName, id, isViewed]);

  const styles = StyleSheet.create({
    isViewed: {
      backgroundColor: "#FE724C26"
    }
  });

  type ActionCode =
    | "USER_REPLY"
    | "USER_FOLLOW"
    | "USER_COMMENT"
    | "USER_UPVOTE"
    | "USER_DOWNVOTE";

  const renderIcon = (code: ActionCode) => {
    switch (code) {
      case "USER_REPLY":
        return (
          <ChatBubbleFillIcon
            width={24}
            height={24}
            color={primary}
          />
        );
      case "USER_FOLLOW":
        return (
          <UserIcon
            width={24}
            height={24}
          />
        );
      case "USER_COMMENT":
        return (
          <ChatBubbleFillIcon
            width={24}
            height={24}
            color={primary}
          />
        );
      case "USER_UPVOTE":
        return (
          <ArrowUpFillIcon
            width={24}
            height={24}
            color={primary}
          />
        );
      case "USER_DOWNVOTE":
        return (
          <ArrowDownFillIcon
            width={24}
            height={24}
            color={primary}
          />
        );
      default:
        return (
          <ChatBubbleFillIcon
            width={24}
            height={24}
            color={primary}
          />
        );
    }
  };

  return (
    <Pressable
      onPress={handlePress}
      style={!isViewed && styles.isViewed}
      className={`relative flex-row gap-3 p-4`}
    >
      <View className='relative'>
        <View className='max-h-[75px]'>
          <Avatar
            size={70}
            rounded
            source={
              imageUrl ? { uri: imageUrl } : require("../../../assets/images/avatar.png")
            }
            containerStyle={imageUrl && { backgroundColor: "#FFC529" }}
          />
          <View className='absolute bottom-0 right-0'>
            {renderIcon(code as ActionCode)}
          </View>
        </View>
      </View>
      <View className='shrink justify-center pt-2'>
        <Text
          className={`${isViewed ? "text-gray-500" : "text-black_white"} text-xl`}
          numberOfLines={2}
        >
          {message}
        </Text>
        <Text className={`${isViewed ? "text-gray-500" : "text-black_white"}`}>
          {formatDistanceToNow(createdAt, {
            locale: i18n.languages[0] === "vi" ? vi : enUS
          })}
          {" " + t("ago")}
        </Text>
      </View>
    </Pressable>
  );
};
