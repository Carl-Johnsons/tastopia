import * as Notifications from "expo-notifications";
import Constants from "expo-constants";
import { Alert, Platform } from "react-native";
import { useEffect, useRef, useState } from "react";
import { protectedAxiosInstance } from "@/constants/host";

export interface PushNotificationState {
  notification?: Notifications.Notification;
  expoPushToken?: Notifications.ExpoPushToken;
  registerForPushNotificationAsync: () => Promise<void>;
}

export const usePushNotification = (): PushNotificationState => {
  Notifications.setNotificationHandler({
    handleNotification: async () => ({
      shouldPlaySound: false,
      shouldShowAlert: true,
      shouldSetBadge: false
    })
  });

  const [expoPushToken, setExpoPushToken] = useState<
    Notifications.ExpoPushToken | undefined
  >();
  const [notification, setNotification] = useState<
    Notifications.Notification | undefined
  >();

  const notificationListener = useRef<Notifications.EventSubscription>();
  const responseListener = useRef<Notifications.EventSubscription>();

  const registerForPushNotificationAsync = async () => {
    let token;
    const { status: existingStatus } = await Notifications.getPermissionsAsync();
    let finalStatus = existingStatus;

    // Request permission again if the status is not granted
    if (existingStatus !== Notifications.PermissionStatus.GRANTED) {
      const { status } = await Notifications.requestPermissionsAsync();
      finalStatus = status;
    }

    if (finalStatus !== Notifications.PermissionStatus.GRANTED) {
      Alert.alert("In order to have notification, you have to allowed this permission");
      console.log("1");

      return;
    }
    token = await Notifications.getExpoPushTokenAsync({
      projectId: Constants.expoConfig?.extra?.eas?.projectId
    });

    if (Platform.OS === "android") {
      Notifications.setNotificationChannelAsync("default", {
        name: "default",
        importance: Notifications.AndroidImportance.MAX,
        vibrationPattern: [0, 250, 250, 250],
        lightColor: "#FF231F7C"
      });
      try {
        await protectedAxiosInstance.post("api/notification/expo-push-token/android", {
          expoPushToken: token.data
        });
        console.log("Register user with push token successfully");
      } catch (err) {
        console.error(
          `Register user with expo push token ${expoPushToken} failed, Please try again`
        );
        console.error(err);
      }
    }
    setExpoPushToken(token);
  };
  useEffect(() => {
    notificationListener.current = Notifications.addNotificationReceivedListener(
      notification => {
        setNotification(notification);
      }
    );

    responseListener.current = Notifications.addNotificationResponseClearedListener(
      response => {
        console.log(response);
      }
    );
    return () => {
      Notifications.removeNotificationSubscription(notificationListener.current!);
      Notifications.removeNotificationSubscription(responseListener.current!);
    };
  }, []);

  return {
    expoPushToken,
    notification,
    registerForPushNotificationAsync
  };
};
