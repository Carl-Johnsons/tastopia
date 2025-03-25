"use client";

import { SignalRHubContext } from "@/components/provider/SignalRProvider";
import React, { useContext } from "react";

const SignalR = () => {
  const test = useContext(SignalRHubContext);
  test?.startConnection();
  return <></>;
};

export default SignalR;
