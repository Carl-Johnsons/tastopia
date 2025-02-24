import { router } from "expo-router";
import { useTranslation } from "react-i18next";
import { CameraIconSvg } from "@/components/common/SVG";
import { ReactElement, ReactNode } from "react";
import { View, Text, TouchableHighlight } from "react-native";
import { Feather, Ionicons, Octicons } from "@expo/vector-icons";
import {
  CAPTURE_PATH,
  COMMUNITY_PATH,
  MAIN_PATH,
  MENU_PATH,
  NOTIFICATION_PATH,
  SEARCH_PATH,
  USER_PATH
} from "@/constants/paths";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "./colors";
import Protected from "@/components/Protected";
import { ROLE } from "@/slices/auth.slice";

type TabIconType = {
  icon: ReactElement | undefined;
  color: string;
  translateCode: string;
  focused: boolean;
  hidden?: boolean;
  hideTitle?: boolean;
};

const TabIcon = ({ icon, translateCode, focused, hidden, hideTitle }: TabIconType) => {
  const { t } = useTranslation("navbar");
  const { c } = useColorizer();
  const { black, white, primary } = colors;
  return (
    <View
      style={{
        display: "flex",
        flexDirection: hidden ? "row" : "column",
        alignItems: "center"
      }}
    >
      <View>{icon}</View>
      {!hideTitle && (
        <Text
          style={{
            color: focused ? primary : c(black.DEFAULT, white.DEFAULT),
            textAlign: "center",
            fontSize: 12,
            marginTop: 2
          }}
        >
          {t(translateCode)}
        </Text>
      )}
    </View>
  );
};

const MainTabIcon = ({ icon, color, translateCode, focused }: TabIconType) => {
  const { c } = useColorizer();
  const { black, white, primary } = colors;

  return (
    <TouchableHighlight
      underlayColor={"none"}
      // underlayColor={globalStyles.color.primary}
      // activeOpacity={0.6}
      onPress={() => router.navigate("/capture")}
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        // backgroundColor: globalStyles.color.dark,
        borderRadius: "100%",
        width: 70,
        height: 60,
        padding: 1,
        shadowColor: "#000",
        elevation: 0
      }}
    >
      <CameraIconSvg
        width={40}
        height={40}
        fill={focused ? primary : c(black.DEFAULT, white.DEFAULT)}
      />
    </TouchableHighlight>
  );
};

type MenuItem = {
  icon?:
    | ((props: { focused: boolean; color: string; size: number }) => ReactNode)
    | undefined;
  path: string;
  code: string;
  translateCode?: string;
  hidden?: boolean;
  includeInModal?: { position: number };
  includeInMainTab?: { position: number };
};

export type Menu = {
  menuTitle: string;
  menuDescription: string;
  menuItems: MenuItem[];
};

export const menuList: Menu[] = [
  {
    menuTitle: "menuTittleTranslate",
    menuDescription: "menuDescriptionTranslate",
    menuItems: [
      /* ================= MAIN TABS ================= */
      {
        path: MAIN_PATH,
        code: "COMMUNITY"
      },
      {
        path: COMMUNITY_PATH,
        icon: ({ color, focused }) => (
          <TabIcon
            icon={
              <Octicons
                name='people'
                size={24}
                color={color}
              />
            }
            color={color}
            translateCode='community'
            focused={focused}
          />
        ),
        code: "COMMUNITY",
        translateCode: "community",
        includeInMainTab: {
          position: 1
        }
      },
      {
        path: SEARCH_PATH,
        icon: ({ color, focused }) => (
          <Protected
            excludedRoles={[ROLE.GUEST]}
            forceDisplay
            requiredLogin
          >
            <TabIcon
              icon={
                <Feather
                  name='search'
                  size={24}
                  color={color}
                />
              }
              color={color}
              translateCode='search'
              focused={focused}
            />
          </Protected>
        ),
        code: "SEARCH",
        translateCode: "search",
        includeInMainTab: {
          position: 2
        }
      },
      {
        path: CAPTURE_PATH,
        icon: ({ color, focused }) => (
          <Protected
            excludedRoles={[ROLE.GUEST]}
            forceDisplay
            requiredLogin
          >
            <MainTabIcon
              icon={
                <Ionicons
                  name='scan-circle-outline'
                  size={40}
                  color={color}
                />
              }
              color={color}
              translateCode='dashboard'
              focused={focused}
            />
          </Protected>
        ),
        code: "CAPTURE",
        translateCode: "capture",
        includeInMainTab: {
          position: 3
        }
      },
      {
        path: NOTIFICATION_PATH,
        icon: ({ color, focused }) => (
          <Protected
            excludedRoles={[ROLE.GUEST]}
            forceDisplay
            requiredLogin
          >
            <TabIcon
              icon={
                <Feather
                  name='bell'
                  size={24}
                  color={color}
                />
              }
              color={color}
              translateCode='notification'
              focused={focused}
            />
          </Protected>
        ),
        code: "NOTIFICATION",
        translateCode: "notification",
        includeInMainTab: {
          position: 4
        }
      },
      {
        path: MENU_PATH,
        icon: ({ color, focused }) => (
          <TabIcon
            icon={
              <Feather
                name='menu'
                size={24}
                color={color}
              />
            }
            color={color}
            translateCode='menu'
            focused={focused}
          />
        ),
        code: "MENU",
        translateCode: "menu",
        includeInMainTab: {
          position: 5
        }
      },
      {
        path: USER_PATH,
        icon: ({ color, focused }) => (
          <TabIcon
            icon={
              <Feather
                name='menu'
                size={24}
                color={color}
              />
            }
            color={color}
            translateCode='user'
            focused={focused}
          />
        ),
        code: "USER",
        translateCode: "user",
        hidden: true
      }
    ]
  }
];
