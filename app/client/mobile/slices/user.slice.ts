import { useAppSelector } from "@/store/hooks";
import { createSlice } from "@reduxjs/toolkit";
import { PURGE } from "redux-persist";

export enum GENDER {
  MALE = "MALE",
  FEMALE = "FEMALE"
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
  displayName: "Guest"
};

export const selectUser = () => useAppSelector(state => state.user);
export const selectUserId = () => useAppSelector(state => state.user.accountId);
export const selectIsActiveUser = () =>
  useAppSelector(state => state.user.accountId && state.user.isAccountActive);

export const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    saveUserData: (state, action: { type: string; payload: Partial<UserState> }) => {
      Object.assign(state, action.payload);
    }
  },
  extraReducers: builder => {
    builder.addCase(PURGE, () => {
      return initialState;
    });
  }
});

export const { saveUserData } = userSlice.actions;
export default userSlice.reducer;
