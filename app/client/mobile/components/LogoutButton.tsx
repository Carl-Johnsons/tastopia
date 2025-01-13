import { router } from "expo-router";
import { Text } from "react-native";
import Protected from "./Protected";
import Button from "./Button";
import { persistor } from "@/store";
import { useBounce } from "@/hooks";

export const LogoutButton = () => {
  const { animate, animatedStyles } = useBounce();
  const logout = async () => {
    animate();
    await persistor.purge();
    router.replace("/welcome");
  };

  return (
    <Protected excludedRoles={[]}>
      <Button
        className='py-2.5 rounded-lg border border-gray-300'
        onPress={logout}
        style={[animatedStyles]}
      >
        <Text className='font-sans text-sm text-center text-black_white'>Log out</Text>
      </Button>
    </Protected>
  );
};

export default LogoutButton;
