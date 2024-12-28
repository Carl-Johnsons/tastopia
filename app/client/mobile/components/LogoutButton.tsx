import { persistor } from "@/store";
import { router } from "expo-router";
import { Button } from "react-native";

export const LogoutButton = () => {
  const logout = async () => {
    await persistor.purge();
    router.replace("/welcome");
  };

  return (
    <Button
      title='Logout'
      onPress={logout}
    />
  );
};

export default LogoutButton;
