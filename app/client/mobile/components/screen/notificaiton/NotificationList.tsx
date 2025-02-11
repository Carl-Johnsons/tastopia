import { useGetNotification } from "@/api/notification";
import { colors } from "@/constants/colors";
import { stringify } from "@/utils/debug";
import { Avatar } from "@rneui/base";
import { router, usePathname, useRouter } from "expo-router";
import { useCallback, useEffect, useMemo } from "react";
import { Pressable, RefreshControl, StyleSheet, Text, View } from "react-native";
import { ActivityIndicator, FlatList } from "react-native";

export default function NotificationList() {
  const { data, isLoading, refetch } = useGetNotification();
  const { primary } = colors;

  useEffect(() => {
    console.log("Data", stringify(data));
  }, [data]);

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

  return (
    <Pressable
      onPress={handlePress}
      style={isOddItem && styles.oddWrapper}
      className={`flex-row gap-2 p-2`}
    >
      <View>
        <Avatar
          size={80}
          rounded
          source={
            imageUrl ? { uri: imageUrl } : require("../../../assets/images/avatar.png")
          }
          containerStyle={imageUrl && { backgroundColor: "#FFC529" }}
        />
      </View>
      <View className='shrink justify-center pt-2'>
        <Text>{message}</Text>
      </View>
    </Pressable>
  );
};
