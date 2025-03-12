import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { useAppSelector } from "@/store/hooks";

export enum GENDER {
  MALE = "MALE",
  FEMALE = "FEMALE",
}

export type UserState = {
  accountId?: string;
  displayName?: string;
  avatarUrl?: string;
  backgroundUrl?: string;
  dob?: string;
  gender?: GENDER;
  bio?: string;
  address?: string;
  totalFollower?: number;
  totalFollowing?: number;
  totalRecipe?: number;
  isAccountActive?: boolean;
  accountUsername?: string;
  isAdmin?: boolean;
  accountEmail?: string;
  accountPhoneNumber?: string;
};

const initialState: UserState = {
  displayName: "Guest",
};

export const useSelectUser = () => useAppSelector((state) => state.user);
export const userSelectUserId = () =>
  useAppSelector((state) => state.user.accountId);
export const useSelectIsActiveUser = () =>
  useAppSelector((state) => state.user.accountId && state.user.isAccountActive);

export const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    saveUserData: (state, { payload }: PayloadAction<Partial<UserState>>) => {
      Object.assign(state, payload);
    },
    clearUserData: (state) => {
      Object.assign(state, initialState);
    },
  },
});

export const { saveUserData, clearUserData } = userSlice.actions;
export default userSlice.reducer;
