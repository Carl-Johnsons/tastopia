import { INumberedPaginatedMetadata } from "./common.interface";

export enum TagStatus {
  Pending = "Pending",
  Active = "Active",
  Inactive = "Inactive"
}
export interface Tag {
  id: string;
  code: string;
  value: string;
  category: string;
  status: TagStatus;
  imageUrl: string;
  createdAt: string;
}

export interface IPaginatedTagResponse {
  paginatedData: Tag[];
  metadata?: INumberedPaginatedMetadata;
}
