import { useUpdateUser } from "@/api/user";
import Header from "@/components/screen/updateProfile/Header";
import ImageChangingSection from "@/components/screen/updateProfile/ImageChangingSection";
import UpdateProfileForm from "@/components/screen/updateProfile/UpadteProfileForm";
import UpdateProfileProvider from "@/components/screen/updateProfile/UpdateProfileProvider";
import useSyncUser from "@/hooks/user/useSyncUser";
import { saveUpdateProfileData } from "@/slices/menu/profile/updateProfileForm.slice";
import { useAppDispatch } from "@/store/hooks";
import { stringify } from "@/utils/debug";
import { dismissKeyboard } from "@/utils/keyboard";
import { router } from "expo-router";
import { useCallback, useState } from "react";
import {
  Alert,
  GestureResponderEvent,
  Keyboard,
  Pressable,
  SafeAreaView,
  View
} from "react-native";
import { useQueryClient } from "react-query";

export default function UpdateProfile() {
  const { mutateAsync: updateProfile } = useUpdateUser();
  const { fetch: fetchUser } = useSyncUser();
  const queryClient = useQueryClient();
  const dispatch = useAppDispatch();

  const [triggerSubmit, setTriggerSubmit] = useState<() => void>();
  const [onChangeGenderValue, setOnChangeGenderValue] =
    useState<(newValue: string) => void>();

  const contextValue = {
    triggerSubmit,
    setTriggerSubmit,
    onChangeGenderValue,
    setOnChangeGenderValue
  };

  /**
   * Update the user's information and then fetch the new information from the server.
   */
  const updateUser = async (data: IUpdateUserDTO) => {
    console.log("data", stringify(data));
    dispatch(saveUpdateProfileData({ isLoading: true }));

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
      onSettled: () => {
        dispatch(saveUpdateProfileData({ isLoading: false }));
      }
    });
  };

  return (
    <UpdateProfileProvider value={contextValue}>
      <SafeAreaView>
        <Pressable onPress={dismissKeyboard}>
          <View className='bg-white_black flex h-full'>
            <Header />
            <ImageChangingSection />
            <UpdateProfileForm
              className='mt-10 px-4'
              onSubmit={updateUser}
            />
          </View>
        </Pressable>
      </SafeAreaView>
    </UpdateProfileProvider>
  );
}
