import { combineReducers, configureStore } from "@reduxjs/toolkit";
import authReducer from "../slices/auth.slice";
import userReducer from "../slices/user.slice";

const rootRecuder = combineReducers({
  auth: authReducer,
  user: userReducer,
});

export const makeStore = () => {
  return configureStore({
    reducer: rootRecuder,
  });
};

export type AppStore = ReturnType<typeof makeStore>;
export type RootState = ReturnType<AppStore["getState"]>;
export type AppDispatch = AppStore["dispatch"];
