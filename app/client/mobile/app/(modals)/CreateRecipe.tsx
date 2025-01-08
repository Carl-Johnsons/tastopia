import { SafeAreaView, Text, View } from "react-native";

const CreateRecipe = () => {
  return (
    <SafeAreaView>
      <View className={`size-full flex-col bg-black px-3`}>
        <Text className={`py-3 text-center text-2xl text-white`}>Create Recipe</Text>
      </View>
    </SafeAreaView>
  );
};

export default CreateRecipe;
