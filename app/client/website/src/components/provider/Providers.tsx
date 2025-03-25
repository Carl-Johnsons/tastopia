"use client";

import { ThemeProvider } from "@/context/ThemeProvider";
import { SessionProvider } from "next-auth/react";
import { isServer, QueryClient, QueryClientProvider } from "@tanstack/react-query";
import StoreProvider from "./StoreProvider";
import { ReactNode } from "react";
import ToastProvider from "./ToastProvider";
import { SignalRHubProvider } from "./SignalRProvider";

function makeQueryClient() {
  return new QueryClient({
    defaultOptions: {
      queries: {
        staleTime: 60 * 1000
      }
    }
  });
}

let browserQueryClient: QueryClient | undefined;

function getQueryClient() {
  if (isServer) {
    return makeQueryClient();
  } else {
    if (!browserQueryClient) browserQueryClient = makeQueryClient();
    return browserQueryClient;
  }
}

/**
 * The main provider for the app.
 *
 * Refer to the [React Query docs](https://tanstack.com/query/latest/docs/framework/react/guides/advanced-ssr) for more information about the react query client initializing logic.
 */
export default function Providers({ children }: { children: ReactNode }) {
  const queryClient = getQueryClient();

  return (
    <StoreProvider>
      <SessionProvider>
        <QueryClientProvider client={queryClient}>
          <SignalRHubProvider>
            <ThemeProvider>
              <ToastProvider />
              {children}
            </ThemeProvider>
          </SignalRHubProvider>
        </QueryClientProvider>
      </SessionProvider>
    </StoreProvider>
  );
}
