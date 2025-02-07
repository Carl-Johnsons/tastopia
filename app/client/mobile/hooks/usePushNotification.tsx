import {
  AndroidImportance,
  EventSubscription,
  ExpoPushToken,
  Notification,
  NotificationResponse,
  PermissionStatus,
  addNotificationReceivedListener,
  addNotificationResponseReceivedListener,
  getExpoPushTokenAsync,
  getLastNotificationResponseAsync,
  getPermissionsAsync,
  removeNotificationSubscription,
  requestPermissionsAsync,
  setNotificationChannelAsync,
  setNotificationHandler
} from "expo-notifications";
import Constants from "expo-constants";
import { Alert, Platform } from "react-native";
import { useEffect, useRef, useState } from "react";
import { protectedAxiosInstance } from "@/constants/host";
import { useRouter } from "expo-router";

export interface PushNotificationState {
  notification?: Notification;
  expoPushToken?: ExpoPushToken;
  registerForPushNotificationAsync: () => Promise<void>;
}

export const usePushNotification = (): PushNotificationState => {
  const router = useRouter();

  const [expoPushToken, setExpoPushToken] = useState<ExpoPushToken | undefined>();
  const [notification, setNotification] = useState<Notification | undefined>();

  const notificationListener = useRef<EventSubscription>();
  const responseListener = useRef<EventSubscription>();

  const registerForPushNotificationAsync = async () => {
    setNotificationHandler({
      handleNotification: async () => ({
        shouldPlaySound: true,
        shouldShowAlert: true,
        shouldSetBadge: true
      })
    });

    let token;
    const { status: existingStatus } = await getPermissionsAsync();
    let finalStatus = existingStatus;

    if (existingStatus !== PermissionStatus.GRANTED) {
      const { status } = await requestPermissionsAsync();
      finalStatus = status;
    }

    if (finalStatus !== PermissionStatus.GRANTED) {
      Alert.alert(
        "In order to receive notification, you have to allow the app to send notification."
      );
      return;
    }

    const projectId =
      Constants?.expoConfig?.extra?.eas?.projectId ?? Constants?.easConfig?.projectId;

    token = await getExpoPushTokenAsync({ projectId });

    if (Platform.OS === "android") {
      await setNotificationChannelAsync("default", {
        name: "Regular Notifications",
        importance: AndroidImportance.MAX,
        vibrationPattern: [0, 250, 250, 250],
        lightColor: "#FF231F7C"
      });

      try {
        await protectedAxiosInstance.post("api/notification/expo-push-token/android", {
          expoPushToken: token.data
        });
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
    notificationListener.current = addNotificationReceivedListener(notification => {
      setNotification(notification);
    });

    const handleNotificationResponseReceived = (response: NotificationResponse) => {
      const data = response.notification.request.content.data;
      const { redirectUri, params } = data;

      if (redirectUri) {
        router.push({
          pathname: redirectUri,
          params: params
        });
      }
    };

    getLastNotificationResponseAsync().then(res => {
      if (res) {
        handleNotificationResponseReceived(res);
      }
    });

    responseListener.current = addNotificationResponseReceivedListener(
      handleNotificationResponseReceived
    );

    return () => {
      notificationListener.current &&
        removeNotificationSubscription(notificationListener.current);
      responseListener.current &&
        removeNotificationSubscription(responseListener.current);
    };
  }, []);

  return {
    expoPushToken,
    notification,
    registerForPushNotificationAsync
  };
};
