import Vote from "./Vote";
import { Ionicons, Feather } from "@expo/vector-icons";
import { View, Text, Image, TouchableWithoutFeedback } from "react-native";
import { useRouter } from "expo-router";

const Recipe = ({
  id,
  authorId,
  recipeImgUrl,
  title,
  description,
  authorDisplayName,
  authorAvtUrl,
  voteDiff,
  numberOfComment
}: RecipeType) => {
  const router = useRouter();
  const handleOnPress = () => {
    router.push({
      pathname: "/(protected)/community/[id]",
      params: { id }
    });
  };
  const handleTouchMenu = () => {};

  return (
    <TouchableWithoutFeedback onPress={handleOnPress}>
      <View className='bg-white_black rounded-3xl pb-4'>
        <View className='flex-between flex-row px-4 py-2'>
          {authorId && authorDisplayName && authorAvtUrl && (
            <TouchableWithoutFeedback
              onPress={() => {
                console.log("go to user detail");
              }}
            >
              <View className='flex-center flex-row gap-2'>
                <Image
                  source={{ uri: authorAvtUrl }}
                  className='size-[30px] rounded-full'
                />
                <Text className='paragraph-medium'>{authorDisplayName}</Text>
              </View>
            </TouchableWithoutFeedback>
          )}

          <TouchableWithoutFeedback onPress={handleTouchMenu}>
            <Feather
              name='more-horizontal'
              size={24}
              color='black'
            />
          </TouchableWithoutFeedback>
        </View>
        <View className='flex gap-3'>
          <Image
            source={{ uri: recipeImgUrl }}
            style={{ width: "100%", height: 240, borderRadius: 10 }}
          />

          <View className='gap-3 px-4'>
            <View className='gap-1'>
              <Text
                numberOfLines={1}
                ellipsizeMode='tail'
                className='font-bold text-2xl'
              >
                {title}
              </Text>
              <Text
                numberOfLines={4}
                ellipsizeMode='tail'
                className='body-regular'
              >
                {description}
              </Text>
            </View>

            <View className='flex-start flex-row gap-2'>
              <Vote voteDiff={voteDiff} />

              <TouchableWithoutFeedback>
                <View className='flex-center flex-row gap-2 rounded-3xl border-[0.5px] border-gray-300 px-3 py-2.5'>
                  <Ionicons
                    name='chatbubble-outline'
                    size={20}
                    color='black'
                  />
                  <Text>{numberOfComment}</Text>
                </View>
              </TouchableWithoutFeedback>
            </View>
          </View>
        </View>
      </View>
    </TouchableWithoutFeedback>
  );
};

export default Recipe;
