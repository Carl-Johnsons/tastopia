"use server";

import { protectedAxiosInstance } from "@/constants/host";
import { StatisticItem } from "@/types/statistic";
import { IPaginatedTagResponse, Tag } from "@/types/tag";
import { getLocale } from "next-intl/server";

export async function getTags(
  skip = 0,
  sortBy = "",
  sortOrder = "asc",
  keyword = "",
  limit = 6
) {
  const language = await getLocale();

  try {
    const url = `/api/admin/recipe/get-tags?Skip=${skip}&SortBy=${sortBy}&SortOrder=${sortOrder}&limit=${limit}&Keyword=${encodeURIComponent(keyword.trim())}`;

    const { data } = await protectedAxiosInstance.post(url, {
      language
    });

    return data as IPaginatedTagResponse;
  } catch (error) {
    console.log(error);
    throw error;
  }
}

export async function createTag(formData: FormData) {
  try {
    const url = "/api/admin/recipe/create-tag";
    const response = await protectedAxiosInstance.post(url, formData, {
      headers: {
        "Content-Type": "multipart/form-data"
      }
    });

    return response.data as Tag;
  } catch (error) {
    console.log(error);
    throw error;
  }
}

export async function updateTag(formData: FormData) {
  try {
    const url = "/api/admin/recipe/update-tag";
    const response = await protectedAxiosInstance.post(url, formData, {
      headers: {
        "Content-Type": "multipart/form-data"
      }
    });

    return response.data as Tag;
  } catch (error) {
    console.log(error);
    throw error;
  }
}

export async function getTagRanking() {
  const language = await getLocale();
  const url = "/api/admin/recipe/statistic/get-tag-ranking-by-popular";
  try {
    const { data } = await protectedAxiosInstance.post<StatisticItem[]>(url, {
      language
    });
    return data;
  } catch (error) {
    console.log(error);
    throw error;
  }
}
