import { useCallback } from "react";
import { HubConnection } from "@microsoft/signalr";
import {
  useConnectedSubscription,
  useReceiveNotificationSubscription,
  useReceiveOnlineUserNumberSubscription,
  useReceiveTotalRecipeNumberSubscription,
  useReceiveTotalUserNumberSubscription
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

  const { subscribeReceiveTotalUserNumberEvent, unsubscribeReceiveTotalUserNumberEvent } =
    useReceiveTotalUserNumberSubscription();

  const {
    subscribeReceiveTotalRecipeNumberEvent,
    unsubscribeReceiveTotalRecipeNumberEvent
  } = useReceiveTotalRecipeNumberSubscription();

  const subscribeAllEvents = useCallback((connection: HubConnection) => {
    subscribeConnectedEvent(connection);
    subscribeReceiveNotificationEvent(connection);
    subscribeReceiveOnlineUserNumberEvent(connection);
    subscribeReceiveTotalUserNumberEvent(connection);
    subscribeReceiveTotalRecipeNumberEvent(connection);
  }, []);

  const unsubscribeAllEvents = useCallback((connection: HubConnection) => {
    unsubscribeConnectedEvent(connection);
    unsubscribeReceiveNotificationEvent(connection);
    unsubscribeReceiveOnlineUserNumberEvent(connection);
    unsubscribeReceiveTotalUserNumberEvent(connection);
    unsubscribeReceiveTotalRecipeNumberEvent(connection);
  }, []);

  return {
    subscribeAllEvents,
    unsubscribeAllEvents
  };
};
export { useSubscribeSignalREvents };
