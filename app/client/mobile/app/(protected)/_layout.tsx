import React from "react";
import { ActivityIndicator, Image, ImageSourcePropType, Text, View } from "react-native";
import { Tabs, Redirect, useRootNavigationState, router } from "expo-router";
import AntDesign from "@expo/vector-icons/AntDesign";

import { icons } from "../../constants";
import { selectJwtToken } from "@/slices/auth.slice";
import Button from "@/components/Button";
import { useOsValue } from "@/hooks/alternator";

type TabIconType = {
  icon: ImageSourcePropType | undefined;
  color: string;
  name: string;
  focused: boolean;
};

const TabIcon = ({ icon, color, name, focused }: TabIconType) => {
  return (
    <View className='items-center justify-center gap-2'>
      <Image
        source={icon}
        resizeMode='contain'
        tintColor={color}
        className='size-6'
      />
      <Text
        className={`${focused ? "font-psemibold" : "font-pregular"} text-xs`}
        style={{ color: color }}
      >
        {name}
      </Text>
    </View>
  );
};

const TabsLayout = () => {
  const jwtToken = selectJwtToken();
  const navigationState = useRootNavigationState();

  if (!jwtToken) {
    return !navigationState?.key ? (
      <View className='h-full justify-center'>
        <ActivityIndicator
          size={"large"}
          color={"black"}
        />
      </View>
    ) : (
      <Redirect href={"/sign-in"} />
    );
  }

  return (
    <>
      <Tabs
        screenOptions={{
          tabBarShowLabel: false,
          headerShown: false,
          tabBarActiveTintColor: "#FFA001",
          tabBarInactiveTintColor: "#CDCDE0",
          tabBarStyle: {
            backgroundColor: "#161622",
            borderTopWidth: 1,
            borderTopColor: "#232553",
            height: 84
          }
        }}
      >
        <Tabs.Screen
          name='index'
          options={{
            title: "Home",
            tabBarIcon: ({ color, focused }) => (
              <TabIcon
                icon={icons.home}
                color={color}
                name='home'
                focused={focused}
              />
            ),
            tabBarIconStyle: {
              marginRight: "20%"
            }
          }}
        />
        <Tabs.Screen
          name='search'
          options={{
            title: "Search",
            tabBarIcon: ({ color, focused }) => (
              <TabIcon
                icon={icons.search}
                color={color}
                name='search'
                focused={focused}
              />
            ),
            tabBarIconStyle: {
              marginRight: "67%"
            }
          }}
        />
        <Tabs.Screen
          name='profile'
          options={{
            title: "Profile",
            tabBarIcon: ({ color, focused }) => (
              <TabIcon
                icon={icons.profile}
                color={color}
                name='profile'
                focused={focused}
              />
            ),
            tabBarIconStyle: {
              marginLeft: "20%"
            }
          }}
        />
      </Tabs>
      <View
        className={`absolute ${useOsValue("bottom-7", "bottom-5")} right-[30%] rounded-3xl bg-white/10 p-4 blur-xl`}
      >
        <Button
          onPress={() => router.navigate("/createPost")}
          className='size-full'
        >
          <AntDesign
            name='plus'
            size={24}
            color='white'
          />
        </Button>
      </View>
    </>
  );
};

export default TabsLayout;
