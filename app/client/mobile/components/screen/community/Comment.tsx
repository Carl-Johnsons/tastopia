import { Image } from "expo-image";
import { Text, View, TouchableWithoutFeedback } from "react-native";

type CommentProps = {
  avatarUrl: string;
  displayName: string;
  content: string;
};

const Comment = ({ avatarUrl, displayName, content }: CommentProps) => {
  const handleTouchUser = () => {};
  return (
    <View className='flex-row gap-3'>
      <TouchableWithoutFeedback onPress={handleTouchUser}>
        <Image
          source={avatarUrl}
          style={{ width: 26, height: 26, borderRadius: 100 }}
        />
      </TouchableWithoutFeedback>
      <View className='w-full max-w-[90%] flex-col gap-2'>
        <TouchableWithoutFeedback onPress={handleTouchUser}>
          <Text className='text-black_white'>{displayName}</Text>
        </TouchableWithoutFeedback>
        <Text className='text-black_white'>{content}</Text>
      </View>
    </View>
  );
};

export default Comment;
