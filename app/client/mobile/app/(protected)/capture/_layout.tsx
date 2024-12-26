import { Stack } from "expo-router";

export default function CaptureLayout() {
  return (
    <Stack
      screenOptions={{
        headerShown: false
      }}
    >
      <Stack.Screen name='index' />
    </Stack>
  );
}
