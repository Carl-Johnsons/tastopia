import { router } from "expo-router";
import { Button } from "react-native";
import { useDispatch } from "react-redux";
import { ROLE, logout as logoutAction } from "@/slices/auth.slice";
import Protected from "./Protected";

export const LogoutButton = () => {
  const dispatch = useDispatch();

  const logout = async () => {
    dispatch(logoutAction());
    router.replace("/welcome");
  };

  return (
    <Protected excludedRoles={[ROLE.GUEST]}>
      <Button
        title='Logout'
        onPress={logout}
      />
    </Protected>
  );
};

export default LogoutButton;
