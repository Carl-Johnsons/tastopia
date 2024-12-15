import { persistor } from "@/store";
import { fetchApi } from "@/utils/fetch";
import { router } from "expo-router";
import { Button } from "react-native";

export const LogoutButton = () => {
  const logout = async () => {
    await fetchApi("/api/users/logout", {
      method: "POST"
    });
    await persistor.purge();
    router.navigate("/sign-in");
  };

  return (
    <Button
      title='Logout'
      onPress={() => logout()}
    />
  );
};

export default LogoutButton;
