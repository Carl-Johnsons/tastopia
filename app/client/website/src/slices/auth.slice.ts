import { PURGE } from "redux-persist";
import { Roles } from "@/constants/role";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { useAppSelector } from "@/store/hooks";

export interface AuthState {
  accessToken: string | null;
  refreshToken: string | null;
  idToken: string | null;
  role: Roles | null;
  isVerifyingAccount: boolean;
  verifyIdentifier: string | null;
}

const initialState: AuthState = {
  accessToken: null,
  refreshToken: null,
  idToken: null,
  role: null,
  isVerifyingAccount: false,
  verifyIdentifier: null,
};

export const AuthSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    saveAuthData: (state, { payload }: PayloadAction<Partial<AuthState>>) => {
      Object.assign(state, payload);
    },
    clearAuthData: (state) => {
      Object.assign(state, initialState);
    },
  },
  extraReducers: (builder) => {
    builder.addCase(PURGE, () => {
      return initialState;
    });
  },
});

export const useSelectAccessTokena = () =>
  useAppSelector((state) => state.auth.accessToken);

export const useSelectAccessToken = () =>
  useAppSelector((state) => state.auth.accessToken);

export const useSelectRefreshToken = () =>
  useAppSelector((state) => state.auth.refreshToken);

export const useSelectIdToken = () =>
  useAppSelector((state) => state.auth.idToken);

export const useSelectRole = () => useAppSelector((state) => state.auth.role);

export const useSelectIsVerifyingAccount = () =>
  useAppSelector((state) => state.auth.isVerifyingAccount);

export const useSelectVerifyIdentifier = () =>
  useAppSelector((state) => state.auth.verifyIdentifier);

export const { saveAuthData, clearAuthData } = AuthSlice.actions;

export default AuthSlice.reducer;
