import { router } from "expo-router";
import { Button } from "react-native";
import { useDispatch } from "react-redux";
import { logout as logoutAction } from "@/slices/auth.slice";

export const LogoutButton = () => {
  const dispatch = useDispatch();

  const logout = async () => {
    dispatch(logoutAction());
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
