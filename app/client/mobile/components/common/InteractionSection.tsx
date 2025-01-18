import { TouchableWithoutFeedback, View, Text } from "react-native";
import Vote from "./Vote";
import { Ionicons } from "@expo/vector-icons";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";

const InteractionSection = ({
  voteDiff,
  numberOfComment,
  handleOnPress
}: {
  voteDiff: number | undefined;
  numberOfComment: number | undefined;
  handleOnPress: () => void;
}) => {
  const { c } = useColorizer();
  const { black, white } = colors;

  return (
    <View className='flex-start flex-row gap-2'>
      {voteDiff !== undefined && <Vote voteDiff={voteDiff} />}

      {numberOfComment !== undefined && (
        <TouchableWithoutFeedback onPress={handleOnPress}>
          <View className='flex-center flex-row gap-2 rounded-3xl border-[0.5px] border-gray-300 px-3 py-2.5'>
            <Ionicons
              name='chatbubble-outline'
              size={20}
              color={c(black.DEFAULT, white.DEFAULT)}
            />
            <Text className={`text-black_white mx-2 text-center`}>{numberOfComment}</Text>
          </View>
        </TouchableWithoutFeedback>
      )}
    </View>
  );
};

export default InteractionSection;
