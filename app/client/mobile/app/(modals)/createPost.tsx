import { SafeAreaView, Text, View } from "react-native";

const CreatePost = () => {
  return (
    <SafeAreaView>
      <View className={`size-full flex-col bg-black px-3`}>
        <Text className={`py-3 text-center text-2xl text-white`}>Create Post</Text>
      </View>
    </SafeAreaView>
  );
};

export default CreatePost;
