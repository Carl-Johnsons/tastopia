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
        name='termOfServices'
        options={{
          presentation: "modal"
        }}
      />
    </Stack>
  );
}
