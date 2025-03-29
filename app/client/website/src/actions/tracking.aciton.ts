"use server";

import { protectedAxiosInstance } from "@/constants/host";
import { IPaginatedAdminActivityLogListResponse } from "@/generated/interfaces/tracking.interface";
import { PaginatedQueryParams } from "@/types/common";
import { withErrorProcessor } from "@/utils/errorHanlder";

export async function getActivityLogs(options?: PaginatedQueryParams) {
  const url = "/api/admin/tracking/all";
  const {
    limit = 10,
    skip = 0,
    sortBy = "createdAt",
    sortOrder = "DESC",
    lang = "en",
    keyword = ""
  } = options || {};

  try {
    const res = await protectedAxiosInstance.get<IPaginatedAdminActivityLogListResponse>(
      url,
      {
        params: {
          limit,
          skip,
          sortBy,
          sortOrder,
          lang,
          keyword: encodeURIComponent(keyword)
        }
      }
    );

    return res.data;
  } catch (error) {
    withErrorProcessor(error);
    throw error;
  }
}
