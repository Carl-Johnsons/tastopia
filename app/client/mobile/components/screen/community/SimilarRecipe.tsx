import { router } from "expo-router";
import { Text, View, Image, TouchableWithoutFeedback } from "react-native";

type SimilarRecipeProps = {
  recipeId: string;
  imageUrl: string;
  title: string;
};

const SimilarRecipe = ({ recipeId, imageUrl, title }: SimilarRecipeProps) => {
  const handleSelect = () => {
    router.push({
      pathname: "/(protected)/community/[id]",
      params: { id: recipeId }
    });
  };

  return (
    <TouchableWithoutFeedback onPress={handleSelect}>
      <View className='relative gap-3 rounded-xl'>
        <Image
          source={{ uri: imageUrl }}
          className='h-[120px] w-full rounded-xl'
          resizeMode='cover'
        />
        <Text className='body-semibold text-black_white uppercase'>{title}</Text>
      </View>
    </TouchableWithoutFeedback>
  );
};

export default SimilarRecipe;
