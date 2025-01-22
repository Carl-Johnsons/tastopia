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
        name='termAndServices'
        options={{
          presentation: "modal"
        }}
      />
    </Stack>
  );
}
