"use server";

import { protectedAxiosInstance } from "@/constants/host";
import { IPaginatedTagResponse } from "@/types/tag";

export async function getTags(
  skip = 0,
  sortBy = "",
  sortOrder = "asc",
  keyword = "",
  limit = 6
) {
  try {
    const url = `/api/admin/recipe/get-tags?Skip=${skip}&SortBy=${sortBy}&SortOrder=${sortOrder}&limit=${limit}&Keyword=${encodeURIComponent(keyword)}`;

    const { data } = await protectedAxiosInstance.get(url);

    return data as IPaginatedTagResponse;
  } catch (error) {
    console.log(error);
    throw error;
  }
}
