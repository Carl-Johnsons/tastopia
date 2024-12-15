import { useAppSelector } from "@/store/hooks";
import { createSlice, current } from "@reduxjs/toolkit";
import { PURGE } from "redux-persist";

export interface AuthState {
  jwtToken: string | null;
}

const initialState: AuthState = {
  jwtToken: null
};

export const selectJwtToken = () => useAppSelector(state => state.auth.jwtToken);

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
