import { useUpdateUser } from "@/api/user";
import Header from "@/components/screen/updateProfile/Header";
import ImageChangingSection from "@/components/screen/updateProfile/ImageChangingSection";
import UpdateProfileForm from "@/components/screen/updateProfile/UpadteProfileForm";
import UpdateProfileProvider from "@/components/screen/updateProfile/UpdateProfileProvider";
import { IUpdateUserDTO } from "@/generated/interfaces/user.interface";
import { useErrorHandler } from "@/hooks/useErrorHandler";
import useSyncUser from "@/hooks/user/useSyncUser";
import { saveUpdateProfileData } from "@/slices/menu/profile/updateProfileForm.slice";
import { selectUserId } from "@/slices/user.slice";
import { useAppDispatch } from "@/store/hooks";
import { stringify } from "@/utils/debug";
import { router } from "expo-router";
import { useCallback, useState } from "react";
import { Alert, SafeAreaView, ScrollView, View } from "react-native";
import { useQueryClient } from "react-query";

export default function UpdateProfile() {
  const { mutateAsync: updateProfile } = useUpdateUser();
  const { fetch: fetchUser } = useSyncUser();
  const queryClient = useQueryClient();
  const dispatch = useAppDispatch();
  const userId = selectUserId();

  const { handleError } = useErrorHandler();
  const [triggerSubmit, setTriggerSubmit] = useState<() => void>();
  const [onChangeGenderValue, setOnChangeGenderValue] =
    useState<(newValue: string) => void>();

  const contextValue = {
    triggerSubmit,
    setTriggerSubmit,
    onChangeGenderValue,
    setOnChangeGenderValue
  };

  const goToProfile = useCallback(() => {
    router.navigate({
      pathname: "/(protected)/user/[id]",
      params: { id: userId ?? "" }
    });
  }, [router, userId]);

  /**
   * Update the user's information and then fetch the new information from the server.
   */
  const updateUser = async (data: IUpdateUserDTO) => {
    console.log("data", stringify(data));
    dispatch(saveUpdateProfileData({ isLoading: true }));

    await updateProfile(data, {
      onSuccess: async () => {
        await queryClient.invalidateQueries({ queryKey: "getUserDetails" });
        await queryClient.invalidateQueries({ queryKey: ["user", userId] });
        await fetchUser();

        goToProfile();
        Alert.alert("Success", "Update profile successfully.");
      },
      onError: error => handleError(error),
      onSettled: () => {
        dispatch(saveUpdateProfileData({ isLoading: false }));
      }
    });
  };

  return (
    <UpdateProfileProvider value={contextValue}>
      <SafeAreaView>
        <View className='bg-white_black flex h-full'>
          <Header />
          <ScrollView>
            <ImageChangingSection />
            <UpdateProfileForm
              className='mt-10 px-4'
              onSubmit={updateUser}
            />
          </ScrollView>
        </View>
      </SafeAreaView>
    </UpdateProfileProvider>
  );
}
