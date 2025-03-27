import { combineReducers, configureStore } from "@reduxjs/toolkit";
import authReducer from "../slices/auth.slice";
import userReducer from "../slices/user.slice";
import adminReducer from "@/slices/admin.slice";

const rootRecuder = combineReducers({
  auth: authReducer,
  user: userReducer,
  admin: adminReducer
});

export const makeStore = () => {
  return configureStore({
    reducer: rootRecuder,
  });
};

export type AppStore = ReturnType<typeof makeStore>;
export type RootState = ReturnType<AppStore["getState"]>;
export type AppDispatch = AppStore["dispatch"];
