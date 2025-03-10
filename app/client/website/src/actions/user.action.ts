"use server";

import { useAxios } from "@/hooks/api/useAxios";
import { privateAxios } from "@/lib/axiosInstance";

export async function useGetUserById(id: string) {
  const { protectedAxiosInstance } = useAxios();
  try {
    const url = "/api/user/get-current-user-details";
    const { data } = await protectedAxiosInstance.post(url, {
      accountId: id,
    });

    return data;
  } catch (error) {
    console.log(error);
    throw error;
  }
}
