import { saveAuthData } from "@/slices/auth.slice";
import { persistor, store } from "@/store";
import { router } from "expo-router";

let isClearingSession = false;

/*
 * Clear session on client side without triggering the backend cleaning up route.
 */
export const clearSession = async () => {
  if (isClearingSession) return;
  isClearingSession = true;

  try {
    store.dispatch(
      saveAuthData({
        accessToken: null,
        refreshToken: null,
        idToken: null,
        role: null
      })
    );
    await persistor.purge();
  } catch (error) {
    console.error("Failed to purge session:", error);
  } finally {
    router.replace("/welcome");
    isClearingSession = false;
  }
};
