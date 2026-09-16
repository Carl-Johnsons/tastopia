import { getRandomBytes } from "expo-crypto";

export function generateTraceparent(): string {
  const bytes = getRandomBytes(24);
  const hex = Array.from(bytes, b => b.toString(16).padStart(2, "00")).join("");
  const traceId = hex.slice(0, 32);
  const parentId = hex.slice(32, 48);
  return `00-${traceId}-${parentId}-01`;
}
