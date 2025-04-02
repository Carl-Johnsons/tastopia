import { Platform, View } from "react-native";
import CircleBg from "@/components/CircleBg";
import AddIdentifierForm from "@/components/screen/verify/AddIdentifierForm";

const AddIdentifier = () => {
  const isAndroid = Platform.OS === "android";

  return (
    <View className='bg-white_black200 relative h-full'>
      <CircleBg />

      <View
        className={`absolute ${isAndroid ? "top-[5%]" : "top-[6%]"} flex w-full justify-center gap-[4vh] px-4`}
      >
        <AddIdentifierForm className='mt-[5vh]' />
      </View>
    </View>
  );
};

export default AddIdentifier;
