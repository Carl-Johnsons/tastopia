import { IErrorResponseDTO } from "@/generated/interfaces/common.interface";
import { Alert } from "react-native";

/**
 * Handle Axios error by checking for error code's presence. Intented to be used in a catch
 * block after using a useMutation hoook.
 */
export const handleError = (error: any, t: (key: string) => string) => {
  if (!error.response || !error.response.data) throw error;

  console.log("1");
  console.log("error.response.data", error.response.data.code);
  const data = error.response.data as IErrorResponseDTO;
  console.log("data", data);
  console.log("2");

  const { code } = data;
  console.log("3");
  console.log("code", code);
  console.log("4");
  Alert.alert(t("alertTitle"), t(code));
  console.log("5");
  return;
};
