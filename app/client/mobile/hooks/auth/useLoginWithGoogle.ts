import {
  AuthSessionResult,
  makeRedirectUri,
  useAuthRequest,
  useAutoDiscovery
} from "expo-auth-session";
import { API_GATEWAY_HOST } from "@/constants/host";
import { maybeCompleteAuthSession } from "expo-web-browser";
import { transformPlatformURI } from "@/utils/functions";
import { useState } from "react";

interface UseLoginWithGoogleResult {
  /** The response object */
  response?: AuthSessionResult | null;
  /** Initializes the Google login flow. */
  loginWithGoogle: () => Promise<void>;
}

/**
 * A hook that hanldes Google's login flow.
 *
 * @param initialValue The initial image data object
 */
export const useLoginWithGoogle = (): UseLoginWithGoogleResult => {
  const discoveryUrl = transformPlatformURI(`http://${API_GATEWAY_HOST}:5001`);

  const discovery = useAutoDiscovery(discoveryUrl);

  // const redirectUri = makeRedirectUri();
  const redirectUri = "com.tastopia.app://";
  const [base64_state] = useState(() => btoa(Math.random().toString()));

  const [_request, response, promptAsync] = useAuthRequest(
    {
      clientId: "react.native",
      redirectUri: redirectUri,
      scopes: ["openid", "profile", "email", "offline_access"],
      state: base64_state
    },
    discovery
  );

  const loginWithGoogle = async () => {
    console.log("Initing google flow");

    console.log("Start login flow");
    console.log("redirectUri", redirectUri);

    maybeCompleteAuthSession();
    await promptAsync();

    console.log("End login flow");
    console.log("Data:", JSON.stringify(response, null, 2));
  };

  return { loginWithGoogle, response: undefined };
};

export default useLoginWithGoogle;
