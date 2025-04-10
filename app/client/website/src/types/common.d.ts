import { IErrorResponseDTO } from "@/generated/interfaces/common.interface";

export type PaginatedQueryParams = {
  limit?: number;
  skip?: number;
  sortBy?: string;
  sortOrder?: string;
  lang?: string;
  keyword?: string;
};

export type SuccessResponse<T> = {
  ok: true;
  data: T;
};

export type ErrorResponse = {
  ok: false;
  error: IErrorResponseDTO;
};

export type Response<T> = SuccessResponse<T> | ErrorResponse;
