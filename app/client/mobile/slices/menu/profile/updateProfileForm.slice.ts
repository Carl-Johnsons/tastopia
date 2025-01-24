import { GENDER } from "@/slices/user.slice";
import { useAppSelector } from "@/store/hooks";
import { createSlice } from "@reduxjs/toolkit";

type IsDirtyFields = {
  avatar: boolean;
  background: boolean;
};

export type UpdateProfileState = {
  avatar?: ImageFileType;
  isDirty: IsDirtyFields;
  background?: ImageFileType;
  triggerSubmit?: string;
  gender?: GENDER;
  isLoading: boolean;
};

const initialState: UpdateProfileState = {
  isLoading: false,
  isDirty: {
    avatar: false,
    background: false
  }
};

export const selectUpdateProfile = () => useAppSelector(state => state.updateProfile);

type SaveDataAction = {
  type: string;
  payload: Partial<UpdateProfileState>;
};

type saveIsDirtyFieldsAction = {
  type: string;
  payload: Partial<IsDirtyFields>;
};

export const UpdateProfileSlice = createSlice({
  name: "updateProfile",
  initialState,
  reducers: {
    saveUpdateProfileData: (state, action: SaveDataAction) => {
      Object.assign(state, action.payload);
    },
    saveIsDirtyFieldsData: (state, action: saveIsDirtyFieldsAction) => {
      Object.assign(state.isDirty, action.payload);
    }
  }
});

export const { saveUpdateProfileData, saveIsDirtyFieldsData } = UpdateProfileSlice.actions;

export default UpdateProfileSlice.reducer;
