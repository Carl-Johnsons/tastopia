import { useCallback } from "react";
import { HubConnection } from "@microsoft/signalr";
import {
  useConnectedSubscription,
  useReceiveNotificationSubscription,
  useReceiveOnlineUserNumberSubscription
} from ".";

const useSubscribeSignalREvents = () => {
  const { subscribeConnectedEvent, unsubscribeConnectedEvent } =
    useConnectedSubscription();

  const { subscribeReceiveNotificationEvent, unsubscribeReceiveNotificationEvent } =
    useReceiveNotificationSubscription();

  const {
    subscribeReceiveOnlineUserNumberEvent,
    unsubscribeReceiveOnlineUserNumberEvent
  } = useReceiveOnlineUserNumberSubscription();

  const subscribeAllEvents = useCallback((connection: HubConnection) => {
    subscribeConnectedEvent(connection);
    subscribeReceiveNotificationEvent(connection);
    subscribeReceiveOnlineUserNumberEvent(connection);
  }, []);

  const unsubscribeAllEvents = useCallback((connection: HubConnection) => {
    unsubscribeConnectedEvent(connection);
    unsubscribeReceiveNotificationEvent(connection);
    unsubscribeReceiveOnlineUserNumberEvent(connection);
  }, []);

  return {
    subscribeAllEvents,
    unsubscribeAllEvents
  };
};
export { useSubscribeSignalREvents };
