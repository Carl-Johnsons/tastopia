"use server";

import { protectedAxiosInstance } from "@/constants/host";

export async function getUserById(id: string) {
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

export async function getUserActivitiesById(id: string, language: string) {
  try {
    const url = "/api/recipe/admin-get-user-activities";
    const { data } = await protectedAxiosInstance.post(url, {
      accountId: id,
      language,
      skip: 0,
    });

    return data;
  } catch (error) {
    console.log(error);
    throw error;
  }
}
