import { useAppSelector } from "@/store/hooks";
import { createSlice } from "@reduxjs/toolkit";
import { PURGE } from "redux-persist";

export type NotificationType = "Community" | "System";

export interface NotificationState {
  type: NotificationType | null;
  pushToken: string | null;
}

export type SaveNotificationDataAction = {
  type: string;
  payload: Partial<NotificationState>;
};

const initialState: NotificationState = {
  type: "Community",
  pushToken: null
};

export const selectNotificationType = () => useAppSelector(state => state.notification.type);
export const selectPushToken = () => useAppSelector(state => state.notification.pushToken);

export const NotificationSlice = createSlice({
  name: "notification",
  initialState,
  reducers: {
    saveNotificationData: (state, action: SaveNotificationDataAction) => {
      Object.assign(state, action.payload);
    },
  },
  extraReducers: builder => {
    builder.addCase(PURGE, () => {
      return initialState;
    });
  }
});

export const { saveNotificationData } = NotificationSlice.actions;

export default NotificationSlice.reducer;
