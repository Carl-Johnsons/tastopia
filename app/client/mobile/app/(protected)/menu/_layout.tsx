import { Stack } from "expo-router";

export default function MenuLayout() {
  return (
      <Stack
        screenOptions={{
          headerShown: false
        }}
      >
        <Stack.Screen name='index' />
        <Stack.Screen name='bookmark' />
        <Stack.Screen name='history' />
        <Stack.Screen name='deleted-recipe' />
      </Stack>
  );
}
