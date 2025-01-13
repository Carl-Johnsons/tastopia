import {
  View,
  Text,
  SafeAreaView,
  TouchableWithoutFeedback,
  ActivityIndicator,
  ScrollView,
  RefreshControl
} from "react-native";
import { useLocalSearchParams, useRouter } from "expo-router";
import { globalStyles } from "@/components/common/GlobalStyles";
import { useRecipeDetail } from "@/api/recipe";
import { Image } from "expo-image";
import BackButton from "@/components/BackButton";
import Vote from "@/components/common/Vote";
import { Entypo, Feather } from "@expo/vector-icons";
import Bookmark from "@/components/common/Bookmark";
import { Suspense, useCallback, useEffect, useMemo, useState } from "react";
import Ingredient from "@/components/screen/community/Ingredient";
import Step from "@/components/screen/community/Step";
import { useGetRecipeComment } from "@/api/comment";
import Comment from "@/components/screen/community/Comment";
import Button from "@/components/Button";
import AddCommentSection from "@/components/common/AddCommentSection";
import { filterUniqueItems } from "@/utils/dataFilter";
import { useTranslation } from "react-i18next";

const RecipeDetail = () => {
  const { t } = useTranslation("recipeDetail");
  const router = useRouter();
  const { id } = useLocalSearchParams<{ id: string }>();
  const {
    data: recipeDetailData,
    isLoading: isLoadingRecipeDetail,
    refetch: refetchRecipeDetail,
    isRefetching: isRefetchingRecipeDetail
  } = useRecipeDetail(id);
  const sortedSteps = useMemo(() => {
    return recipeDetailData?.recipe.steps.sort(
      (a, b) => a.ordinalNumber - b.ordinalNumber
    );
  }, [recipeDetailData?.recipe.steps]);

  const {
    data: commentData,
    fetchNextPage,
    hasNextPage,
    isLoading: isLoadingGetRecipeComment,
    isFetchingNextPage,
    refetch: refetchGetRecipeComment,
    isRefetching: isRefetchingGetRecipeComment
  } = useGetRecipeComment(id);

  const [comments, setComments] = useState(
    commentData?.pages.flatMap(page => page.paginatedData) ?? []
  );

  const [isBookmarked, setIsBookmarked] = useState<boolean>(false);

  const handleRefresh = () => {
    refetchRecipeDetail();
    refetchGetRecipeComment();
  };
  const handleTouchMenu = () => {};
  const handleOpenCookMode = () => {};
  const handleTouchUser = () => {};
  const handleToggleBookmark = () => {
    setIsBookmarked(prev => !prev);
  };

  const handleLoadMoreComment = useCallback(() => {
    if (!isFetchingNextPage && hasNextPage) {
      fetchNextPage();
    }
  }, [isFetchingNextPage, hasNextPage, fetchNextPage]);

  const setParentState = useCallback((comment: CommentType) => {
    setComments(prev => [comment, ...prev]);
  }, []);

  useEffect(() => {
    if (commentData?.pages) {
      const uniqueComments = filterUniqueItems(commentData.pages);
      setComments(uniqueComments);
    }
  }, [commentData]);

  if (isLoadingRecipeDetail || isLoadingGetRecipeComment) {
    return (
      <View className='flex-1 items-center justify-center'>
        <ActivityIndicator
          size='large'
          color={globalStyles.color.primary}
        />
      </View>
    );
  }
  return (
    <SafeAreaView
      style={{
        backgroundColor: globalStyles.color.light,
        height: "100%"
      }}
    >
      {!isLoadingRecipeDetail && !isRefetchingRecipeDetail && recipeDetailData ? (
        <ScrollView
          refreshControl={
            <RefreshControl
              refreshing={isRefetchingGetRecipeComment || isRefetchingRecipeDetail}
              onRefresh={handleRefresh}
              colors={[globalStyles.color.primary]}
            />
          }
        >
          <View className='px-4'>
            <Suspense
              fallback={
                <ActivityIndicator
                  size='large'
                  color={globalStyles.color.primary}
                />
              }
            >
              <View className='relative'>
                <View className='absolute left-3 top-3 z-10'>
                  <BackButton
                    onPress={router.back}
                    style={{
                      backgroundColor: globalStyles.color.light,
                      padding: 12,
                      borderRadius: 12,
                      shadowColor: "#000",
                      shadowOffset: { width: 0, height: 2 },
                      shadowOpacity: 0.25,
                      shadowRadius: 3.84,
                      elevation: 5
                    }}
                  />
                </View>
                <Image
                  source={recipeDetailData.recipe.imageUrl}
                  style={{ height: 200, borderRadius: 10 }}
                />
              </View>

              <View className='mt-6 gap-3'>
                <View className='flex-between flex-row'>
                  <Text className='h3-bold'>{recipeDetailData.recipe.title}</Text>
                  <TouchableWithoutFeedback onPress={handleTouchMenu}>
                    <Feather
                      name='more-horizontal'
                      size={24}
                      color='black'
                    />
                  </TouchableWithoutFeedback>
                </View>

                <View className='flex-between flex-row'>
                  <View className='flex-start flex-row gap-3'>
                    <Vote voteDiff={recipeDetailData.recipe.voteDiff!} />
                    <Bookmark
                      isBookmarked={isBookmarked}
                      handleToggleBookmark={handleToggleBookmark}
                    />
                  </View>
                  <TouchableWithoutFeedback onPress={handleOpenCookMode}>
                    <View className='rounded-3xl bg-primary px-5 py-2'>
                      <Text className='body-semibold text-white_black'>
                        {t("cookButton")}
                      </Text>
                    </View>
                  </TouchableWithoutFeedback>
                </View>

                <View className='gap-6'>
                  <TouchableWithoutFeedback onPress={handleTouchUser}>
                    <View className='flex-row items-center gap-1'>
                      <Image
                        source={{ uri: recipeDetailData.authorAvtUrl }}
                        style={{ width: 36, height: 36, borderRadius: 100 }}
                      />
                      <Text className='base-medium'>
                        {recipeDetailData.authorDisplayName}
                      </Text>
                      <Text className='body-regular'>
                        @{recipeDetailData.authorUsername}
                      </Text>
                      <Entypo
                        name='dot-single'
                        size={16}
                        color={globalStyles.color.gray400}
                      />
                      <Text className='body-regular'>
                        {recipeDetailData.authorNumberOfFollower}{" "}
                        {recipeDetailData.authorNumberOfFollower === 1
                          ? t("follower")
                          : t("followers")}
                      </Text>
                    </View>
                  </TouchableWithoutFeedback>

                  <Text className='body-paragraph text-black_white'>
                    {recipeDetailData.recipe.description}
                  </Text>

                  <View className='gap-3'>
                    <View className='h-[1.4px] w-full bg-black'></View>
                    <View className='flex-center'>
                      <Text className='body-semibold'>
                        {recipeDetailData.recipe.cookTime}
                      </Text>
                    </View>
                    <View className='h-[1.4px] w-full bg-black'></View>
                  </View>

                  <View>
                    <Text className='base-semibold mb-1'>{t("ingredient")}</Text>
                    <Text className='body-regular'>
                      {t("for")} {recipeDetailData.recipe.serves}{" "}
                      {recipeDetailData.recipe.serves === 1
                        ? t("serving")
                        : t("servings")}
                    </Text>
                    <View className='mt-4 gap-3'>
                      {recipeDetailData.recipe.ingredients.map((ingredient, index) => {
                        return (
                          <Ingredient
                            key={ingredient + index}
                            ingredient={ingredient}
                          />
                        );
                      })}
                    </View>
                  </View>

                  <View>
                    <Text className='base-semibold'>{t("step")}</Text>
                    <View className='mt-4 gap-3'>
                      {sortedSteps?.map(step => {
                        return (
                          <Step
                            key={step.id}
                            content={step.content}
                            ordinalNumber={step.ordinalNumber}
                            attachedImageUrls={step.attachedImageUrls}
                          />
                        );
                      })}
                    </View>
                  </View>

                  <View className='h-[1px] w-full bg-primary'></View>

                  <View className='justify-center gap-4'>
                    <Text className='base-semibold'>{t("comment")}</Text>
                    <AddCommentSection
                      recipeId={id}
                      setParentState={setParentState}
                    />
                    <View className='gap-4'>
                      {comments.map(comment => {
                        return (
                          comment.isActive && (
                            <Comment
                              key={comment.id}
                              avatarUrl={comment.avatarUrl}
                              displayName={comment.displayName}
                              content={comment.content}
                            />
                          )
                        );
                      })}
                    </View>

                    {hasNextPage && (
                      <View className='flex-center'>
                        <Button
                          onPress={handleLoadMoreComment}
                          className='w-[180px] rounded-full bg-primary px-1 py-2 text-white'
                          isLoading={isLoadingGetRecipeComment || isFetchingNextPage}
                          disabled={isLoadingGetRecipeComment || isFetchingNextPage}
                          spinner={
                            <ActivityIndicator
                              animating={isLoadingGetRecipeComment}
                              color={"white"}
                            />
                          }
                        >
                          <Text className='text-white_black body-semibold text-center'>
                            {t("loadMoreComment")}
                          </Text>
                        </Button>
                      </View>
                    )}
                  </View>
                </View>
              </View>
            </Suspense>
          </View>
        </ScrollView>
      ) : (
        <View className='flex-center h-full'>
          <ActivityIndicator
            size='large'
            color={globalStyles.color.primary}
          />
        </View>
      )}
    </SafeAreaView>
  );
};

export default RecipeDetail;
