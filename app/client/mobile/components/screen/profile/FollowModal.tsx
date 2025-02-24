import { useGetUserFollowers, useGetUserFollowings } from "@/api/user";
import CustomTab, { ItemProps } from "@/components/common/Tab";
import User from "@/components/common/User";
import { colors } from "@/constants/colors";
import { ArrowBackIcon } from "@/constants/icons";
import useColorizer from "@/hooks/useColorizer";
import useDebounce from "@/hooks/useDebounce";
import useHydrateData from "@/hooks/useHydrateData";
import {
  BottomSheetBackdrop,
  BottomSheetModal,
  BottomSheetView,
  useBottomSheetModal
} from "@gorhom/bottom-sheet";
import { TabView } from "@rneui/themed";
import { useFocusEffect } from "expo-router";
import {
  ReactElement,
  forwardRef,
  useCallback,
  useMemo,
  useState
} from "react";
import { useTranslation } from "react-i18next";
import {
  ActivityIndicator,
  FlatList,
  ListRenderItemInfo,
  RefreshControl,
  StyleSheet,
  Text,
  View
} from "react-native";
import { useQueryClient } from "react-query";
import Empty from "../community/Empty";
import { SearchBar } from "../history/Content";

export const FollowModal = forwardRef<BottomSheetModal, {}>((_props, ref) => {
  const { c } = useColorizer();
  const { black, white } = colors;

  const styles = StyleSheet.create({
    handleIndicatorStyle: {
      backgroundColor: c(black.DEFAULT, white.DEFAULT),
      display: "none"
    },
    backgroundStyle: {
      backgroundColor: c(white.DEFAULT, black[200])
    }
  });

  return (
    <BottomSheetModal
      ref={ref}
      handleIndicatorStyle={styles.handleIndicatorStyle}
      backgroundStyle={styles.backgroundStyle}
      enableContentPanningGesture={false}
      backdropComponent={props => (
        <BottomSheetBackdrop
          {...props}
          disappearsOnIndex={-1}
          appearsOnIndex={0}
        />
      )}
    >
      <BottomSheetView>
        <Header />
        <Body />
      </BottomSheetView>
    </BottomSheetModal>
  );
});

const Header = (className: { className?: string }) => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const { t } = useTranslation("follow");
  const { dismiss } = useBottomSheetModal();

  return (
    <View className={`flex-row justify-between px-4 py-4 pt-2 ${className.className}`}>
      <ArrowBackIcon
        color={c(black.DEFAULT, white.DEFAULT)}
        width={28}
        height={28}
        onPress={() => dismiss()}
      />
      <Text className='text-black_white font-medium text-2xl'>{t("title")}</Text>
      <View className='w-[28px]' />
    </View>
  );
};

const Body = ({ className }: { className?: string }) => {
  const { t } = useTranslation("follow");
  const styles = StyleSheet.create({
    title: {}
  });

  const tabItems: ItemProps[] = useMemo(
    () => [
      { title: t("follower.title"), titleStyle: styles.title },
      { title: t("following.title"), titleStyle: styles.title }
    ],
    [styles.title]
  );

  const tabViews: ReactElement[] = useMemo(
    () => [<FollowersTab key='FollowersTab' />, <FollowingTab key='FollowingTab' />],
    [styles.title]
  );

  return (
    <View className={`h-[90vh] ${className}`}>
      <CustomTab
        tabItems={tabItems}
        tabViews={tabViews}
      />
    </View>
  );
};

const FollowersTab = () => {
  const [keyword, setKeyword] = useState<string>("");
  const [followers, setFollowers] = useState<SearchUserResultType[]>();

  const { t } = useTranslation("follow", { keyPrefix: "follower" });
  const debouncedKeyword = useDebounce(keyword, 200);
  const { data, isLoading, refetch, isStale, fetchNextPage } =
    useGetUserFollowers(debouncedKeyword);
  const { primary } = colors;

  useHydrateData({ source: data, setter: setFollowers });
  const queryClient = useQueryClient();

  const fetchData = useCallback(() => {
    if (isStale) {
      refetch();
    }
  }, [isStale]);

  useFocusEffect(fetchData);

  const invalidateSearch = useCallback(() => {
    queryClient.invalidateQueries({ queryKey: ["getUserFollowers", debouncedKeyword] });
    queryClient.invalidateQueries({ queryKey: ["getUserFollowers", ""] });
  }, [queryClient, debouncedKeyword]);

  const renderItem = useCallback(
    ({ item }: ListRenderItemInfo<SearchUserResultType>) => {
      if (!followers) return null;

      return (
        <View
          className='p-4'
          key={item.id + item.username}
        >
          <User
            {...item}
            invalidateSearch={invalidateSearch}
          />
        </View>
      );
    },
    [followers?.length, invalidateSearch]
  );

  return (
    <TabView.Item className='w-full'>
      <View className='px-4 pt-4'>
        <SearchBar
          searchValue={keyword}
          setSearchValue={setKeyword}
          isSearching={isLoading}
          placeholder={t("searchPlaceHolder")}
        />

        {isLoading || !followers ? (
          <View className='flex-center h-[50%]'>
            <ActivityIndicator
              size='large'
              color={primary}
            />
          </View>
        ) : (
          <FlatList
            data={followers}
            className='h-full'
            onEndReached={() => fetchNextPage()}
            onEndReachedThreshold={0.1}
            ItemSeparatorComponent={() => (
              <View className='my-4 h-[1px] w-full bg-gray-300/50' />
            )}
            showsHorizontalScrollIndicator={false}
            ListEmptyComponent={() => (
              <View className='h-[100vh]'>
                <Empty
                  type={!!debouncedKeyword ? "emptyFollowerSearch" : "emptyFollower"}
                />
              </View>
            )}
            renderItem={renderItem}
            refreshControl={
              <RefreshControl
                refreshing={isLoading}
                tintColor={primary}
                onRefresh={refetch}
              />
            }
          />
        )}
      </View>
    </TabView.Item>
  );
};

const FollowingTab = () => {
  const [keyword, setKeyword] = useState<string>("");
  const [followings, setFollowings] = useState<SearchUserResultType[]>();

  const {t} = useTranslation("follow", { keyPrefix: "following" });
  const debouncedKeyword = useDebounce(keyword, 200);
  const { data, isLoading, refetch, isStale, fetchNextPage } =
    useGetUserFollowings(debouncedKeyword);
  const { primary } = colors;

  useHydrateData({ source: data, setter: setFollowings });
  const queryClient = useQueryClient();

  const fetchData = useCallback(() => {
    if (isStale) {
      refetch();
    }
  }, [isStale]);

  useFocusEffect(fetchData);

  const invalidateSearch = useCallback(() => {
    queryClient.invalidateQueries({ queryKey: ["getUserFollowings", debouncedKeyword] });
    queryClient.invalidateQueries({ queryKey: ["getUserFollowings", ""] });
  }, [queryClient, debouncedKeyword]);

  const renderItem = useCallback(
    ({ item }: ListRenderItemInfo<SearchUserResultType>) => {
      if (!followings) return null;

      return (
        <View
          className='p-4'
          key={item.id + item.username}
        >
          <User
            {...item}
            invalidateSearch={invalidateSearch}
          />
        </View>
      );
    },
    [followings?.length, invalidateSearch]
  );

  return (
    <TabView.Item className='w-full'>
      <View className='px-4 pt-4'>
        <SearchBar
          searchValue={keyword}
          setSearchValue={setKeyword}
          isSearching={isLoading}
          placeholder={t("searchPlaceHolder")}
        />

        {isLoading || !followings ? (
          <View className='flex-center h-[50%]'>
            <ActivityIndicator
              size='large'
              color={primary}
            />
          </View>
        ) : (
          <FlatList
            data={followings}
            className='h-full'
            onEndReached={() => fetchNextPage()}
            onEndReachedThreshold={0.1}
            ItemSeparatorComponent={() => (
              <View className='my-4 h-[1px] w-full bg-gray-300/50' />
            )}
            showsHorizontalScrollIndicator={false}
            ListEmptyComponent={() => (
              <View className='h-[100vh]'>
                <Empty
                  type={!!debouncedKeyword ? "emptyFollowingSearch" : "emptyFollowing"}
                />
              </View>
            )}
            renderItem={renderItem}
            refreshControl={
              <RefreshControl
                refreshing={isLoading}
                tintColor={primary}
                onRefresh={refetch}
              />
            }
          />
        )}
      </View>
    </TabView.Item>
  );
};

export default FollowModal;
