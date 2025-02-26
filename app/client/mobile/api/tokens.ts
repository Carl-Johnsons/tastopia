import axios, { AxiosError } from "axios";
import { CLIENT_ID, SCOPE } from "@/constants/api";
import { stringify } from "@/utils/debug";
import { IErrorResponseDTO } from "@/generated/interfaces/common.interface";

export const refreshAccessToken = async (refreshToken: string) => {
  const body = new URLSearchParams({
    client_id: CLIENT_ID,
    scope: SCOPE,
    grant_type: "refresh_token",
    refresh_token: refreshToken
  }).toString();

  try {
    const { data } = await axios.post<LoginResponse>("/connect/token", body, {
      headers: {
        "Content-Type": "application/x-www-form-urlencoded"
      }
    });

    return data;
  } catch (error) {
    console.debug("refreshAccessToken", stringify(error));

    if (error instanceof AxiosError) {
      const data = error.response?.data as IErrorResponseDTO;
      throw new Error(data.message);
    }

    throw new Error("An error has occurred.");
  }
};
