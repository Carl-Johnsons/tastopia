import { Gender } from "@/constants/gender";
import { Roles } from "@/constants/role";
import { INumberedPaginatedMetadata } from "@/generated/interfaces/common.interface";
import { getCreateAdminSchema } from "@/schemas/admin";
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

const CreateAdminSchema = getCreateAdminSchema((key: string) => key);
export type CreateAdminFormFields = z.infer<typeof CreateAdminSchema>;
