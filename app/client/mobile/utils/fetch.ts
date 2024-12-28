import { AuthState } from "@/slices/auth.slice";
import AsyncStorage from "@react-native-async-storage/async-storage";
import Constants from "expo-constants";

const { expoConfig } = Constants;
const host: string =
  process.env.API_HOST || expoConfig?.hostUri?.split(":")[0] || "10.0.2.2";
console.log("Host config", host);

const API_URL = `http://${host}:5000`;

/**
 * A helper function that retrieves the user's JWT token from the Redux store.
 *
 * @returns The Access token if it exists, otherwise null
 */
export const getAccessToken = async (): Promise<string | null> => {
  const item = await AsyncStorage.getItem("persist:root");
  let accessToken = null;

  if (item) {
    const authState = JSON.parse(JSON.parse(item).auth) as AuthState;
    accessToken = authState.accessToken;
  }

  return accessToken ? `Bearer ${accessToken}` : null;
};

/**
 * A wrapper function for fetching data from the project's server.
 *
 * @param route The api route that is intended to be called
 * @param init The init options
 * @returns A promise containing the response
 */
export const fetchApi = async (route: string, init?: RequestInit) => {
  const DEFAULT_OPTIONS = {
    headers: {
      "Content-Type": "application/json",
      Authorization: await getAccessToken()
    }
  } as RequestInit;

  if (init) {
    Object.assign(init, { headers: { ...init.headers, ...DEFAULT_OPTIONS.headers } });
  }

  return fetch(`${API_URL}${route}`, init || DEFAULT_OPTIONS);
};
