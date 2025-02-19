import {
  persistStore,
  persistReducer,
  FLUSH,
  REHYDRATE,
  PAUSE,
  PERSIST,
  PURGE,
  REGISTER
} from "redux-persist";
import { configureStore, combineReducers } from "@reduxjs/toolkit";
import AsyncStorage from "@react-native-async-storage/async-storage";

import userReducer from "@/slices/user.slice";
import authReducer from "@/slices/auth.slice";
import settingReducer from "@/slices/setting.slice";
import searchRecipeReducer from "@/slices/searchRecipe.slice";
import updateProfileReducer from "@/slices/menu/profile/updateProfileForm.slice";

const persistConfig = {
  key: "root",
  storage: AsyncStorage,
  whitelist: ["auth", "user"]
};

const rootReducer = combineReducers({
  auth: authReducer,
  user: userReducer,
  setting: settingReducer,
  searchRecipe: searchRecipeReducer,
  updateProfile: updateProfileReducer
});

const persistedReducer = persistReducer(persistConfig, rootReducer);

export const store = configureStore({
  reducer: persistedReducer,
  middleware: getDefaultMiddleware =>
    getDefaultMiddleware({
      serializableCheck: {
        ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER]
      }
    })
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const persistor = persistStore(store);
