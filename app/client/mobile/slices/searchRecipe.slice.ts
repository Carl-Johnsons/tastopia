import { RootState } from "@/store";
import { createSelector, createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

export interface searchRecipeState {
  tags: SelectedTag[];
  keyword: string;
}

const initialState: searchRecipeState = {
  tags: [],
  keyword: ""
};

type SaveSearchDataAction = {
  type: string;
  payload: Partial<searchRecipeState>;
};

const selectSearchRecipe = (state: RootState) => state.searchRecipe;

export const selectSearchKeyword = createSelector(
  [selectSearchRecipe],
  searchRecipe => searchRecipe.keyword
);

export const selectSearchTags = createSelector(
  [selectSearchRecipe],
  searchRecipe => searchRecipe.tags
);

export const selectSearchTagCodes = createSelector([selectSearchTags], tags =>
  tags.map(tag => tag.code)
);

export const searchRecipeSlice = createSlice({
  name: "searchRecipe",
  initialState,
  reducers: {
    saveSearchData: (state, action: SaveSearchDataAction) => {
      Object.assign(state, action.payload);
    },
    addKeyword: (state, action: PayloadAction<string>) => {
      state.keyword = action.payload;
    },
    addTagValue: (state, action: PayloadAction<SelectedTag>) => {
      if (!state.tags.some(tag => tag.code === action.payload.code)) {
        state.tags.push(action.payload);
      }
    },
    removeTagValue: (state, action: PayloadAction<string>) => {
      state.tags = state.tags.filter(tag => tag.code !== action.payload);
    },
    clearTagValue: state => {
      state.tags = [];
    }
  }
});

export const { saveSearchData, addKeyword, addTagValue, removeTagValue, clearTagValue } =
  searchRecipeSlice.actions;

export default searchRecipeSlice.reducer;
