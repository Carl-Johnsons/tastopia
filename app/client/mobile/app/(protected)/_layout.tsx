import { Badge } from "@rneui/themed";
import { colors } from "@/constants/colors";
import { COMMUNITY_PATH, MAIN_PATH } from "@/constants/paths";
import { menuList } from "@/constants/menu";
import { Redirect, Tabs, usePathname, useRootNavigationState } from "expo-router";
import { selectRole } from "@/slices/auth.slice";
import { useEffect, useState } from "react";
import { useGetNotification } from "@/api/notification";
import { useQueryClient } from "react-query";
import { useTranslation } from "react-i18next";
import {
  View,
  StyleSheet,
  Keyboard,
  ActivityIndicator,
  Platform,
  Pressable
} from "react-native";
import useColorizer from "@/hooks/useColorizer";
import { NotificationCategories } from "@/constants/notifications";

const isAndroid = Platform.OS === "android";

const ProtectedLayout = () => {
  const { data } = useGetNotification(NotificationCategories.ALL);

  const queryClient = useQueryClient();
  const { t } = useTranslation("menu");
  const { c } = useColorizer();
  const { black, white, primary } = colors;
  const pathname = usePathname();
  const unreadNotification = data?.pages[0].metadata?.unreadNotifications;
  const formatUnreadNotification = unreadNotification
    ? unreadNotification >= 10
      ? "9+"
      : unreadNotification
    : undefined;

  const [isKeyBoardVisible, setIsKeyBoardVisible] = useState(false);
  const role = selectRole();
  const navigationState = useRootNavigationState();

  // Handle bottom tabs bar visibility when keyboard is shown
  const sortedMenuItems = menuList?.[0].menuItems.sort(
    (cur, next) =>
      (cur.includeInMainTab?.position ?? -99) - (next.includeInMainTab?.position ?? -99)
  );

  useEffect(() => {
    const KeyboardDidShow = Keyboard.addListener("keyboardDidShow", () => {
      setIsKeyBoardVisible(true);
    });

    const KeyboardDidHide = Keyboard.addListener("keyboardDidHide", () => {
      setIsKeyBoardVisible(false);
    });

    queryClient.invalidateQueries({ queryKey: "getNotification" });

    return () => {
      KeyboardDidShow.remove();
      KeyboardDidHide.remove();
    };
  }, []);

  if (!role) {
    return !navigationState?.key ? (
      <View className='h-full justify-center'>
        <ActivityIndicator
          size={"large"}
          color={"black"}
        />
      </View>
    ) : (
      <Redirect href={"/welcome"} />
    );
  }

  const styles = StyleSheet.create({
    tabBar: {
      display: "flex",
      flexDirection: "row",
      justifyContent: "space-around",
      alignItems: "center",
      backgroundColor: c(white.DEFAULT, black[100]),
      height: isAndroid ? 64 : 80,
      paddingTop: 10,
      paddingBottom: 10,
      paddingHorizontal: 10,
      zIndex: 2,
      overflow: "hidden",
      shadowColor: "#000",
      shadowOffset: { width: 0, height: -2 },
      shadowOpacity: 0.1,
      shadowRadius: 2,
      elevation: 10
    },

    tabItem: {
      flex: 1,
      padding: 0,
      justifyContent: "center",
      alignItems: "center",
      height: "100%"
    }
  });

  const isHiddenPath = (
    isNotAllowed: boolean,
    code: string,
    hidden: boolean | undefined,
    includeInMainTab: Object | undefined
  ) => {
    return (isNotAllowed && code !== "OPTION") || hidden || !includeInMainTab;
  };

  return (
    <>
      <Tabs
        screenOptions={{
          tabBarShowLabel: false,
          tabBarActiveTintColor: primary,
          tabBarInactiveTintColor: c(black.DEFAULT, white.DEFAULT),
          tabBarStyle: [styles.tabBar],
          tabBarHideOnKeyboard: false
        }}
      >
        {sortedMenuItems.map(menuItem => {
          const { code, translateCode, path, icon, hidden, includeInMainTab, hasBadge } =
            menuItem;
          // const isNotAllowed = !accountPermissionGroups?.includes(code);
          //! Update authorization later
          const isNotAllowed = false;
          return (
            <Tabs.Screen
              key={path}
              name={path}
              options={{
                ...(isHiddenPath(isNotAllowed, code, hidden, includeInMainTab) && {
                  href: null
                }),
                ...(translateCode && { title: t(translateCode) }),
                headerShown: false,
                ...(!isHiddenPath(isNotAllowed, code, hidden, includeInMainTab) && {
                  tabBarButton: props => (
                    <Pressable
                      {...props}
                      android_ripple={null}
                      style={[
                        props.style,
                        {
                          flex: 1,
                          justifyContent: "center",
                          alignItems: "center",
                          height: "100%"
                        }
                      ]}
                    />
                  )
                }),
                tabBarIcon: ({ size, focused }) => (
                  <View
                    style={styles.tabItem}
                    testID={code}
                  >
                    {typeof icon === "function"
                      ? icon({
                          focused:
                            focused ||
                            (path === MAIN_PATH && pathname.includes(COMMUNITY_PATH)),
                          color: focused ? primary : c(black.DEFAULT, white.DEFAULT),
                          size
                        })
                      : icon}
                    {hasBadge && formatUnreadNotification && (
                      <Badge
                        value={formatUnreadNotification}
                        status='error'
                        containerStyle={{ position: "absolute", top: -10, left: 44 }}
                      />
                    )}
                  </View>
                )
              }}
            />
          );
        })}
      </Tabs>
    </>
  );
};

export default ProtectedLayout;
