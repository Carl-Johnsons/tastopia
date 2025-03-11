"use server";

import { protectedAxiosInstance } from "@/constants/host";

export async function useGetUserById(id: string) {
  try {
    const url = "/api/user/admin-get-user-detail";
    const { data } = await protectedAxiosInstance.post(url, {
      accountId: id,
    });

    return data;
  } catch (error) {
    console.log(error);
    throw error;
  }
}
