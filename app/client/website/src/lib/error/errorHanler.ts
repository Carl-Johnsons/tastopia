"use client";

import { IErrorResponseDTO } from "@/generated/interfaces/common.interface";
import { ErrorResponse } from "@/types/common";
import { toast } from "react-toastify";

export class ApiError extends Error {
  public readonly response: ErrorResponse;

  constructor(response: ErrorResponse) {
    super(response.error.message);
    this.name = "APIError";
    this.response = response;
  }
}

/**
 * Handle APIError. Intented to be used in a catch block after using a useMutation hook.
 */
export const handleError = (
  { response: { error } }: ApiError,
  t: (key: string) => string
) => {
  console.debug("Error", error);

  const { code } = error;
  console.log("code", code);
  toast.error(t(code));
};

/**
 * Handle ErrorResponse. This is an utility function for the useErrorHanler hook, use it instead.
 */
export const handleBareError = ({ error }: ErrorResponse, t: (key: string) => string) => {
  console.debug("Error", error);

  const data = error as IErrorResponseDTO;
  const { code } = data;
  toast.error(t(code));
};
