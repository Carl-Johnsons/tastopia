import { useGetNotification, useSetViewedNotification } from "@/api/notification";
import { colors } from "@/constants/colors";
import {
  ArrowDownFillIcon,
  ArrowUpFillIcon,
  ChatBubbleFillIcon,
  UserIcon
} from "@/constants/icons";
import { FlatList } from "react-native";
import { Avatar } from "@rneui/base";
import { enUS, vi } from "date-fns/locale";
import { formatDistanceToNow } from "date-fns";
import { INotificationsResponse } from "@/generated/interfaces/notification.interface";
import { Pressable, RefreshControl, StyleSheet, Text, View } from "react-native";
import { router, useFocusEffect, usePathname } from "expo-router";
import { saveNotificationData } from "@/slices/notification.slice";
import { useAppDispatch } from "@/store/hooks";
import { ReactElement, useCallback, useEffect, useMemo, useState } from "react";
import { useQueryClient } from "react-query";
import { useTranslation } from "react-i18next";
import Empty from "../community/Empty";
import i18n from "@/i18n/i18next";
import useHydrateData from "@/hooks/useHydrateData";
import { useErrorHandler } from "@/hooks/useErrorHandler";
import CustomTab, { ItemProps } from "@/components/common/Tab";
import { TabView } from "@rneui/themed";
import Header from "./Header";
import { NotificationCategories } from "@/generated/enums/notification.enum";
import { NotificationTemplateCode } from "@/constants/notifications";
import { AntDesign, Feather } from "@expo/vector-icons";

export default function NotificationList() {
  const { t } = useTranslation("notification");

  const tabItems: ItemProps[] = useMemo(
    () => [{ title: t("community") }, { title: t("system") }],
    [t]
  );

  const tabViews: ReactElement[] = useMemo(
    () => [
      <Tab
        key='CommunityTab'
        type={NotificationCategories.USER}
      />,
      <Tab
        key='SystemTab'
        type={NotificationCategories.SYSTEM}
      />
    ],
    []
  );

  return (
    <View className='h-[92vh]'>
      <Header />
      <CustomTab
        tabItems={tabItems}
        tabViews={tabViews}
      />
    </View>
  );
}

type INotificationJsonData = {
  redirectUri: string;
};

export const Notification = ({
  item,
  type
}: {
  item: INotificationsResponse;
  index: number;
  type: NotificationCategories;
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

  const renderIcon = useCallback(
    (code: NotificationTemplateCode) => {
      switch (code) {
        case NotificationTemplateCode.USER_COMMENT:
          return (
            <ChatBubbleFillIcon
              width={24}
              height={24}
              color={primary}
            />
          );
        case NotificationTemplateCode.USER_CREATE_RECIPE:
          return (
            <AntDesign
              name='plus'
              size={24}
              color={primary}
            />
          );
        case NotificationTemplateCode.USER_FOLLOW:
          return (
            <UserIcon
              width={24}
              height={24}
            />
          );
        case NotificationTemplateCode.USER_REPLY:
          return (
            <ChatBubbleFillIcon
              width={24}
              height={24}
              color={primary}
            />
          );
        case NotificationTemplateCode.USER_UPVOTE:
          return (
            <ArrowUpFillIcon
              width={24}
              height={24}
              color={primary}
            />
          );
        case NotificationTemplateCode.USER_DOWNVOTE:
          return (
            <ArrowDownFillIcon
              width={24}
              height={24}
              color={primary}
            />
          );
        case NotificationTemplateCode.SYSTEM_DISABLE_RECIPE:
        case NotificationTemplateCode.ADMIN_DISABLE_RECIPE:
        case NotificationTemplateCode.ADMIN_DISABLE_COMMENT:
          return (
            <Feather
              name='slash'
              size={24}
              color={primary}
            />
          );
        case NotificationTemplateCode.ADMIN_RESTORE_RECIPE:
        case NotificationTemplateCode.ADMIN_RESTORE_COMMENT:
          return (
            <Feather
              name='rotate-cw'
              size={24}
              color={primary}
            />
          );
        default:
          console.log("Unknown notification code:", code);
          return (
            <ChatBubbleFillIcon
              width={24}
              height={24}
              color={primary}
            />
          );
      }
    },
    [primary]
  );

  return (
    <Pressable
      onPress={handlePress}
      style={!isViewed && styles.isViewed}
      className={`relative flex-row gap-3 p-4`}
    >
      <View className='relative'>
        <View className='max-h-[75px]'>
          {type === NotificationCategories.SYSTEM ? (
            <>
              <View className='flex-center size-[70px] rounded-full border border-gray-600'>
                {renderIcon(code as NotificationTemplateCode)}
              </View>
            </>
          ) : (
            <>
              <Avatar
                size={70}
                rounded
                source={
                  imageUrl
                    ? { uri: imageUrl }
                    : require("../../../assets/images/avatar.png")
                }
                containerStyle={imageUrl && { backgroundColor: "#FFC529" }}
              />
              <View className='absolute bottom-0 right-0'>
                {renderIcon(code as NotificationTemplateCode)}
              </View>
            </>
          )}
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

type TabProps = {
  type: NotificationCategories;
};

const Tab = ({ type }: TabProps) => {
  const { primary } = colors;

  const [notifications, setNotifications] = useState<INotificationsResponse[]>();
  const { data, isLoading, isStale, refetch, fetchNextPage } = useGetNotification(type);
  useHydrateData({ source: data, setter: setNotifications });

  const dispatch = useAppDispatch();
  const fetchData = useCallback(() => {
    if (isStale) {
      refetch();
    }
  }, [isStale]);

  useFocusEffect(fetchData);

  useEffect(() => {
    dispatch(
      saveNotificationData({
        unreadNotifications: data?.pages[0].metadata?.unreadNotifications ?? 0
      })
    );
  }, [data]);

  return (
    <TabView.Item className='w-full'>
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
              type={type}
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
    </TabView.Item>
  );
};
