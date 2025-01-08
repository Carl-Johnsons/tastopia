import { Stack } from "expo-router";

export default function ModalLayout() {
  return (
    <Stack
      screenOptions={{
        headerShown: false,
        animation: "slide_from_bottom"
      }}
    >
      <Stack.Screen
        name='AddComment'
        options={{
          presentation: "modal"
        }}
      />
      <Stack.Screen
        name='CreateRecipe'
        options={{
          presentation: "modal"
        }}
      />
    </Stack>
  );
}
