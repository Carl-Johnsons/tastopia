import Header from "@/components/screen/updateProfile/Header";
import ImageChangingSection from "@/components/screen/updateProfile/ImageChangingSection";
import UpdateProfileForm from "@/components/screen/updateProfile/UpadteProfileForm";
import { SafeAreaView, View } from "react-native";

export default function UpdateProfile() {
  return (
    <SafeAreaView>
      <View className='flex h-full bg-white_black'>
        <Header />
        <ImageChangingSection />
        <UpdateProfileForm className="px-4 mt-10" />
      </View>
    </SafeAreaView>
  );
}
