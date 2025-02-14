import { colors } from "@/constants/colors";
import { DotIcon } from "@/constants/icons";
import useColorizer from "@/hooks/useColorizer";
import { formatDate } from "@/utils/format-date";
import { Text, TouchableWithoutFeedback, View } from "react-native";

const Comment = ({
  displayName,
  content,
  recipeTitle,
  createdAt,
  isActive
}: IAccountRecipeCommentResponse) => {
  const { gray } = colors;

  return (
    isActive && (
      <TouchableWithoutFeedback>
        <View className='bg-white_black100 gap-2'>
          <Text className='text-black_white text-xl'>{recipeTitle}</Text>
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
