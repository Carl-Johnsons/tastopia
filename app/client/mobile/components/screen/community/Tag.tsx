import { Dispatch, SetStateAction } from "react";
import { Text, View, Image, TouchableWithoutFeedback } from "react-native";
import { AntDesign } from "@expo/vector-icons";
import Animated, {
  useAnimatedStyle,
  withSpring,
  withTiming
} from "react-native-reanimated";

type TagProps = {
  value: string;
  code: string;
  imageUrl: string;
  selectedTags: SelectedTag[];
  setSelectedTags: Dispatch<SetStateAction<SelectedTag[]>>;
};

const Tag = ({ value, code, imageUrl, selectedTags, setSelectedTags }: TagProps) => {
  const isSelected = selectedTags.find(selectedTag => selectedTag.code === code);

  const handleSelect = () => {
    if (isSelected) {
      setSelectedTags(prev => prev.filter(tag => tag.code !== code));
    } else {
      setSelectedTags(prev => [...prev, { code, value }]);
    }
  };

  const checkmarkStyle = useAnimatedStyle(() => {
    return {
      transform: [{ scale: withSpring(isSelected ? 1 : 0) }],
      opacity: withTiming(isSelected ? 1 : 0)
    };
  });

  return (
    <TouchableWithoutFeedback onPress={handleSelect}>
      <View className='relative rounded-xl'>
        <Image
          source={{ uri: imageUrl }}
          className='h-[120px] w-full rounded-xl'
          resizeMode='cover'
        />
        <View className='absolute inset-0 rounded-xl bg-black/30' />

        {isSelected && (
          <View className='absolute inset-0 flex-col items-center justify-center rounded-xl bg-black/60'>
            <View className='rounded-full p-3'>
              <Animated.View style={[checkmarkStyle]}>
                <AntDesign
                  name='check'
                  size={44}
                  color='#22C55E'
                />
              </Animated.View>
            </View>
          </View>
        )}

        <Text className='body-semibold absolute bottom-2 left-2 uppercase text-white'>
          {value}
        </Text>
      </View>
    </TouchableWithoutFeedback>
  );
};

export default Tag;
