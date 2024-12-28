import { useAppSelector } from "@/store/hooks";
import { createSlice } from "@reduxjs/toolkit";
import { PURGE } from "redux-persist";

export enum ROLE {
  PREMIUM,
  USER,
  GUEST
}

export interface AuthState {
  accessToken: string | null;
  refreshToken: string | null;
  role: ROLE | null;
}

const initialState: AuthState = {
  accessToken: null,
  refreshToken: null,
  role: ROLE.GUEST
};

export const selectAccessToken = () => useAppSelector(state => state.auth.accessToken);
export const selectRefreshToken = () => useAppSelector(state => state.auth.refreshToken);
export const selectRole = () => useAppSelector(state => state.auth.role);

export const AuthSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    saveAuthData: (state, action) => {
      Object.assign(state, action.payload);
    }
  },
  extraReducers: builder => {
    builder.addCase(PURGE, () => {
      return initialState;
    });
  }
});

export const { saveAuthData } = AuthSlice.actions;

export default AuthSlice.reducer;
