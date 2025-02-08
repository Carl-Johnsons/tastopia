import { Stack } from "expo-router";

export default function CommunityLayout() {
  return (
    <Stack
      screenOptions={{
        headerShown: false
      }}
      initialRouteName='index'
    >
      <Stack.Screen
        name='create-recipe'
        options={{
          animation: "slide_from_bottom"
        }}
      />
      <Stack.Screen
        name='update-recipe'
        options={{
          animation: "slide_from_bottom"
        }}
      />
      <Stack.Screen name='index' />
      <Stack.Screen name='[id]' />
      <Stack.Screen name='cooking-mode' />
    </Stack>
  );
}
