import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { FontAwesome6 } from "@expo/vector-icons";
import { Text, TouchableWithoutFeedback, View } from "react-native";

type SearchHistoryProps = {
  item: string;
  handleSelectHistory: (item: string) => void;
};

const SearchHistory = ({ item, handleSelectHistory }: SearchHistoryProps) => {
  const { c } = useColorizer();
  const { black, white } = colors;

  return (
    <TouchableWithoutFeedback onPress={() => handleSelectHistory(item)}>
      <View className='flex-row items-center gap-4'>
        <FontAwesome6
          name='clock-rotate-left'
          size={18}
          color={c(black.DEFAULT, white.DEFAULT)}
        />
        <Text className='text-black_white text-lg'>{item}</Text>
      </View>
    </TouchableWithoutFeedback>
  );
};

export default SearchHistory;
