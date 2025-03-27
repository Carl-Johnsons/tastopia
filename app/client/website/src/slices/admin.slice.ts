import { useAppSelector } from "@/store/hooks";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export interface AdminState {
  /** Whether the form is currently submitting. */
  isLoading: boolean;
}

const initialState: AdminState = {
  isLoading: false
};

export const AdminSlice = createSlice({
  name: "admin",
  initialState,
  reducers: {
    setAdminData: (state, { payload }: PayloadAction<Partial<AdminState>>) => {
      Object.assign(state, payload);
    }
  }
});

export const useSelectAdmin = () => useAppSelector(state => state.admin);
export const { setAdminData } = AdminSlice.actions;

const adminReducer = AdminSlice.reducer;
export default adminReducer;
