import { useAppSelector } from "@/store/hooks";
import { createSlice } from "@reduxjs/toolkit";
import { PURGE } from "redux-persist";

export type UserState = {
  accountId: string | null;
  displayName: string | null;
  avatarUrl: string | null;
  backgroundUrl: string | null;
  dob: string | null;
  gender: string | null;
  bio: string | null;
  address: string | null;
  totalFollower: number | null;
  totalFollowing: number | null;
  totalRecipe: number | null;
  isAccountActive: boolean | null;
  accountUsername: string | null;
  isAdmin: boolean | null;
  accountEmail: string | null;
  accountPhoneNumber: string | null;
};

const initialState: UserState = {
  accountId: null,
  displayName: null,
  avatarUrl: null,
  backgroundUrl: null,
  dob: null,
  gender: null,
  bio: null,
  address: null,
  totalFollower: null,
  totalFollowing: null,
  totalRecipe: null,
  isAccountActive: null,
  accountUsername: null,
  isAdmin: null,
  accountEmail: null,
  accountPhoneNumber: null
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
