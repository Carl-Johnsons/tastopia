import { PURGE } from "redux-persist";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { useAppSelector } from "@/store/hooks";

export interface AdminState {
  isFormOpen: boolean;
  formType: "create" | "update";
  targetId?: string;
  /** Whether the form's data is being fetched. */
  isFormLoading: boolean;
  /** Whether the form is for updating the current user. */
  isSelf?: boolean;
}

const initialState: AdminState = {
  isFormOpen: false,
  formType: "create",
  isFormLoading: false,
};

export const AdminSlice = createSlice({
  name: "admin",
  initialState,
  reducers: {
    saveAdminData: (state, { payload }: PayloadAction<Partial<AdminState>>) => {
      Object.assign(state, payload);
    },
    createAdmin: state => {
      Object.assign(state, {
        isFormOpen: true,
        formType: "create"
      });
    },
    updateAdmin: (
      state,
      { payload }: PayloadAction<Pick<AdminState, "targetId" | "isSelf">>
    ) => {
      const { targetId, isSelf } = payload;

      Object.assign(state, {
        isFormOpen: true,
        formType: "update",
        targetId,
        isSelf
      });
    },
    closeForm: state => {
      Object.assign(state, {
        isFormOpen: false
      });
    },
    clearAdminData: state => {
      Object.assign(state, initialState);
    }
  },
  extraReducers: builder => {
    builder.addCase(PURGE, () => {
      return initialState;
    });
  }
});

export const useSelectAdmin = () => useAppSelector(state => state.admin);

export const { saveAdminData, createAdmin, updateAdmin, clearAdminData, closeForm } =
  AdminSlice.actions;
const adminReducer = AdminSlice.reducer;
export default adminReducer;
