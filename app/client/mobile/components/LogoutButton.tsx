import { persistor } from "@/store";
import { protectedAxiosInstance } from "@/constants/host";
import { router } from "expo-router";
import { Platform, Text } from "react-native";
import { useBounce } from "@/hooks";
import { useTranslation } from "react-i18next";
import Button from "./Button";
import Protected from "./Protected";
import { ROLE, selectRole } from "@/slices/auth.slice";
import { selectPushToken } from "@/slices/notification.slice";

export const LogoutButton = () => {
  const role = selectRole();
  const { t } = useTranslation("menu");
  const { animate, animatedStyles } = useBounce();
  const pushNotificationToken = selectPushToken();

  const logout = async () => {
    animate();

    try {
      if (role !== ROLE.GUEST && pushNotificationToken) {
        if (Platform.OS === "android")
          await protectedAxiosInstance.delete("api/notification/expo-push-token/android");
        else if (Platform.OS === "ios")
          await protectedAxiosInstance.delete("api/notification/expo-push-token/ios");
      }
    } catch (error) {
      console.log("error", error);
    } finally {
      await persistor.purge();
      router.replace("/welcome");
    }
  };

  return (
    <Protected excludedRoles={[]}>
      <Button
        className='rounded-lg border border-gray-300 py-2.5'
        onPress={logout}
        style={[animatedStyles]}
      >
        <Text className='text-black_white text-center font-sans text-lg'>
          {role === ROLE.GUEST ? t("backToLogin") : t("logout")}
        </Text>
      </Button>
    </Protected>
  );
};

export default LogoutButton;
