import { useCallback } from "react";
import { HubConnection } from "@microsoft/signalr";
import { SignalREvent } from "@/constants/signalr";

const useReceiveTotalRecipeNumberSubscription = () => {
  const subscribeReceiveTotalRecipeNumberEvent = useCallback(
    (connection?: HubConnection) => {
      if (!connection) {
        return;
      }
      connection.on(SignalREvent.RECEIVE_TOTAL_RECIPE_NUMBER, (number: number) => {
        if (!connection) {
          return;
        }
        console.log("SignalR: Receive total recipe:", number);
      });
    },
    []
  );

  const unsubscribeReceiveTotalRecipeNumberEvent = useCallback(
    (connection?: HubConnection) => {
      if (!connection) {
        return;
      }
      connection.off(SignalREvent.RECEIVE_TOTAL_RECIPE_NUMBER);
    },
    []
  );

  return {
    subscribeReceiveTotalRecipeNumberEvent,
    unsubscribeReceiveTotalRecipeNumberEvent
  };
};

export { useReceiveTotalRecipeNumberSubscription };
