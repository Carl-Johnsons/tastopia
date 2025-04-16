"use server";

import { protectedAxiosInstance } from "@/constants/host";
import { Response } from "@/types/common";
import { StatisticItem } from "@/types/statistic";
import { IPaginatedTagResponse, Tag } from "@/types/tag";
import { withErrorProcessor } from "@/utils/errorHanlder";
import { AxiosError } from "axios";
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

export async function createTag(formData: FormData): Promise<Response<Tag>> {
  try {
    const url = "/api/admin/recipe/create-tag";
    const response = await protectedAxiosInstance.post<Tag>(url, formData, {
      headers: {
        "Content-Type": "multipart/form-data"
      }
    });

    return {
      ok: true,
      data: response.data
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function updateTag(formData: FormData): Promise<Response<Tag>> {
  try {
    const url = "/api/admin/recipe/update-tag";
    const response = await protectedAxiosInstance.post(url, formData, {
      headers: {
        "Content-Type": "multipart/form-data"
      }
    });

    return {
      ok: true,
      data: response.data
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function getTagRanking(): Promise<Response<StatisticItem[]>> {
  const language = await getLocale();
  const url = "/api/admin/recipe/statistic/get-tag-ranking-by-popular";
  try {
    const { data } = await protectedAxiosInstance.post<StatisticItem[]>(url, {
      language
    });

    return {
      ok: true,
      data
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}
