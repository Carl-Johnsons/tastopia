import {
  TokenResponse,
  exchangeCodeAsync,
  makeRedirectUri,
  useAuthRequest,
  useAutoDiscovery
} from "expo-auth-session";
import { API_GATEWAY_HOST } from "@/constants/host";
import { maybeCompleteAuthSession } from "expo-web-browser";
import { transformPlatformURI } from "@/utils/functions";
import { useState } from "react";
import Constants from "expo-constants";
import { CLIENT_ID, SCOPE } from "@/constants/api";
import { useTranslation } from "react-i18next";
import { Alert, Platform } from "react-native";
import { stringify } from "@/utils/debug";
import { useDispatch } from "react-redux";
import { ROLE, saveAuthData } from "@/slices/auth.slice";
import useSyncSetting from "../user/useSyncSetting";
import useSyncUser from "../user/useSyncUser";
import { router } from "expo-router";

interface UseLoginWithGoogleResult {
  /** Initialize the Google login flow. */
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
  const { t: te } = useTranslation("loginWithGoogle", { keyPrefix: "error" });
  const { t: ti } = useTranslation("loginWithGoogle", { keyPrefix: "info" });

  const iosBundleId = Constants.expoConfig?.ios?.bundleIdentifier;
  const androidPackage = Constants.expoConfig?.android?.package;

  const [base64_state] = useState(() => btoa(Math.random().toString()));

  const redirectUri = makeRedirectUri({
    scheme: Platform.select({
      ios: iosBundleId,
      android: androidPackage
    })
  });

  const [request, _, promptAsync] = useAuthRequest(
    {
      clientId: CLIENT_ID,
      redirectUri,
      scopes: SCOPE.split(" "),
      state: base64_state
    },
    discovery
  );

  const loginWithGoogle = async () => {
    if (!request) {
      return;
    }

    try {
      console.log("Start login flow");
      console.log("redirectUri", redirectUri);
      maybeCompleteAuthSession();

      const result = await promptAsync();

      if (result.type === "dismiss" || result.type === "cancel") {
        return Alert.alert(ti("title"), ti("cancel"));
      } else if (result.type === "error") {
        return Alert.alert(te("title"), te("error"));
      }

      if (request && result?.type === "success" && discovery) {
        const tokenResponse = await exchangeCodeAsync(
          {
            clientId: CLIENT_ID,
            redirectUri,
            code: result.params.code,
            extraParams: request.codeVerifier
              ? { code_verifier: request.codeVerifier }
              : undefined
          },
          discovery
        );

        await saveData(tokenResponse);
        router.navigate("/(protected)");
      }
    } catch (error) {
      Alert.alert(te("title"), te("error"));
      console.debug("Authorization error:", stringify(error));
    }
  };

  const dispatch = useDispatch();
  const { upload: uploadSettings } = useSyncSetting();
  const { fetch: fetchUser } = useSyncUser();

  const saveData = async (tokens: TokenResponse) => {
    console.log("Tokens", stringify(tokens));
    const { accessToken, refreshToken } = tokens;

    dispatch(
      saveAuthData({
        accessToken,
        refreshToken,
        role: ROLE.USER
      })
    );

    await uploadSettings();
    await fetchUser();
  };

  return { loginWithGoogle };
};

export default useLoginWithGoogle;
