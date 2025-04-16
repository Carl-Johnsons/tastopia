import { IErrorResponseDTO } from "@/generated/interfaces/common.interface";
import { redirect } from "@/i18n/navigation";
import { ErrorResponse, SuccessResponse } from "@/types/common";
import { AxiosError } from "axios";
import { getLocale } from "next-intl/server";

/**
 * Handle Axios success response by returning a SuccessResponse object.
 */
export const withSuccessfulResponse = <T>(data: T): SuccessResponse<T> => {
  const response: SuccessResponse<T> = {
    ok: true,
    data,
    error: null
  };

  return response;
};

/**
 * Handle Axios error by returning a customised ErrorResponse object. Intented to be used in a catch
 * block after fetching api. The newly returned error should be handled from upstream code.
 */
export const withErrorProcessor = (error: AxiosError): ErrorResponse => {
  console.debug("Error", error);

  const data = error?.response?.data as IErrorResponseDTO;
  const { status, code, message } = data;

  const response: ErrorResponse = {
    ok: false,
    error: {
      status: status ?? 500,
      code: code ?? "GENERAL",
      message: message ?? error.message
    } as IErrorResponseDTO,
    data: null
  };

  return response;
};

export const withPapeErrorProcessor = async (error: unknown) => {
  if (error instanceof AxiosError && error.status === 403) {
    const locale = await getLocale();
    redirect({
      href: {
        pathname: "/auth"
      },
      locale
    });
  }
};
