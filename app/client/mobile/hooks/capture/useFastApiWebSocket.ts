import { API_GATEWAY_HOST } from "@/constants/host";
import { selectUserId } from "@/slices/user.slice";
import { transformPlatformURI } from "@/utils/functions";
import { useLayoutEffect, useRef } from "react";

type Props = {
  onMessageHandler?: (event: MessageEvent<any>) => void;
};

const RECONNECT_INTERVAL = 1000;

export const useFastApiWebsocket = ({ onMessageHandler }: Props) => {
  const userId = selectUserId();
  const WEBSOCKET_URL = transformPlatformURI(`ws://${API_GATEWAY_HOST}:5009/ws/video/${userId}`);

  const wsRef = useRef<WebSocket | null>(null);

  useLayoutEffect(() => {
    const connect = () => {
      wsRef.current = new WebSocket(WEBSOCKET_URL);
      wsRef.current.onopen = () => {
        console.log("WebSocket connection opened.");
      };

      wsRef.current.onmessage = event => {
        onMessageHandler && onMessageHandler(event);
      };

      wsRef.current.onerror = error => {
        console.log("WebSocket error:", error);
      };

      wsRef.current.onclose = event => {
        console.log(`WebSocket closed. Reconnecting in ${RECONNECT_INTERVAL} seconds...`);
        setTimeout(connect, RECONNECT_INTERVAL);
      };
    };
    connect();
    console.log(JSON.stringify(wsRef.current, null, 2));
    return () => {
      wsRef.current && wsRef.current.close();
    };
  }, []);

  return wsRef;
};
