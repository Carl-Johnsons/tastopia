"use client";

import { AppStore, makeStore } from "@/store";
import { ReactNode, useRef } from "react";
import { Provider } from "react-redux";

/**
 * Provides the Redux store to the app. Refer to the [Redux's documentation](https://redux.js.org/usage/nextjs) on why it was implemented this way.
 */
export default function StoreProvider({ children }: { children: ReactNode }) {
  const storeRef = useRef<AppStore | null>(null);

  if (!storeRef.current) {
    // Create the store instance the first time this renders
    storeRef.current = makeStore();
  }

  return <Provider store={storeRef.current}>{children}</Provider>;
}
