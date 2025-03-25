import { useCallback } from "react";
import { HubConnection } from "@microsoft/signalr";
import { SignalREvent } from "@/constants/signalr";

const useReceiveOnlineUserNumberSubscription = () => {
  const subscribeReceiveOnlineUserNumberEvent = useCallback(
    (connection?: HubConnection) => {
      if (!connection) {
        return;
      }
      connection.on(SignalREvent.RECEIVE_ONLINE_USER_NUMBER, (number: number) => {
        console.log("SignalR: Receive online conntection:", connection);
        if (!connection) {
          return;
        }
        console.log("SignalR: Receive online user:", number);
      });
    },
    []
  );

  const unsubscribeReceiveOnlineUserNumberEvent = useCallback(
    (connection?: HubConnection) => {
      if (!connection) {
        return;
      }
      connection.off(SignalREvent.RECEIVE_ONLINE_USER_NUMBER);
    },
    []
  );

  return {
    subscribeReceiveOnlineUserNumberEvent,
    unsubscribeReceiveOnlineUserNumberEvent
  };
};

export { useReceiveOnlineUserNumberSubscription };
