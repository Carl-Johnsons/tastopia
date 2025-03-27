import {
  IAdminGetAdminDetailResponse,
  IAdminListResponse,
  IPaginatedAdminListResponse
} from "@/types/admin";
import { faker } from "@faker-js/faker";
import { Roles } from "./role";
import { Gender } from "./gender";

faker.seed(123);

const generateRandomAdminListResponse = (): IAdminListResponse => ({
  accountId: faker.string.uuid(),
  username: faker.internet.username(),
  displayName: faker.person.fullName(),
  email: faker.internet.email(),
  phoneNumber: faker.phone.number(),
  dob: faker.date.past().toUTCString(),
  isActive: faker.datatype.boolean(),
  address: faker.location.city()
});

const generateRandomAdminDetailResponse = (): IAdminGetAdminDetailResponse => ({
  ...generateRandomAdminListResponse(),
  avatarUrl: faker.image.avatar(),
  role: faker.helpers.arrayElement(Object.values(Roles)),
  gender: faker.helpers.arrayElement(Object.values(Gender))
});

const generatePaginatedAdminListResponse = (
  count: number
): IPaginatedAdminListResponse => ({
  paginatedData: Array.from({ length: count }, generateRandomAdminListResponse),
  metadata: {
    totalRow: count,
    currentPage: 1,
    totalPage: count
  }
});


export const adminListData = generatePaginatedAdminListResponse(33);
export const adminDetailData = generateRandomAdminDetailResponse();
