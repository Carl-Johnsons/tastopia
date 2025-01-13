import { TouchableWithoutFeedback, View, Text } from "react-native";
import Vote from "./Vote";
import { Ionicons } from "@expo/vector-icons";

const InteractionSection = ({
  voteDiff,
  numberOfComment,
  handleOnPress
}: {
  voteDiff: number | undefined;
  numberOfComment: number | undefined;
  handleOnPress: () => void;
}) => {
  return (
    <View className='flex-start flex-row gap-2'>
      {voteDiff !== undefined && <Vote voteDiff={voteDiff} />}

      {numberOfComment !== undefined && (
        <TouchableWithoutFeedback onPress={handleOnPress}>
          <View className='flex-center flex-row gap-2 rounded-3xl border-[0.5px] border-gray-300 px-3 py-2.5'>
            <Ionicons
              name='chatbubble-outline'
              size={20}
              color='black'
            />
            <Text>{numberOfComment}</Text>
          </View>
        </TouchableWithoutFeedback>
      )}
    </View>
  );
};

export default InteractionSection;
