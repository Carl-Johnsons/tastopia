import { useGetNotification } from "@/api/notification";
import { colors } from "@/constants/colors";
import {
  ArrowDownFillIcon,
  ArrowUpFillIcon,
  ChatBubbleFillIcon,
  UserIcon
} from "@/constants/icons";
import useColorizer from "@/hooks/useColorizer";
import { Avatar } from "@rneui/base";
import { router, useFocusEffect, usePathname } from "expo-router";
import { useCallback, useEffect, useMemo, useState } from "react";
import { Pressable, RefreshControl, StyleSheet, Text, View } from "react-native";
import { ActivityIndicator, FlatList } from "react-native";
import Empty from "../community/Empty";
import { stringify } from "@/utils/debug";
import { INotificationsResponse } from "@/generated/interfaces/notification.interface";
import { filterUniqueItems } from "@/utils/dataFilter";

export default function NotificationList() {
  const {
    data,
    isLoading,
    refetch,
    isStale,
    isFetchingNextPage,
    hasNextPage,
    fetchNextPage
  } = useGetNotification();
  const { primary } = colors;
  const [notifications, setNotifications] = useState<INotificationsResponse[]>([]);

  const fetchData = useCallback(() => {
    if (isStale) {
      refetch();
    }
  }, [isStale]);

  useFocusEffect(fetchData);

  const handleRefreshing = useCallback(async () => {
    await refetch();
  }, [refetch]);

  const handleLoadMore = () => {
    if (!isFetchingNextPage && hasNextPage) {
      fetchNextPage();
    }
  };

  useEffect(() => {
    if (data?.pages) {
      setNotifications(filterUniqueItems(data.pages));
    }
  }, [data]);

  if (isLoading) {
    return (
      <View className='flex-center h-full w-full'>
        <ActivityIndicator
          size='large'
          color={primary}
        />
      </View>
    );
  }

  return (
    <View className='pb-[85px] pt-2'>
      <FlatList
        className='h-full'
        data={notifications}
        onEndReached={handleLoadMore}
        onEndReachedThreshold={0.1}
        ListEmptyComponent={() => (
          <View style={{ flex: 1, marginTop: 50 }}>
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
            onRefresh={handleRefreshing}
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
  const { message, imageUrl, jsonData, code } = item;
  const currentRouteName = usePathname();
  const isOddItem = useMemo(() => {
    return index % 2 === 0;
  }, [index]);
  const { white, black, primary } = colors;
  const { c } = useColorizer();

  const handlePress = useCallback(() => {
    if (!jsonData) return;

    const NOTIFICATION_ROUTE = "/(protected)/notification";
    const { redirectUri } = JSON.parse(jsonData) as INotificationJsonData;

    if (!redirectUri || redirectUri === NOTIFICATION_ROUTE) return;
    router.push(redirectUri as any);
  }, [jsonData, currentRouteName]);

  const styles = StyleSheet.create({
    oddWrapper: {
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
            color={c(white.DEFAULT, black.DEFAULT)}
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
            color={c(white.DEFAULT, black.DEFAULT)}
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
            color={c(white.DEFAULT, black.DEFAULT)}
          />
        );
    }
  };

  return (
    <Pressable
      onPress={handlePress}
      style={isOddItem && styles.oddWrapper}
      className={`relative flex-row gap-2 p-4`}
    >
      <View className='relative'>
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
      <View className='shrink justify-center pt-2'>
        <Text className='text-black_white'>{message}</Text>
      </View>
    </Pressable>
  );
};
