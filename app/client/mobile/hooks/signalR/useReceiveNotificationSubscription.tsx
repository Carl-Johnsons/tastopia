import { useCallback } from "react";
import { HubConnection } from "@microsoft/signalr";
import { SignalREvent } from "@/constants/signalr";
import { useQueryClient } from "react-query";

const useReceiveNotificationSubscription = () => {
  const queryClient = useQueryClient();
  const subscribeReceiveNotificationEvent = useCallback((connection?: HubConnection) => {
    if (!connection) {
      return;
    }
    connection.on(SignalREvent.RECEIVE_NOTIFICATION, () => {
      if (!connection) {
        return;
      }

      queryClient.invalidateQueries({ queryKey: "getNotification" });
      console.log("Receive notification");
    });
  }, []);

  const unsubscribeReceiveNotificationEvent = useCallback((connection?: HubConnection) => {
    if (!connection) {
      return;
    }
    connection.off(SignalREvent.RECEIVE_NOTIFICATION);
  }, []);

  return { subscribeReceiveNotificationEvent, unsubscribeReceiveNotificationEvent };
};

export { useReceiveNotificationSubscription };
