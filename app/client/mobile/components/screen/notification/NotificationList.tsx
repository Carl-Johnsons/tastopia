import { useGetNotification } from "@/api/notification";
import { colors } from "@/constants/colors";
import { ChatBubbleFillIcon } from "@/constants/icons";
import useColorizer from "@/hooks/useColorizer";
import { Avatar } from "@rneui/base";
import { router, useFocusEffect, usePathname } from "expo-router";
import { useCallback, useMemo } from "react";
import { useTranslation } from "react-i18next";
import { Pressable, RefreshControl, StyleSheet, Text, View } from "react-native";
import { ActivityIndicator, FlatList } from "react-native";

export default function NotificationList() {
  const { data, isLoading, refetch, isStale } = useGetNotification();
  const { primary } = colors;
  const { t } = useTranslation("notification");

  const fetchData = useCallback(() => {
    if (isStale) {
      refetch();
    }
  }, [isStale]);

  useFocusEffect(fetchData);

  const handleRefreshing = useCallback(async () => {
    await refetch();
  }, [refetch]);

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

  if (data?.paginatedData?.length && data?.paginatedData?.length === 0) {
    return (
      <View className='text-black_white flex-center h-full w-full'>
        <Text>{t("noNotification")}</Text>
      </View>
    );
  }

  return (
    <View className='pt-2'>
      <FlatList
        className='h-full'
        data={data?.paginatedData}
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

export const Notification = ({
  item,
  index
}: {
  item: INotificationsResponse;
  index: number;
}) => {
  const { message, imageUrl, jsonData } = item;
  const currentRouteName = usePathname();
  const isOddItem = useMemo(() => {
    return index % 2 === 0;
  }, [index]);
  const { white, black } = colors;
  const { c } = useColorizer();

  const handlePress = useCallback(() => {
    if (jsonData) {
      const data = JSON.parse(jsonData);

      if (data.redirectUri && currentRouteName !== "/notification") {
        router.push(data.redirectUri);
      }
    }
  }, [jsonData, currentRouteName]);

  const styles = StyleSheet.create({
    oddWrapper: {
      backgroundColor: "#FE724C26"
    }
  });

  const renderIcon = () => {
    //TODO: Add conditions to render different icons

    return (
      <ChatBubbleFillIcon
        width={24}
        height={24}
        color={c(white.DEFAULT, black.DEFAULT)}
      />
    );
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
        <View className='absolute bottom-0 right-0'>{renderIcon()}</View>
      </View>
      <View className='shrink justify-center pt-2'>
        <Text className='text-black_white'>{message}</Text>
      </View>
    </Pressable>
  );
};
