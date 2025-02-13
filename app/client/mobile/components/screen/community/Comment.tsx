import { Image } from "expo-image";
import { router } from "expo-router";
import { Text, View, TouchableWithoutFeedback } from "react-native";

type CommentProps = {
  accountId: string;
  avatarUrl: string;
  displayName: string;
  content: string;
};

const Comment = ({ accountId, avatarUrl, displayName, content }: CommentProps) => {
  const handleTouchUser = () => {
    router.push({
      pathname: "/(protected)/user/[id]",
      params: { id: accountId }
    });
  };
  return (
    <View className='flex-row gap-3'>
      <TouchableWithoutFeedback onPress={handleTouchUser}>
        <Image
          source={avatarUrl}
          style={{ width: 26, height: 26, borderRadius: 100 }}
        />
      </TouchableWithoutFeedback>
      <View className='w-full max-w-[90%] flex-col gap-1'>
        <TouchableWithoutFeedback onPress={handleTouchUser}>
          <Text className='text-black_white paragraph-regular'>{displayName}</Text>
        </TouchableWithoutFeedback>
        <Text className='text-black_white paragraph-regular'>{content}</Text>
      </View>
    </View>
  );
};

export default Comment;
