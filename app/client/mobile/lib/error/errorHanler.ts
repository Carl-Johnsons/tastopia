import { IErrorResponseDTO } from "@/generated/interfaces/common.interface";
import { Alert } from "react-native";

/**
 * Handle Axios error by checking for error code's presence. Intented to be used in a catch
 * block after using a useMutation hoook.
 */
export const handleError = (error: any, t: (key: string) => string) => {
  if (!error.response || !error.response.data) throw error;
  const data = error.response.data as IErrorResponseDTO;
  const { code } = data;
  Alert.alert(t("alertTitle"), t(code));
  return;
};
