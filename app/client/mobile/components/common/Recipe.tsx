import { Feather } from "@expo/vector-icons";
import { View, Text, Image, TouchableWithoutFeedback } from "react-native";
import { useRouter } from "expo-router";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";
import InteractionSection from "./InteractionSection";
import { RefObject } from "react";
import { BottomSheetMethods } from "@gorhom/bottom-sheet/lib/typescript/types";

const Recipe = ({
  bottomSheetRef,
  setCurrentRecipeId,
  setCurrentAuthorId,
  id,
  authorId,
  recipeImgUrl,
  title,
  description,
  authorDisplayName,
  authorAvtUrl,
  voteDiff,
  numberOfComment,
  vote
}: RecipeType & {
  bottomSheetRef: RefObject<BottomSheetMethods>;
  setCurrentRecipeId: (id: string) => void;
  setCurrentAuthorId: (id: string) => void;
}) => {
  const router = useRouter();
  const { c } = useColorizer();
  const { black, white } = colors;
  const handleOnPress = () => {
    router.push({
      pathname: "/(protected)/community/[id]",
      params: { id }
    });
  };
  const handleTouchMenu = () => {
    setCurrentRecipeId(id);
    setCurrentAuthorId(authorId);
    bottomSheetRef.current?.expand();
  };

  return (
    <TouchableWithoutFeedback onPress={handleOnPress}>
      <View className='bg-white_black100 w-[94vw] rounded-3xl pb-4'>
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
                  className='mr-1 size-[30px] rounded-full'
                />
                <Text className='paragraph-medium text-black_white'>
                  {authorDisplayName}
                </Text>
              </View>
            </TouchableWithoutFeedback>
          )}

          <TouchableWithoutFeedback onPress={handleTouchMenu}>
            <View>
              <Feather
                name='more-horizontal'
                size={24}
                color={c(black.DEFAULT, white.DEFAULT)}
              />
            </View>
          </TouchableWithoutFeedback>
        </View>
        <View className='flex gap-3'>
          <Image
            source={{ uri: recipeImgUrl }}
            style={{ width: "100%", height: 240, borderRadius: 10 }}
          />

          <View className='gap-3'>
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

            {(voteDiff !== undefined ||
              numberOfComment !== undefined ||
              vote !== undefined) && (
              <InteractionSection
                recipeId={id}
                vote={vote}
                handleOnPress={handleOnPress}
                voteDiff={voteDiff}
                numberOfComment={numberOfComment}
              />
            )}
          </View>
        </View>
      </View>
    </TouchableWithoutFeedback>
  );
};

export default Recipe;
