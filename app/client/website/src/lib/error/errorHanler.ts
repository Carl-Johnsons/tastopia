"use client";

import { IErrorResponseDTO } from "@/generated/interfaces/common.interface";
import { ErrorResponse } from "@/types/common";
import { toast } from "react-toastify";
/**
 * Handle Axios error by checking for error code's presence. Intented to be used in a catch
 * block after using a useMutation hook.
 */
export const handleError = (error: any, t: (key: string) => string) => {
  console.debug("Error", error);

  if (!error.response || !error.response.data || !error.response.data.code) {
    toast.error(t("General"));
    return;
  }

  const data = error.response.data as IErrorResponseDTO;
  const { code } = data;
  console.log("code", code);
  toast.error(t(code));
};

export const handleBareError = ({ error }: ErrorResponse, t: (key: string) => string) => {
  console.debug("Error", error);

  const data = error as IErrorResponseDTO;
  const { code } = data;
  toast.error(t(code));
};
