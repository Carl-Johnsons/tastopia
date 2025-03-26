import { useCallback } from "react";
import { HubConnection } from "@microsoft/signalr";
import { SignalREvent } from "@/constants/signalr";

const useReceiveTotalUserNumberSubscription = () => {
  const subscribeReceiveTotalUserNumberEvent = useCallback(
    (connection?: HubConnection) => {
      if (!connection) {
        return;
      }
      connection.on(SignalREvent.RECEIVE_TOTAL_USER_NUMBER, (number: number) => {
        if (!connection) {
          return;
        }
        console.log("SignalR: Receive total User:", number);
      });
    },
    []
  );

  const unsubscribeReceiveTotalUserNumberEvent = useCallback(
    (connection?: HubConnection) => {
      if (!connection) {
        return;
      }
      connection.off(SignalREvent.RECEIVE_TOTAL_USER_NUMBER);
    },
    []
  );

  return {
    subscribeReceiveTotalUserNumberEvent,
    unsubscribeReceiveTotalUserNumberEvent
  };
};

export { useReceiveTotalUserNumberSubscription };
