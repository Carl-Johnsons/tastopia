import Button from "@/components/Button";
import { colors } from "@/constants/colors";
import { CloseIcon } from "@/constants/icons";
import useUpdateProfile from "@/hooks/components/screen/updateProfile/useUpdateProfile";
import useColorizer from "@/hooks/useColorizer";
import { selectUpdateProfile } from "@/slices/menu/profile/updateProfileForm.slice";
import { router } from "expo-router";
import { useTranslation } from "react-i18next";
import { ActivityIndicator, Alert, Text, View } from "react-native";

export default function Header() {
  const { t } = useTranslation("updateProfile");
  const { c } = useColorizer();
  const { black, white, primary } = colors;
  const { isLoading } = selectUpdateProfile();
  const { triggerSubmit } = useUpdateProfile();

  const handleSubmit = () => {
    console.debug("triggerSubmit from Header status:", triggerSubmit);

    if (triggerSubmit) {
      console.log("Trigger submit");
      triggerSubmit();
      console.log("Done submit");
    } else {
      Alert.alert("Error", "An error has occured. Please try again later.");
    }
  };

  return (
    <View className='bg-white_black relative h-[7.4vh] flex-row items-center justify-between px-6'>
      <CloseIcon
        color={c(black.DEFAULT, white.DEFAULT)}
        width={28}
        height={28}
        onPress={router.back}
      />
      <Text
        className='text-black_white font-medium text-xl'
      >
        {t("updateProfile")}
      </Text>
      <Button
        onPress={handleSubmit}
        isLoading={isLoading}
        style={{
          width: 42
        }}
        spinner={
          <ActivityIndicator
            animating={isLoading}
            color={primary}
          />
        }
      >
        <Text className='font-medium text-xl text-primary'>{t("save")}</Text>
      </Button>
    </View>
  );
}
