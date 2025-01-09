import { Image, Text, View } from "react-native";

interface IngredientProps {
  name: string;
  image: string;
}

const Ingredient = ({ name, image }: IngredientProps) => {
  return (
    <View className='relative rounded-xl bg-black-100/10 shadow-sm'>
      <Image
        source={require("../../../assets/images/recipe.png")}
        className='h-[120px] w-full rounded-xl'
        resizeMode='cover'
      />
      <Text className='absolute bottom-2 left-2 font-medium uppercase text-black'>
        {name}
      </Text>
    </View>
  );
};

export default Ingredient;
