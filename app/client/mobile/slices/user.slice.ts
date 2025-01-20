import { useAppSelector } from "@/store/hooks";
import { createSlice } from "@reduxjs/toolkit";
import { PURGE } from "redux-persist";

export type UserState = {
  accountId?: string;
  displayName?: string;
  avatarUrl?: string;
  backgroundUrl?: string;
  dob?: string;
  gender?: string;
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

export const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    saveUserData: (state, action) => {
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
