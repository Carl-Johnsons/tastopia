import { API_HOST } from "@/constants/host";
import Constants from "expo-constants";
import { useState } from "react";

const { expoConfig } = Constants;
const hostUri: string = API_HOST || expoConfig?.hostUri?.split(":")[0] || "10.0.2.2";

interface UseApiHostResult {
  /** The api's host */
  host: string;
}

/**
 * A hook that return backend api's host.
 */
export const useApiHost = (): UseApiHostResult => {
  const [host, _setHost] = useState<string>(hostUri);
  return { host };
};
