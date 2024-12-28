import AsyncStorage from "@react-native-async-storage/async-storage";
import Constants from "expo-constants";
import { Platform } from "react-native";

const { expoConfig } = Constants;
const host: string = expoConfig?.hostUri?.split(":")[0] || "10.0.2.2";

const API_URL = `http://${host}:5050`;

/**
 * A helper function that retrieves the user's JWT token from the Redux store.
 *
 * @returns The JWT token if it exists, otherwise null
 */
const getAuthToken = async (): Promise<string | null> => {
  const item = await AsyncStorage.getItem("persist:root");
  const jwtToken = item ? JSON.parse(JSON.parse(item).auth).jwtToken : null;
  return jwtToken ? `Bearer ${jwtToken}` : null;
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
      Authorization: await getAuthToken()
    }
  } as RequestInit;

  if (init) {
    Object.assign(init, { headers: { ...init.headers, ...DEFAULT_OPTIONS.headers } });
  }

  return fetch(`${API_URL}${route}`, init || DEFAULT_OPTIONS);
};

const getBaseUrl = (port: string) => {
  const isAndroid = Platform.OS === "android";

  if (isAndroid) return `http://10.0.2.2:${port}/`;
  else return `http://localhost:${port}/`;
};

// http://10.0.2.2:5005/api/recipe/get-recipe-feed
export const getAPIUrl = (port: string | number, endpoint: string) => {
  const baseUrl = getBaseUrl(port.toString());

  return baseUrl + endpoint;
};
