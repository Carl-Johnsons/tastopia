import { View } from "react-native";

function Skeleton({ className }: { className: string }) {
  return <View className={`animate-pulse rounded-md bg-primary/10 ${className}`} />;
}

export { Skeleton };
