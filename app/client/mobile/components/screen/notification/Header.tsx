import { colors } from "@/constants/colors";
import { ArrowBackIcon } from "@/constants/icons";
import useColorizer from "@/hooks/useColorizer";
import { router } from "expo-router";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { Pressable, Text, StyleSheet, View } from "react-native";

export default function Header() {
  const { t } = useTranslation("notification");
  const { c } = useColorizer();
  const { black, white } = colors;
  const [notificationType, setNotificationType] = useState<NotificationType>("Community");

  const styles = StyleSheet.create({
    wrapper: {
      height: 62
    },
    title: {
      position: "absolute",
      top: "50%",
      left: "58%",
      transform: [{ translateY: "-50%" }, { translateX: "-50%" }]
    },
    tabWrapper: {
      width: "50%"
    }
  });

  return (
    <View className='bg-white_black'>
      <View
        style={styles.wrapper}
        className='relative flex-row items-center justify-between px-6'
      >
        <ArrowBackIcon
          color={c(black.DEFAULT, white.DEFAULT)}
          width={28}
          height={28}
          onPress={router.back}
        />
        <Text
          style={styles.title}
          className='text-black_white font-medium text-2xl'
        >
          {t("Notification")}
        </Text>
      </View>
      <View className='w-full flex-row'>
        <View style={styles.tabWrapper}>
          <Tab
            label={"Community"}
            value={"Community"}
            isActive={notificationType === "Community"}
          />
        </View>
        <View style={styles.tabWrapper}>
          <Tab
            label={"System"}
            value={"System"}
            isActive={notificationType === "System"}
          />
        </View>
      </View>
    </View>
  );
}

type NotificationType = "Community" | "System";

const Tab = ({
  isActive,
  label
}: {
  isActive: boolean;
  label: string;
  value: NotificationType;
}) => {
  const handlePress = () => {
    console.log("Pressed");
  };

  const styles = StyleSheet.create({
    activeWrapper: {
      borderBottomWidth: 3
    },
    wrapper: {
      height: 52
    }
  });

  return (
    <Pressable
      onPress={handlePress}
      className={`w-full items-center justify-center ${isActive && "border-primary"}`}
      style={[isActive && styles.activeWrapper, styles.wrapper]}
    >
      <Text
        className={`font-medium text-xl text-gray-500 ${isActive && "text-black_white"}`}
      >
        {label}
      </Text>
    </Pressable>
  );
};
