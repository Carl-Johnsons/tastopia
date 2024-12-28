import React from "react";
import { Stack } from "expo-router";
import AuthProvider from "@/components/AuthProvider";

const AuthLayout = () => {
  return (
    <AuthProvider>
      <Stack screenOptions={{ headerShown: false }} />
    </AuthProvider>
  );
};

export default AuthLayout;
