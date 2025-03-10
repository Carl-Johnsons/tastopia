import { combineReducers, configureStore } from "@reduxjs/toolkit";
import authReducer from "../slices/auth.slice";

const rootRecuder = combineReducers({
  auth: authReducer,
});

export const makeStore = () => {
  return configureStore({
    reducer: rootRecuder,
  });
};

export type AppStore = ReturnType<typeof makeStore>;
export type RootState = ReturnType<AppStore["getState"]>;
export type AppDispatch = AppStore["dispatch"];
