import { PURGE } from "redux-persist";
import { RootState } from "@/store";
import { Roles } from "@/constants/role";
import { createSelector, createSlice } from "@reduxjs/toolkit";
import { useAppSelector } from "@/store/hooks";

export interface AuthState {
  accessToken: string | null;
  refreshToken: string | null;
  role: Roles | null;
  isVerifyingAccount: boolean;
  verifyIdentifier: string | null;
}

export type SaveAuthDataAction = {
  type: string;
  payload: Partial<AuthState>;
};

const initialState: AuthState = {
  accessToken: null,
  refreshToken: null,
  role: null,
  isVerifyingAccount: false,
  verifyIdentifier: null,
};

export const AuthSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    saveAuthData: (state, action: SaveAuthDataAction) => {
      Object.assign(state, action.payload);
    },
  },
  extraReducers: (builder) => {
    builder.addCase(PURGE, () => {
      return initialState;
    });
  },
});

export const useSelectAccessTokena = () => useAppSelector((state) => state.auth.accessToken);

export const useSelectAccessToken = () => useAppSelector((state) => state.auth.accessToken);

export const useSelectRefreshToken = () => useAppSelector((state) => state.auth.refreshToken);

export const useSelectRole = () => useAppSelector((state) => state.auth.role);

export const useSelectIsVerifyingAccount = () => useAppSelector((state) => state.auth.isVerifyingAccount);

export const useSelectVerifyIdentifier = () => useAppSelector((state) => state.auth.verifyIdentifier);

export const { saveAuthData } = AuthSlice.actions;

export default AuthSlice.reducer;
