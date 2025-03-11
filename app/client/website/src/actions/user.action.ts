"use server";

import { protectedAxiosInstance } from "@/constants/host";
import { cookies } from "next/headers";

export async function useGetUserById(id: string) {
  const cookieStore = cookies();
  console.log("Cookies in useGetUserById:", cookieStore.getAll());

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
