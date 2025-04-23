import LoadingImage from "@/components/common/LoadingImage";
import { RecipeType } from "@/types/recipe";
import { useRouter } from "expo-router";
import { View, Text, Image, TouchableWithoutFeedback } from "react-native";

const SearchRecipeResult = ({
  id,
  authorId,
  recipeImgUrl,
  title,
  description,
  authorDisplayName,
  authorAvtUrl,
  handleSelectSearchResult
}: RecipeType & {
  handleSelectSearchResult: () => void;
}) => {
  const router = useRouter();
  const handleOnPress = () => {
    handleSelectSearchResult();
    router.push({
      pathname: "/(protected)/community/[id]",
      params: { id, authorId }
    });
  };
  return (
    <TouchableWithoutFeedback onPress={handleOnPress}>
      <View className='bg-white_black100 rounded-3xl pb-4'>
        <View className='flex-between flex-row px-4 py-2'>
          {authorId && authorDisplayName && authorAvtUrl && (
            <TouchableWithoutFeedback
              onPress={() => {
                router.push({
                  pathname: "/(protected)/user/[id]",
                  params: { id: authorId }
                });
              }}
            >
              <View className='flex-center flex-row gap-2'>
                <Image
                  source={{ uri: authorAvtUrl }}
                  className='size-[30px] rounded-full'
                />
                <Text className='paragraph-medium text-black_white'>
                  {authorDisplayName}
                </Text>
              </View>
            </TouchableWithoutFeedback>
          )}
        </View>
        <View className='flex gap-3'>
          <LoadingImage
            source={recipeImgUrl}
            style={{ width: "100%", height: 240, borderRadius: 10 }}
          />

          <View className='gap-3 px-4'>
            <View className='gap-1'>
              <Text
                numberOfLines={1}
                ellipsizeMode='tail'
                className='text-black_white font-bold text-2xl'
              >
                {title}
              </Text>
              <Text
                numberOfLines={4}
                ellipsizeMode='tail'
                className='paragraph-regular text-black_white'
              >
                {description}
              </Text>
            </View>
          </View>
        </View>
      </View>
    </TouchableWithoutFeedback>
  );
};

export default SearchRecipeResult;
