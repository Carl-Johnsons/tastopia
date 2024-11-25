import { useAppSelector } from "@/store/hooks";
import { createSlice, current } from "@reduxjs/toolkit";
import { PURGE } from "redux-persist";

export type UserState = {
  _id: string | null;
  name: string | null;
  username: string | null;
  email: string | null;
  profilePic: string | null;
  followers: Array<string> | null;
  following: Array<string> | null;
  bio: string | null;
};

const initialState: UserState = {
  _id: null,
  name: null,
  username: null,
  email: null,
  profilePic: null,
  followers: null,
  following: null,
  bio: null
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
