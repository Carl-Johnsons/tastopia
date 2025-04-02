"use client";
import {
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
  LogLevel
} from "@microsoft/signalr";
import { useSelectAccessToken } from "@/slices/auth.slice";
import { useSelectUser, useSelectUserId } from "@/slices/user.slice";
import {
  createContext,
  ReactNode,
  useCallback,
  useContext,
  useEffect,
  useRef,
  useState
} from "react";
import { useSubscribeSignalREvents } from "@/hooks/signalR/useSubscribeSignalREvents";
import { SIGNALR_URI } from "@/constants/apiClient";

interface SignalRHubContextType {
  connection: HubConnection | null;
  startConnection: () => Promise<void>;
  stopConnection: () => Promise<void>;
  connected: boolean;
  onlineUserCount: number;
}

interface Props {
  children: ReactNode;
}

const SignalRHubContext = createContext<SignalRHubContextType | null>(null);

const SignalRHubProvider = ({ children }: Props) => {
  const hubUrl = `${SIGNALR_URI}/tastopia-hub`;
  console.log("hubUrl", hubUrl);
  const [waitingToReconnect, setWaitingToReconnect] = useState(true);
  const accessToken = useSelectAccessToken();
  const user = useSelectUser();
  const currentUserId = useSelectUserId();

  const { subscribeAllEvents, unsubscribeAllEvents, onlineUserCount } =
    useSubscribeSignalREvents();
  const connectionRef = useRef<HubConnection | null>(null);
  const isConnected = connectionRef?.current?.state === HubConnectionState.Connected;

  const startConnection = useCallback(async () => {
    if (!connectionRef.current || isConnected) return;

    console.log("starting signalr with user", user, "accessToken", accessToken);
    console.log("connectionRef.current", connectionRef.current);

    connectionRef.current
      .start()
      .then(() => {
        connectionRef.current && subscribeAllEvents(connectionRef.current);
        setWaitingToReconnect(false);
        console.log("signalR connected");
      })
      .catch(err => console.error(err));
  }, [subscribeAllEvents, user, accessToken, isConnected]);

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
        connected: !waitingToReconnect,
        onlineUserCount
      }}
    >
      {children}
    </SignalRHubContext.Provider>
  );
};

const useSignalR = () => {
  const context = useContext(SignalRHubContext);
  if (context === undefined) {
    throw new Error("useSignalR must be use within a SignalRProvider");
  }

  return context;
};

export { SignalRHubProvider, SignalRHubContext, useSignalR };
