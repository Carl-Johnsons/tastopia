import { useAppSelector } from "@/store/hooks";
import { createSlice } from "@reduxjs/toolkit";
import { PURGE } from "redux-persist";

export type HistoryState = {
  isLoading: boolean;
  currentRecipeId: string;
  currentAuthorId: string;
};

export type SaveHistoryDataAction = {
  type: string;
  payload: Partial<HistoryState>;
};

const initialState: HistoryState = {
  isLoading: false,
  currentRecipeId: "",
  currentAuthorId: "",
};

export const selectHistory = () => useAppSelector(state => state.history);
export const selectHistoryIsLoading = () =>
  useAppSelector(state => state.history.isLoading);

export const HistorySlice = createSlice({
  name: "history",
  initialState,
  reducers: {
    saveHistoryData: (state, action: SaveHistoryDataAction) => {
      Object.assign(state, action.payload);
    }
  },
  extraReducers: builder => {
    builder.addCase(PURGE, () => {
      return initialState;
    });
  }
});

export const { saveHistoryData } = HistorySlice.actions;
export default HistorySlice.reducer;
