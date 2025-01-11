import { View, Text, TouchableOpacity } from "react-native";
import { AntDesign } from "@expo/vector-icons";
import { useAppDispatch } from "@/store/hooks";
import { globalStyles } from "@/components/common/GlobalStyles";

type SelectedTagProps = {
  code: string;
  value: string;
  onRemove: (code: string) => void;
};

const SelectedTag = ({ code, value, onRemove }: SelectedTagProps) => {
  return (
    <View className='bg-coral-100 flex-row items-center rounded-full px-3 py-1.5'>
      <Text className='text-coral-600 body-semibold mr-2'>{value}</Text>
      <TouchableOpacity
        onPress={() => onRemove(code)}
        hitSlop={{ top: 10, bottom: 10, left: 10, right: 10 }}
      >
        <AntDesign
          name='close'
          size={16}
          color={globalStyles.color.primary}
        />
      </TouchableOpacity>
    </View>
  );
};

export default SelectedTag;
