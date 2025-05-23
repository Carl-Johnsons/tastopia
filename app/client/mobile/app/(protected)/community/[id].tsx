import {
  View,
  Text,
  SafeAreaView,
  TouchableWithoutFeedback,
  ActivityIndicator,
  ScrollView,
  RefreshControl,
  KeyboardAvoidingView,
  Platform,
  FlatList,
  StatusBar
} from "react-native";
import { Link, useLocalSearchParams, useRouter } from "expo-router";
import { globalStyles } from "@/components/common/GlobalStyles";
import { useBookmarkRecipe, useRecipeDetail } from "@/api/recipe";
import { Image } from "expo-image";
import BackButton from "@/components/BackButton";
import Vote from "@/components/common/Vote";
import { Entypo, Feather } from "@expo/vector-icons";
import Bookmark from "@/components/common/Bookmark";
import { Suspense, useCallback, useEffect, useMemo, useRef, useState } from "react";
import Ingredient from "@/components/screen/community/Ingredient";
import Step from "@/components/screen/community/Step";
import { useGetRecipeComment } from "@/api/comment";
import Comment from "@/components/screen/community/Comment";
import Button from "@/components/Button";
import AddCommentSection from "@/components/common/AddCommentSection";
import { filterUniqueItems } from "@/utils/dataFilter";
import { useTranslation } from "react-i18next";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";
import { useQueryClient } from "react-query";
import { useRouteGuardExclude } from "@/hooks/auth/useProtected";
import { ROLE } from "@/slices/auth.slice";
import Unauthorize from "@/components/common/Unauthorize";
import SettingRecipe from "@/components/common/SettingRecipe";
import BottomSheet from "@gorhom/bottom-sheet";
import NotFound from "@/app/+not-found";
import SimilarRecipe from "@/components/screen/community/SimilarRecipe";
import Loading from "@/components/common/Loading";
import SettingComment from "@/components/common/SettingComment";
import PreviewImage from "@/components/common/PreviewImage";
import { useErrorHandler } from "@/hooks/useErrorHandler";

const RecipeDetail = () => {
  const { hasAccess } = useRouteGuardExclude([ROLE.GUEST]);

  if (!hasAccess) {
    return (
      <View className='bg-white_black100 flex-1 items-center justify-center'>
        <Unauthorize />
      </View>
    );
  }

  const bottomSheetRef = useRef<BottomSheet>(null);
  const bottomSheetCommentRef = useRef<BottomSheet>(null);
  const queryClient = useQueryClient();
  const { c } = useColorizer();
  const { black, white } = colors;
  const LOAD_MORE_TIMES = 5;

  const router = useRouter();
  const { handleError } = useErrorHandler();
  const { t } = useTranslation("recipeDetail");
  const { id } = useLocalSearchParams<{ id: string }>();
  const [isBookmarked, setIsBookmarked] = useState<boolean>(false);
  const [currentComment, setCurrentComment] = useState<CommentCustomType>();
  const [currentCommentAuthorId, setCurrentCommentAuthorId] = useState("");
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
  const { mutateAsync: bookmark, isLoading: isBookmarking } = useBookmarkRecipe();

  const [comments, setComments] = useState(
    commentData?.pages.flatMap(page => page.paginatedData) ?? []
  );

  const handleRefresh = () => {
    refetchRecipeDetail();
    refetchGetRecipeComment();
  };

  const handleTouchMenu = () => {
    bottomSheetRef.current?.expand();
  };

  const handleTouchUser = () => {
    router.push({
      pathname: "/(protected)/user/[id]",
      params: { id: recipeDetailData?.recipe.authorId ?? "" }
    });
  };

  const handleToggleBookmark = () => {
    if (!isBookmarking) {
      setIsBookmarked(prev => !prev);
      bookmark(
        { recipeId: id },
        {
          onSuccess: async data => {
            setIsBookmarked(data.isBookmark);

            queryClient.setQueryData(["recipe", id], (oldData: unknown) => {
              if (!oldData) return oldData;
              return {
                ...oldData,
                isBookmarked: data.isBookmark
              };
            });
          },
          onError: error => handleError(error)
        }
      );
    }
  };

  const handleLoadMoreComment = useCallback(async () => {
    let callTimes = 1;
    const loadAllComments = async () => {
      if (hasNextPage && !isFetchingNextPage && callTimes <= LOAD_MORE_TIMES) {
        callTimes++;
        await fetchNextPage();
        loadAllComments();
      }
    };

    loadAllComments();
  }, [isFetchingNextPage, hasNextPage, fetchNextPage]);

  const setParentState = useCallback((comment: CommentType) => {
    setComments(prev => [comment, ...prev]);
  }, []);

  const deleteComment = useCallback(
    (commentId: string) => {
      setComments(prev => {
        const newComments = prev.filter(comment => comment.id !== commentId);

        return newComments;
      });
    },
    [setComments]
  );

  const updateComment = useCallback(
    (commentId: string, content: string) => {
      setComments(prev =>
        prev.map(comment =>
          comment.id === commentId ? { ...comment, content } : comment
        )
      );
    },
    [setComments]
  );

  useEffect(() => {
    if (commentData?.pages) {
      const uniqueComments = filterUniqueItems(commentData.pages);
      setComments(uniqueComments);
    }
  }, [commentData]);

  useEffect(() => {
    if (recipeDetailData?.isBookmarked !== undefined) {
      setIsBookmarked(recipeDetailData.isBookmarked);
    }
  }, [recipeDetailData]);

  if (isLoadingRecipeDetail || isLoadingGetRecipeComment) {
    return <Loading />;
  }

  if (!isLoadingRecipeDetail && !recipeDetailData?.recipe.id) {
    return <NotFound />;
  }

  return (
    <SafeAreaView
      style={{ backgroundColor: c(white.DEFAULT, black[100]), height: "100%" }}
    >
      <StatusBar backgroundColor={c(white.DEFAULT, black[100])} />
      {!isLoadingRecipeDetail && !isRefetchingRecipeDetail && recipeDetailData ? (
        <KeyboardAvoidingView
          behavior={Platform.OS === "ios" ? "padding" : "height"}
          style={{ flex: 1 }}
          keyboardVerticalOffset={Platform.OS === "ios" ? 0 : 20}
        >
          <ScrollView
            refreshControl={
              <RefreshControl
                refreshing={isRefetchingGetRecipeComment || isRefetchingRecipeDetail}
                onRefresh={handleRefresh}
                colors={[globalStyles.color.primary]}
              />
            }
          >
            <View className='mb-10 px-4'>
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
                        backgroundColor: c(white.DEFAULT, black[100]),
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
                  <PreviewImage
                    imgUrl={recipeDetailData.recipe.imageUrl}
                    className='h-[200] rounded-lg'
                    height={200}
                    isFill={true}
                    defaultImage={require("../../../assets/images/photo.png")}
                  />
                </View>

                <View className='mt-6 gap-3'>
                  <View className='flex-between flex-row'>
                    <Text className='h3-bold text-black_white'>
                      {recipeDetailData.recipe.title}
                    </Text>
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

                  <View className='flex-between flex-row'>
                    <View className='flex-start flex-row gap-3'>
                      <Vote
                        recipeId={recipeDetailData.recipe.id}
                        vote={recipeDetailData.vote}
                        voteDiff={recipeDetailData.recipe.voteDiff!}
                      />
                      <Bookmark
                        isBookmarked={isBookmarked}
                        handleToggleBookmark={handleToggleBookmark}
                      />
                    </View>
                    <Link
                      href={{
                        pathname: "/(protected)/community/cooking-mode",
                        params: { id: recipeDetailData.recipe.id }
                      }}
                    >
                      <View className='rounded-3xl bg-primary px-5 py-2'>
                        <Text className='body-semibold text-white_black'>
                          {t("cookButton")}
                        </Text>
                      </View>
                    </Link>
                  </View>

                  <View className='gap-6'>
                    <TouchableWithoutFeedback onPress={handleTouchUser}>
                      <View className='flex-row items-center gap-1'>
                        <Image
                          source={{ uri: recipeDetailData.authorAvtUrl }}
                          style={{
                            width: 36,
                            height: 36,
                            borderRadius: 100,
                            marginRight: 8
                          }}
                        />
                        <Text className='base-medium text-black_white'>
                          {recipeDetailData.authorDisplayName}
                        </Text>
                        <Text className='body-regular text-black_white'>
                          @{recipeDetailData.authorUsername}
                        </Text>
                        <Entypo
                          name='dot-single'
                          size={16}
                          color={globalStyles.color.gray400}
                        />
                        <Text className='body-regular text-black_white'>
                          {recipeDetailData.authorNumberOfFollower}{" "}
                          {recipeDetailData.authorNumberOfFollower === 1
                            ? t("follower")
                            : t("followers")}
                        </Text>
                      </View>
                    </TouchableWithoutFeedback>

                    <Text className='paragraph-regular text-black_white'>
                      {recipeDetailData.recipe.description}
                    </Text>

                    <View className='gap-3'>
                      <View className='bg-black_white h-[1.4px] w-full'></View>
                      <View className='flex-center'>
                        <Text className='paragraph-semibold text-black_white'>
                          {recipeDetailData.recipe.cookTime}
                        </Text>
                      </View>
                      <View className='bg-black_white h-[1.4px] w-full'></View>
                    </View>

                    <View>
                      <Text className='base-semibold text-black_white mb-1'>
                        {t("ingredient")}
                      </Text>
                      <Text className='paragraph-regular text-black_white'>
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
                      <Text className='base-semibold text-black_white'>{t("step")}</Text>
                      <View className='mt-4 gap-3'>
                        {sortedSteps?.map(step => {
                          return (
                            <Step
                              key={step.id}
                              isActive
                              content={step.content}
                              ordinalNumber={step.ordinalNumber}
                              attachedImageUrls={step.attachedImageUrls}
                            />
                          );
                        })}
                      </View>
                    </View>

                    <View className='h-[1px] w-full bg-primary'></View>

                    <View
                      className='justify-center gap-4'
                      testID='comment-section'
                    >
                      <Text className='base-semibold text-black_white'>
                        {t("comment")}
                      </Text>
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
                                accountId={comment.accountId}
                                avatarUrl={comment.avatarUrl}
                                displayName={comment.displayName}
                                content={comment.content}
                                commentId={comment.id}
                                bottomSheetRef={bottomSheetCommentRef}
                                setCurrentComment={(comment: CommentCustomType) => {
                                  setCurrentComment(comment);
                                }}
                                setCurrentCommentAuthorId={(id: string) => {
                                  setCurrentCommentAuthorId(id);
                                }}
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

                    {recipeDetailData.similarRecipes.length > 0 && (
                      <>
                        <View className='h-[1px] w-full bg-primary'></View>

                        <View className='gap-6'>
                          <Text className='base-bold text-black_white text-center'>
                            {t("similarRecipes")}
                          </Text>

                          <FlatList
                            scrollEnabled={false}
                            data={recipeDetailData.similarRecipes}
                            keyExtractor={item => item.recipeId}
                            showsVerticalScrollIndicator={false}
                            numColumns={2}
                            columnWrapperStyle={{
                              justifyContent: "space-between",
                              paddingHorizontal: 0
                            }}
                            renderItem={({ item, index }) => (
                              <View
                                className={`mb-4 w-[48%] ${index % 2 === 0 ? "mr-[4%]" : ""}`}
                              >
                                <SimilarRecipe {...item} />
                              </View>
                            )}
                          />
                        </View>
                      </>
                    )}
                  </View>
                </View>
              </Suspense>
            </View>
          </ScrollView>
        </KeyboardAvoidingView>
      ) : (
        <View className='flex-center h-full'>
          <ActivityIndicator
            size='large'
            color={globalStyles.color.primary}
          />
        </View>
      )}

      <SettingRecipe
        id={id}
        authorId={recipeDetailData?.recipe.authorId}
        ref={bottomSheetRef}
      />

      {currentComment?.id && currentComment?.content && (
        <SettingComment
          id={currentComment.id}
          recipeId={id}
          content={currentComment.content}
          authorId={currentCommentAuthorId}
          ref={bottomSheetCommentRef}
          deleteComment={deleteComment}
          updateComment={updateComment}
        />
      )}
    </SafeAreaView>
  );
};

export default RecipeDetail;
