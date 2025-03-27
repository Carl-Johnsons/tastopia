import { IErrorResponseDTO } from "@/generated/interfaces/common.interface";
import { AxiosError } from "axios";

/**
 * Handle Axios error by throwing a customised Error object. Intented to be used in a catch
 * block after fetching api. The newly thrown error should be handled from upstream code.
 */
export const withErrorProcessor = (error: unknown) => {
  if (error instanceof AxiosError) {
    const { code } = error.response?.data as IErrorResponseDTO;
    throw new Error(code);
  }

  throw error;
};
