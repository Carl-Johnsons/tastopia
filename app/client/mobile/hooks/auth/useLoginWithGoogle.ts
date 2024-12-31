import { useApiHost } from "@/hooks";
import {
  AuthSessionResult,
  makeRedirectUri,
  useAuthRequest,
  useAutoDiscovery
} from "expo-auth-session";
import { maybeCompleteAuthSession } from "expo-web-browser";

interface UseLoginWithGoogleResult {
  /** The response object */
  response: AuthSessionResult | null;
  /** Initializes the Google login flow. */
  loginWithGoogle: () => Promise<void>;
}

/**
 * A hook that hanldes Google's login flow.
 *
 * @param initialValue The initial image data object
 */
export const useLoginWithGoogle = (): UseLoginWithGoogleResult => {
  const { host } = useApiHost();
  const discoveryUrl = `http://${host}:5001`;
  const discovery = useAutoDiscovery(discoveryUrl);
  const redirectUri = makeRedirectUri();
  const base64_state = btoa(Math.random().toString());

  const [_request, response, promptAsync] = useAuthRequest(
    {
      clientId: "react.native",
      redirectUri,
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

  return { loginWithGoogle, response };
};

export default useLoginWithGoogle;
