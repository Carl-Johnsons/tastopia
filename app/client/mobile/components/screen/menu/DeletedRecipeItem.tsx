import { useRouter } from "expo-router";
import { RecipeType } from "@/types/recipe";
import { useTranslation } from "react-i18next";
import { useRestoreOwnRecipe } from "@/api/recipe";
import { useRouteGuardExclude } from "@/hooks/auth/useProtected";
import { ROLE } from "@/slices/auth.slice";
import Unauthorize from "@/components/common/Unauthorize";
import { useQueryClient } from "react-query";
import { View, Text, Image, TouchableWithoutFeedback, Alert } from "react-native";

const DeletedRecipeItem = ({
  id,
  authorId,
  recipeImgUrl,
  title,
  description,
  authorDisplayName,
  authorAvtUrl
}: RecipeType) => {
  const { hasAccess } = useRouteGuardExclude([ROLE.GUEST]);

  if (!hasAccess) {
    return (
      <View className='bg-white_black100 flex-1 items-center justify-center'>
        <Unauthorize />
      </View>
    );
  }

  const router = useRouter();
  const { t } = useTranslation("menu");
  const queryClient = useQueryClient();
  const { mutateAsync: restoreOwnRecipe, isLoading: isDeletingOwnRecipe } =
    useRestoreOwnRecipe();

  const handleRestore = () => {
    restoreOwnRecipe(
      { recipeId: id },
      {
        onSuccess: async data => {
          queryClient.invalidateQueries({ queryKey: "deletedRecipes" });
          Alert.alert(t("restoreSuccessfully"));
        },
        onError: error => {
          Alert.alert(t("restoreFailed"));
        }
      }
    );
  };

  return (
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

          <TouchableWithoutFeedback onPress={handleRestore}>
            <View className='self-start rounded-3xl bg-primary px-5 py-2'>
              <Text className='body-semibold text-white_black'>{t("restore")}</Text>
            </View>
          </TouchableWithoutFeedback>
        </View>
      </View>
    </View>
  );
};

export default DeletedRecipeItem;
