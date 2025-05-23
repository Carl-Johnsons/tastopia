import {
  View,
  Text,
  Image,
  TouchableWithoutFeedback,
  Platform,
  Pressable
} from "react-native";
import { router } from "expo-router";
import InteractionSection from "./InteractionSection";
import { MiniRecipeType } from "@/types/recipe";
import { useCallback } from "react";

type MiniRecipeProps = MiniRecipeType & {
  /** Custom class name for the container. */
  containerClassName?: string;
};

export default function MiniRecipe({
  id,
  authorId,
  recipeImgUrl,
  title,
  authorDisplayName,
  voteDiff,
  vote,
  containerClassName
}: MiniRecipeProps) {
  const handleOnPress = useCallback(() => {
    router.push({
      pathname: "/(protected)/community/[id]",
      params: { id }
    });
  }, [id, router]);

  return (
    <TouchableWithoutFeedback onPress={handleOnPress}>
      <View
        className={`bg-white_black dark:border dark:border-gray-600 h-[150px] w-[140px] rounded-3xl ${containerClassName}`}
        style={Platform.select({
          ios: {
            shadowOffset: { width: 0, height: 2 },
            shadowOpacity: 0.25,
            shadowRadius: 25
          },
          android: { elevation: 10 }
        })}
      >
        <View className='relative flex gap-1'>
          <Image
            source={{ uri: recipeImgUrl }}
            style={{ width: "100%", height: "60%", borderRadius: 19 }}
          />

          {voteDiff !== undefined && vote !== undefined && (
            <View className='absolute left-0 top-0'>
              <InteractionSection
                containerClassName='bg-white_black200 rounded-full'
                recipeId={id}
                vote={vote}
                voteDiff={voteDiff}
              />
            </View>
          )}

          <View className='px-3'>
            <Text
              numberOfLines={1}
              ellipsizeMode='tail'
              className='text-black_white text-lg'
            >
              {title}
            </Text>

            {authorId && authorDisplayName && (
              <Pressable
                onPress={() => {
                  router.push({
                    pathname: "/(protected)/user/[id]",
                    params: { id: authorId }
                  });
                }}
              >
                <Text
                  className='text-black_white font-sans text-sm'
                  numberOfLines={1}
                  ellipsizeMode='tail'
                >
                  {authorDisplayName}
                </Text>
              </Pressable>
            )}
          </View>
        </View>
      </View>
    </TouchableWithoutFeedback>
  );
}
