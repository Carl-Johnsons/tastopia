import axios, { AxiosError } from "axios";
import { CLIENT_ID, SCOPE } from "@/constants/api";
import { stringify } from "@/utils/debug";
import { IErrorResponseDTO } from "@/generated/interfaces/common.interface";
import { LoginResponse } from "@/types/api/auth";

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
    console.log("Error refreshing access token", error);

    if (error instanceof AxiosError) {
      const data = error.response?.data as IErrorResponseDTO;
      console.log(data.message);
    }
  }
};
