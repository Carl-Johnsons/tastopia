import { IErrorResponseDTO } from "@/generated/interfaces/common.interface";
import { redirect } from "@/i18n/navigation";
import { AxiosError } from "axios";
import { getLocale } from "next-intl/server";

/**
 * Handle Axios error by throwing a customised Error object. Intented to be used in a catch
 * block after fetching api. The newly thrown error should be handled from upstream code.
 */
export const withErrorProcessor = (error: unknown) => {
  console.error("Error", error);

  if (error instanceof AxiosError) {
    if (!error.response || !error.response.data || !error.response.data.code) throw error;
    const { code } = error.response?.data as IErrorResponseDTO;
    throw new Error(code);
  }

  throw error;
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
