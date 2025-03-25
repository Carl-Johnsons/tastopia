"use client";
import { API_GATEWAY_HOST } from "@/constants/api";
import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { useSelectAccessToken } from "@/slices/auth.slice";
import { userSelectUserId } from "@/slices/user.slice";
import {
  createContext,
  ReactNode,
  useCallback,
  useEffect,
  useRef,
  useState
} from "react";
import { useSubscribeSignalREvents } from "@/hooks/signalR/useSubscribeSignalREvents";

interface SignalRHubContextType {
  connection: HubConnection | null;
  startConnection: () => Promise<void>;
  stopConnection: () => Promise<void>;
  connected: boolean;
}

interface Props {
  children: ReactNode;
}

const SignalRHubContext = createContext<SignalRHubContextType | null>(null);

const SignalRHubProvider = ({ children }: Props) => {
  const hubUrl = `http://localhost:5004/tastopia-hub`;
  const [waitingToReconnect, setWaitingToReconnect] = useState(true);
  const currentUserId = "f9a8c16e-610a-49f5-aac0-82183d8c3a16";
  const accessToken =
    "eyJhbGciOiJSUzI1NiIsImtpZCI6IkE4NTY1OUE0NUU1RTgyMzM3MjAzNUM1QzY1QzE4NjQzIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDAxIiwibmJmIjoxNzQyNTc1OTUwLCJpYXQiOjE3NDI1NzU5NTAsImV4cCI6MTc0NTE2Nzk1MCwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzAwMS9yZXNvdXJjZXMiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwicGhvbmUiLCJlbWFpbCIsIklkZW50aXR5U2VydmVyQXBpIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInB3ZCJdLCJjbGllbnRfaWQiOiJuZXh0SlMiLCJzdWIiOiJmOWE4YzE2ZS02MTBhLTQ5ZjUtYWFjMC04MjE4M2Q4YzNhMTYiLCJhdXRoX3RpbWUiOjE3NDI1NzU5NTAsImlkcCI6ImxvY2FsIiwiZ2l2ZW5fbmFtZSI6ImFkbWluIiwicGhvbmVfbnVtYmVyIjoiMDExMTExMTExOSIsImVtYWlsIjoiYWRtaW5AZXhhbXBsZS5jb20iLCJyb2xlIjoiU1VQRVIgQURNSU4iLCJzaWQiOiI5OUEzNDQ3NTA0Q0Q1OTg2NzQyQkM2NTNCOTc0OUNGRiIsImp0aSI6IjJEMDY0NjIzMDgwNTc3N0ZGRTQ5NjM0RDlEOTY3NDFEIn0.UjBMDDiikcO-Ahh4PkboLj1Nyjc84Wge4JCeYcG1eFf42wTiTG85C88r5NmisGREymnRG0V7JNu96DS6zclzMSaZwgnIffKvKylee7IzZ6y1MLaFxIxMLXkWwW2sehXZ_h7LA0cnaNMnRi0xEFv1vyzjpUdBi9MbovA-WIFl2Q9ohXIBnJktW3Lg22uAGcy2P_NQYylsW1HQcS80D9XbWy1WbPLagXJFa3e7ylJWBlwqk2vG1FYR1TtR6XseDEeInA0y1RipT6xA4gC5Mn6zEOGBrvpuakbWnGiF_tstEqVCBOPBiDnslqPhjizDYPtCCVhajBKn40sTjZxkL5NgsQ";

  // console.log("current user id", currentUserId);
  // console.log("accessToken", accessToken);

  const { subscribeAllEvents, unsubscribeAllEvents } = useSubscribeSignalREvents();

  const connectionRef = useRef<HubConnection | null>(null);

  const startConnection = useCallback(async () => {
    if (!connectionRef.current) {
      return;
    }
    console.log("starting signalr");
    console.log("connectionRef.current", connectionRef.current);
    connectionRef.current
      .start()
      .then(() => {
        connectionRef.current && subscribeAllEvents(connectionRef.current);
        setWaitingToReconnect(false);
        console.log("signalR connected");
      })
      .catch(err => console.error(err));
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
