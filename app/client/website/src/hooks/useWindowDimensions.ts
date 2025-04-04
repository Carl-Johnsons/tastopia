"use client";

import { useState, useEffect } from "react";

function getWindowDimensions() {
  const isClient = typeof window !== "undefined";

  if (!isClient) {
    return {
      width: 0,
      height: 0
    };
  }

  const { innerWidth: width, innerHeight: height } = window;
  return {
    width,
    height
  };
}

export default function useWindowDimensions() {
  const [windowDimensions, setWindowDimensions] = useState({ width: 0, height: 0 });

  useEffect(() => {
    function handleResize() {
      setWindowDimensions(getWindowDimensions());
    }

    handleResize();
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  return windowDimensions;
}
