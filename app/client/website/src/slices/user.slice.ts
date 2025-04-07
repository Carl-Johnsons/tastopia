import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { useAppSelector } from "@/store/hooks";
import { IAdminDetailResponse } from "@/generated/interfaces/user.interface";

export type UserState = Partial<IAdminDetailResponse>;

const initialState: UserState = {};

export const useSelectUser = () => useAppSelector(state => state.user);
export const useSelectUserId = () => useAppSelector(state => state.user.accountId);
export const useSelectIsActiveUser = () =>
  useAppSelector(state => state.user.accountId && state.user.isActive);

export const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    saveUserData: (state, { payload }: PayloadAction<Partial<UserState>>) => {
      Object.assign(state, payload);
    },
    clearUserData: state => {
      Object.assign(state, initialState);
    }
  }
});

export const { saveUserData, clearUserData } = userSlice.actions;
export default userSlice.reducer;
