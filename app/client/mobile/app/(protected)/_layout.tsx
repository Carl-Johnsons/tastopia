import { useEffect, useState } from "react";
import {
  View,
  StyleSheet,
  Keyboard,
  ActivityIndicator,
  Platform,
} from "react-native";
import { Redirect, Tabs, usePathname, useRootNavigationState } from "expo-router";
import { menuList } from "@/constants/menu";
import { useTranslation } from "react-i18next";
import { selectRole } from "@/slices/auth.slice";
import { COMMUNITY_PATH, MAIN_PATH } from "@/constants/paths";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";

const isAndroid = Platform.OS === "android";

const ProtectedLayout = () => {
  const { t } = useTranslation("menu");
  const { c } = useColorizer();
  const { black, white, primary } = colors;
  const pathname = usePathname();

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
      justifyContent: "flex-start",
      alignItems: "flex-start",
      borderTopWidth: 1,
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
      width: 80,
      padding: 0,
      justifyContent: "center"
    }
  });

  return (
    <>
      <Tabs
        screenOptions={{
          tabBarShowLabel: false,
          tabBarActiveTintColor: primary,
          tabBarInactiveTintColor: c(black.DEFAULT, white.DEFAULT),
          tabBarStyle: [styles.tabBar, { bottom: isKeyBoardVisible ? -50 : 0 }],
          tabBarHideOnKeyboard: false
        }}
      >
        {sortedMenuItems.map(menuItem => {
          const { code, translateCode, path, icon, hidden, includeInMainTab } = menuItem;
          // const isNotAllowed = !accountPermissionGroups?.includes(code);
          //! Update authorization later
          const isNotAllowed = false;
          return (
            <Tabs.Screen
              key={path}
              name={path}
              options={{
                ...(((isNotAllowed && code !== "OPTION") ||
                  hidden ||
                  !includeInMainTab) && { href: null }),
                ...(translateCode && { title: t(translateCode) }),
                headerShown: false,
                tabBarIcon: ({ size, focused }) => (
                  <View style={styles.tabItem}>
                    {typeof icon === "function"
                      ? icon({
                          focused:
                            focused ||
                            (path === MAIN_PATH && pathname.includes(COMMUNITY_PATH)),
                          color: focused ? primary : c(black.DEFAULT, white.DEFAULT),
                          size
                        })
                      : icon}
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
