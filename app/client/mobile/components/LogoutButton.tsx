import { router } from "expo-router";
import { Text } from "react-native";
import Protected from "./Protected";
import Button from "./Button";
import { persistor } from "@/store";
import { useBounce } from "@/hooks";
import { useTranslation } from "react-i18next";

export const LogoutButton = () => {
  const { t } = useTranslation("menu");
  const { animate, animatedStyles } = useBounce();
  const logout = async () => {
    animate();
    await persistor.purge();
    router.replace("/welcome");
  };

  return (
    <Protected excludedRoles={[]}>
      <Button
        className='rounded-lg border border-gray-300 py-2.5'
        onPress={logout}
        style={[animatedStyles]}
      >
        <Text className='text-black_white text-center font-sans text-sm'>
          {t("logout")}
        </Text>
      </Button>
    </Protected>
  );
};

export default LogoutButton;
