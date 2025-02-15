import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { Feather } from "@expo/vector-icons";
import { BottomSheetMethods } from "@gorhom/bottom-sheet/lib/typescript/types";
import { Image } from "expo-image";
import { router } from "expo-router";
import { RefObject } from "react";
import { Text, View, TouchableWithoutFeedback } from "react-native";

type CommentProps = {
  accountId: string;
  avatarUrl: string;
  displayName: string;
  content: string;
  commentId: string;
};

const Comment = ({
  accountId,
  avatarUrl,
  displayName,
  content,
  commentId,
  setCurrentCommentAuthorId,
  setCurrentComment,
  bottomSheetRef
}: CommentProps & {
  bottomSheetRef: RefObject<BottomSheetMethods>;
  setCurrentComment: (comment: CommentCustomType) => void;
  setCurrentCommentAuthorId: (id: string) => void;
}) => {
  const { c } = useColorizer();
  const { black, white } = colors;

  const handleTouchUser = () => {
    router.push({
      pathname: "/(protected)/user/[id]",
      params: { id: accountId }
    });
  };

  const handleTouchMenu = () => {
    setCurrentComment({
      id: commentId,
      content: content
    });
    setCurrentCommentAuthorId(accountId);
    bottomSheetRef.current?.expand();
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
        <View className='flex-row items-center justify-between'>
          <TouchableWithoutFeedback onPress={handleTouchUser}>
            <Text className='text-black_white paragraph-regular'>{displayName}</Text>
          </TouchableWithoutFeedback>
          <TouchableWithoutFeedback onPress={handleTouchMenu}>
            <View>
              <Feather
                name='more-horizontal'
                size={20}
                color={c(black.DEFAULT, white.DEFAULT)}
              />
            </View>
          </TouchableWithoutFeedback>
        </View>
        <Text className='text-black_white paragraph-regular'>{content}</Text>
      </View>
    </View>
  );
};

export default Comment;
