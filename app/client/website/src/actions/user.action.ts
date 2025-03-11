"use server";

import { protectedAxiosInstance } from "@/constants/host";
import { cookies } from "next/headers";

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

export async function setRandomCookie() {
  const cookieStore = cookies();

  try {
    cookieStore.set("randomCookie", "randomValue");
  } catch (error) {
    console.error("Error setting cookie", error);
  }
}
