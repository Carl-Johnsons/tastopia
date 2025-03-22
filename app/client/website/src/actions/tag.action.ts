"use server";

import { protectedAxiosInstance } from "@/constants/host";
import { IPaginatedTagResponse, Tag } from "@/types/tag";

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
