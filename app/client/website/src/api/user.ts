"use server";

import { protectedAxiosInstance } from "@/constants/host";
import { UserState } from "@/slices/user.slice";
import { AxiosError } from "axios";

export type GetUserDetailsResponse = UserState;

export const getUserDetails = async () => {
  const url = "/api/user/get-current-user-details";

  try {
    const { data } = await protectedAxiosInstance.get(url);
    return data as GetUserDetailsResponse;
  } catch (error) {
    if (error instanceof AxiosError) {
      const data = error.response?.data as IErrorResponseDTO;
      throw new Error(data.message ? data.message : error.message);
    }

    throw new Error("An error has occurred.");
  }
};
