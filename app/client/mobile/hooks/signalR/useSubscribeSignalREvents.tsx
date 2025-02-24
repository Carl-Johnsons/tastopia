import { useCallback } from "react";
import { HubConnection } from "@microsoft/signalr";
import { useConnectedSubscription, useReceiveNotificationSubscription } from ".";

const useSubscribeSignalREvents = () => {
  const { subscribeConnectedEvent, unsubscribeConnectedEvent } =
    useConnectedSubscription();

  const { subscribeReceiveNotificationEvent, unsubscribeReceiveNotificationEvent } =
    useReceiveNotificationSubscription();

  const subscribeAllEvents = useCallback((connection: HubConnection) => {
    subscribeConnectedEvent(connection);
    subscribeReceiveNotificationEvent(connection);
  }, []);

  const unsubscribeAllEvents = useCallback((connection: HubConnection) => {
    unsubscribeConnectedEvent(connection);
    unsubscribeReceiveNotificationEvent(connection);
  }, []);

  return {
    subscribeAllEvents,
    unsubscribeAllEvents
  };
};
export { useSubscribeSignalREvents };
