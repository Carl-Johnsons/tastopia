import { useUpdateUser } from "@/api/user";
import { UpdateProfileForm } from "@/components/screen/updateProfile";
import Header from "@/components/screen/updateProfile/Header";
import ImageChangingSection from "@/components/screen/updateProfile/ImageChangingSection";
import UpdateProfileProvider, {
  UpdateProfileContext
} from "@/components/screen/updateProfile/UpdateProfileProvider";
import useSyncUser from "@/hooks/user/useSyncUser";
import { saveUserData } from "@/slices/user.slice";
import { stringify } from "@/utils/debug";
import { router } from "expo-router";
import { useCallback, useEffect, useState } from "react";
import { Alert, SafeAreaView, View } from "react-native";
import { useQueryClient } from "react-query";
import { useDispatch } from "react-redux";

export default function UpdateProfile() {
  const [avatar, setAvatar] = useState<ImageFileType>();
  const [background, setBackground] = useState<ImageFileType>();
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [triggerSubmit, setTriggerSubmit] = useState<() => void>();

  const { mutateAsync: updateProfile } = useUpdateUser();
  const { fetch: fetchUser } = useSyncUser();
  const queryClient = useQueryClient();

  const contextValues: UpdateProfileContext = {
    avatar,
    setAvatar,
    background,
    setBackground,
    triggerSubmit,
    setTriggerSubmit,
    isLoading,
    setIsLoading
  };

  /**
   * Update the user's information and then fetch the new information from the server.
   */
  const updateUser = async (data: IUpdateUserDTO) => {
    console.log("data", stringify(data));
    setIsLoading(true);

    await updateProfile(data, {
      onSuccess: async () => {
        await queryClient.invalidateQueries({ queryKey: "getUserDetails" });
        await fetchUser();

        router.back();
        Alert.alert("Success", "Update profile successfully.");
      },
      onError: error => {
        Alert.alert("Error", error.message);
      },
      onSettled: () => setIsLoading(false)
    });
  };

  return (
    <UpdateProfileProvider value={contextValues}>
      <SafeAreaView>
        <View className='bg-white_black flex h-full'>
          <Header />
          <ImageChangingSection />
          <UpdateProfileForm
            className='mt-10 px-4'
            onSubmit={updateUser}
          />
        </View>
      </SafeAreaView>
    </UpdateProfileProvider>
  );
}
