import { Stack } from "expo-router";

export default function CommunityLayout() {
  return (
    <Stack
      screenOptions={{
        headerShown: false
      }}
      initialRouteName='index'
    >
      <Stack.Screen name='index' />
      <Stack.Screen name='[id]' />
    </Stack>
  );
}
