import { API_GATEWAY_HOST } from "@/constants/host";
import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { selectAccessToken } from "@/slices/auth.slice";
import { selectUserId } from "@/slices/user.slice";
import { createContext, useCallback, useEffect, useRef, useState } from "react";
import { useSubscribeSignalREvents } from "@/hooks/signalR/useSubscribeSignalREvents";
import { transformPlatformURI } from "@/utils/functions";

interface SignalRHubContextType {
  connection: HubConnection | null;
  startConnection: () => Promise<void>;
  stopConnection: () => Promise<void>;
  connected: boolean;
}

interface Props {
  children: JSX.Element;
}

const SignalRHubContext = createContext<SignalRHubContextType | null>(null);

const SignalRHubProvider = ({ children }: Props) => {
  const hubUrl = transformPlatformURI(`http://${API_GATEWAY_HOST}:5004/tastopia-hub`);
  const [waitingToReconnect, setWaitingToReconnect] = useState(true);
  const currentUserId = selectUserId();
  const accessToken = selectAccessToken();

  const { subscribeAllEvents, unsubscribeAllEvents } = useSubscribeSignalREvents();

  const connectionRef = useRef<HubConnection | null>(null);

  const startConnection = useCallback(async () => {
    if (!connectionRef.current) {
      return;
    }
    console.log("starting signalr");
    connectionRef.current
      .start()
      .then(() => {
        connectionRef.current && subscribeAllEvents(connectionRef.current);
        setWaitingToReconnect(false);
        console.log("signalR connected");
      })
      .catch(err => console.log(err));
  }, [subscribeAllEvents]);

  const stopConnection = useCallback(async () => {
    if (!connectionRef.current) {
      return;
    }
    console.log("stop signalr");
    await connectionRef.current.stop();
    unsubscribeAllEvents(connectionRef.current);
  }, [unsubscribeAllEvents]);

  useEffect(() => {
    if (connectionRef.current || !currentUserId) {
      return;
    }

    connectionRef.current = new HubConnectionBuilder()
      .withUrl(`${hubUrl}?userId=${currentUserId}`, {
        accessTokenFactory: () => accessToken ?? ""
      })
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();

    startConnection();
  }, [
    accessToken,
    currentUserId,
    hubUrl,
    startConnection,
    subscribeAllEvents,
    waitingToReconnect
  ]);

  return (
    <SignalRHubContext.Provider
      value={{
        connection: connectionRef.current,
        startConnection,
        stopConnection,
        connected: !waitingToReconnect
      }}
    >
      {children}
    </SignalRHubContext.Provider>
  );
};

export { SignalRHubProvider, SignalRHubContext };
