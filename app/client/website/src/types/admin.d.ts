import { Gender } from "@/constants/gender";
import { Roles } from "@/constants/role";
import { INumberedPaginatedMetadata } from "@/generated/interfaces/common.interface";
import { getCreateAdminSchema, getUpdateAdminSchema } from "@/schemas/admin";
import { z } from "zod";

export type IAdminListResponse = {
  accountId: string;
  username: string;
  displayName: string;
  email: string;
  phoneNumber: string;
  dob: string;
  isActive: boolean;
  address: string;
};

export type IPaginatedAdminListResponse = {
  paginatedData: IAdminListResponse[];
  metadata?: INumberedPaginatedMetadata;
};

export type IAdminGetAdminDetailResponse = IAdminListResponse & {
  role: Roles;
  gender: Gender;
  avatarUrl: string;
};

const createAdminSchema = getCreateAdminSchema((key: string) => key);
export type CreateAdminSchema = typeof createAdminSchema;
export type CreateAdminFormFields = z.infer<typeof createAdminSchema>;

const updateAdminSchema = getUpdateAdminSchema((key: string) => key);
export type UpdateAdminSchema = typeof updateAdminSchema;
export type UpdateAdminFormFields = z.infer<typeof updateAdminSchema>;
export type ImageFieldType = Pick<CreateAdminFormFields, "avatarFile">["avatarFile"];
