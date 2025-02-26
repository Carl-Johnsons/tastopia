import { TouchableWithoutFeedback, View, Text } from "react-native";
import Vote from "./Vote";
import { Ionicons } from "@expo/vector-icons";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { VoteType } from "@/constants/recipe";

const InteractionSection = ({
  recipeId,
  vote,
  voteDiff,
  numberOfComment,
  handleOnPress,
  containerClassName
}: {
  recipeId: string;
  vote?: VoteType;
  voteDiff: number | undefined;
  numberOfComment?: number;
  handleOnPress?: () => void;
  containerClassName?: string;
}) => {
  const { black, white } = colors;
  const { c } = useColorizer();

  return (
    <View className={`flex-row gap-2 ${containerClassName}`}>
      {voteDiff !== undefined && vote !== undefined && (
        <Vote
          recipeId={recipeId}
          vote={vote}
          voteDiff={voteDiff}
        />
      )}

      {numberOfComment !== undefined && (
        <TouchableWithoutFeedback onPress={handleOnPress}>
          <View className='flex-center flex-row gap-2 rounded-3xl border-[0.5px] border-gray-300 px-3 py-2'>
            <Ionicons
              name='chatbubble-outline'
              size={16}
              color={c(black.DEFAULT, white.DEFAULT)}
            />
            <Text className={`text-black_white mr-2 text-center`}>{numberOfComment}</Text>
          </View>
        </TouchableWithoutFeedback>
      )}
    </View>
  );
};

export default InteractionSection;
