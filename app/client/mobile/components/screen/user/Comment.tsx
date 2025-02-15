import { colors } from "@/constants/colors";
import { DotIcon } from "@/constants/icons";
import useColorizer from "@/hooks/useColorizer";
import { formatDate } from "@/utils/format-date";
import { Feather } from "@expo/vector-icons";
import { BottomSheetMethods } from "@gorhom/bottom-sheet/lib/typescript/types";
import { router } from "expo-router";
import { Dispatch, RefObject, SetStateAction } from "react";
import { Text, TouchableWithoutFeedback, View } from "react-native";

const Comment = ({
  id,
  recipeId,
  accountId,
  displayName,
  content,
  recipeTitle,
  createdAt,
  isActive,
  bottomSheetCommentRef,
  setCurrentCommentId,
  setCurrentCommentAuthorId
}: IAccountRecipeCommentResponse & {
  bottomSheetCommentRef: RefObject<BottomSheetMethods>;
  setCurrentCommentId: Dispatch<SetStateAction<string>>;
  setCurrentCommentAuthorId: Dispatch<SetStateAction<string>>;
}) => {
  const { c } = useColorizer();
  const { black, white, gray } = colors;

  const handleOnPres = () => {
    router.push({
      pathname: "/(protected)/community/[id]",
      params: { id: recipeId }
    });
  };

  const handleTouchMenu = () => {
    setCurrentCommentId(id);
    setCurrentCommentAuthorId(accountId);
    bottomSheetCommentRef.current?.expand();
  };

  return (
    isActive && (
      <TouchableWithoutFeedback onPress={handleOnPres}>
        <View className='bg-white_black100 w-[94vw] gap-2'>
          <View className='flex-between flex-row'>
            <Text className='text-black_white text-xl'>{recipeTitle}</Text>
            <TouchableWithoutFeedback onPress={handleTouchMenu}>
              <View>
                <Feather
                  name='more-horizontal'
                  size={24}
                  color={c(black.DEFAULT, white.DEFAULT)}
                />
              </View>
            </TouchableWithoutFeedback>
          </View>
          <View className='flex-row items-center gap-2'>
            <Text className='text-md font-secondary-roman text-gray-500'>
              {displayName}
            </Text>
            <DotIcon
              width={4}
              height={4}
              color={gray["500"]}
            />
            <Text className='text-md font-secondary-roman text-gray-500'>
              {formatDate(createdAt)}
            </Text>
          </View>
          <Text className='text-black_white font-secondary-roman text-lg'>{content}</Text>
        </View>
      </TouchableWithoutFeedback>
    )
  );
};

export default Comment;
