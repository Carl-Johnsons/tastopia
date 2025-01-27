import {
  AuthSessionResult,
  CodeChallengeMethod,
  exchangeCodeAsync,
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

  // const redirectUri = makeRedirectUri({
  //   scheme: "com.tastopia.app",
  //   isTripleSlashed: true
  // });

  const clientId = "react.native";
  const redirectUri = "com.tastopia.app://"; // TODO: Remove hardcode redirect Uri
  const [base64_state] = useState(() => btoa(Math.random().toString()));

  const [request, _, promptAsync] = useAuthRequest(
    {
      clientId: "react.native",
      redirectUri: redirectUri,
      scopes: ["openid", "profile", "email", "offline_access"],
      state: base64_state,
      codeChallengeMethod: CodeChallengeMethod.S256
    },
    discovery
  );

  const loginWithGoogle = async () => {
    if (!request) {
      return;
    }
    console.log("Initing google flow");

    try {
      console.log("Start login flow");
      console.log("redirectUri", redirectUri);
      maybeCompleteAuthSession();

      const result = await promptAsync();
      if (request && result?.type === "success" && discovery) {
        const tokenResponse = await exchangeCodeAsync(
          {
            clientId,
            redirectUri,
            code: result.params.code,
            extraParams: request.codeVerifier
              ? { code_verifier: request.codeVerifier }
              : undefined
          },
          discovery
        );
        console.log(JSON.stringify(tokenResponse, null, 2));
      } else {
        console.error("Error");
      }

      console.log(JSON.stringify(result, null, 2));
    } catch (error) {
      console.error("Authorization error:", error);
    }
  };

  return { loginWithGoogle, response: undefined };
};

export default useLoginWithGoogle;
