import { useCallback } from "react";
import { HubConnection } from "@microsoft/signalr";
import { SignalREvent } from "@/constants/signalr";

const useConnectedSubscription = () => {
  const subscribeConnectedEvent = useCallback((connection?: HubConnection) => {
    if (!connection) {
      return;
    }
    connection.on(SignalREvent.CONNECTED, () => {
      if (!connection) {
        return;
      }
      console.log("Connected to signalR");
    });
  }, []);

  const unsubscribeConnectedEvent = useCallback((connection?: HubConnection) => {
    if (!connection) {
      return;
    }
    connection.off(SignalREvent.CONNECTED);
  }, []);

  return { subscribeConnectedEvent, unsubscribeConnectedEvent };
};

export { useConnectedSubscription };
