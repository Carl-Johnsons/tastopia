import * as Device from "expo-device";
import * as Notifications from "expo-notifications";
import Constants from "expo-constants";
import { Alert, Platform } from "react-native";
import { useEffect, useRef, useState } from "react";

export interface PushNotificationState {
  notification?: Notifications.Notification;
  expoPushToken?: Notifications.ExpoPushToken;
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
    if (Device.isDevice) {
      const { status: existingStatus } = await Notifications.getPermissionsAsync();
      let finalStatus = existingStatus;
      // Request permission again if the status is not granted
      if (existingStatus !== Notifications.PermissionStatus.GRANTED) {
        const { status } = await Notifications.getPermissionsAsync();
        finalStatus = existingStatus;
      }

      if (finalStatus !== Notifications.PermissionStatus.GRANTED) {
        Alert.alert("In order to have notification, you have to allowed this permission");
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
      }
      return token;
    } else {
      console.error("Error: Please use physical device");
    }
  };
  useEffect(() => {
    registerForPushNotificationAsync().then(token => {
      setExpoPushToken(token);
    });

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
    notification
  };
};
