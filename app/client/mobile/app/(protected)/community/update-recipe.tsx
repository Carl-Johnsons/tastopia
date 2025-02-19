import { AntDesign, Entypo } from "@expo/vector-icons";
import { router, useLocalSearchParams } from "expo-router";
import { useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { FormProvider, SubmitHandler, useFieldArray, useForm } from "react-hook-form";
import uuid from "react-native-uuid";

import {
  StatusBar,
  Text,
  TouchableWithoutFeedback,
  View,
  SafeAreaView,
  Alert,
  ActivityIndicator,
  KeyboardAvoidingView,
  Platform,
  FlatList
} from "react-native";
import { yupResolver } from "@hookform/resolvers/yup";
import { globalStyles } from "@/components/common/GlobalStyles";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";
import TagList from "@/components/screen/community/TagList";
import DraggableFlatList, {
  RenderItemParams,
  ScaleDecorator
} from "react-native-draggable-flatlist";
import { useRecipeDetail } from "@/api/recipe";
import useIsOwner from "@/hooks/auth/useIsOwner";
import PermissionDenied from "@/components/common/PermissionDenied";
import UpdateDraggableStep from "@/components/screen/community/UpdateDraggableStep";
import UpdateFormHeader from "@/components/screen/community/UpdateFormHeader";
import { protectedAxiosInstance } from "@/constants/host";
import { useQueryClient } from "react-query";
import UpdateIngredient from "@/components/screen/community/UpdateIngredient";
import { FormUpdateRecipeType, schema } from "@/schemas/update-recipe";
import Loading from "@/components/common/Loading";

const UpdateRecipe = () => {
  const queryClient = useQueryClient();
  const { id, authorId } = useLocalSearchParams<{ id: string; authorId: string }>();
  const isCreatedByCurrentUser = useIsOwner(authorId);

  if (!isCreatedByCurrentUser) {
    return (
      <View className='bg-white_black100 flex-1 items-center justify-center'>
        <PermissionDenied />
      </View>
    );
  }

  const { c } = useColorizer();
  const { black, white } = colors;
  const { t } = useTranslation("updateRecipe");

  const { data: recipeDetailData, isLoading: isLoadingRecipeDetail } =
    useRecipeDetail(id);

  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [images, setImages] = useState<UpdateImage>({
    additionalImages: [],
    defaultImages: [],
    deleteUrls: []
  });
  const [selectedTags, setSelectedTags] = useState<SelectedTag[]>([]);

  const formUpdateRecipe = useForm<FormUpdateRecipeType>({
    resolver: yupResolver(schema),
    defaultValues: {
      title: "",
      description: "",
      cookTime: "",
      ingredients: [{ key: uuid.v4(), value: "" }],
      steps: [{ key: uuid.v4(), content: "", images: {} }]
    }
  });

  const {
    control: formControl,
    formState: { errors },
    handleSubmit,
    setValue,
    getValues
  } = formUpdateRecipe;

  const {
    fields: ingredientsFields,
    append: appendIngredient,
    remove: removeIngredient
  } = useFieldArray({
    control: formControl,
    name: "ingredients"
  });

  const {
    fields: stepsFields,
    append: appendStep,
    remove: removeStep
  } = useFieldArray({
    control: formControl,
    name: "steps"
  });

  const onSubmit: SubmitHandler<FormUpdateRecipeType> = async formData => {
    const isInputIngredient = formData.ingredients?.some(ingredient => {
      return ingredient.value;
    });

    const isInputStep = formData.steps?.some(step => {
      return step.content;
    });

    const validateRecipeImage = () => {
      if (!images.deleteUrls?.length && !images.additionalImages?.length) {
        return true;
      }

      if (images.deleteUrls?.length! > 0 && !images.additionalImages?.length) {
        Alert.alert(t("validation.image"));
        return false;
      }

      if (images.additionalImages?.length! > 1) {
        Alert.alert(t("validation.onlyOneImage"));
        return false;
      }

      return true;
    };

    if (!validateRecipeImage()) {
      return;
    }

    if (images?.defaultImages?.length! < 1 && images?.additionalImages?.length! < 1) {
      Alert.alert(t("validation.image"));
      return;
    }

    if (!isInputIngredient) {
      Alert.alert(t("validation.ingredient"));
      return;
    }

    if (!isInputStep) {
      Alert.alert(t("validation.step"));
      return;
    }

    setIsLoading(true);
    const data = new FormData();

    data.append("id", id);
    data.append("title", formData.title);
    data.append("description", formData.description);
    data.append("serves", formData.serves.toString());
    data.append("cookTime", formData.cookTime);

    const image = images.additionalImages?.[0];

    if (image) {
      data.append(`recipeImage`, {
        uri: image.uri,
        name: image.name,
        type: image.type || "image/jpeg"
      } as unknown as Blob);
    }

    formData.ingredients?.forEach((ingredient, index) => {
      data.append(`ingredients[${index}]`, ingredient.value);
    });

    formData.steps?.forEach((step, index) => {
      data.append(`steps[${index}].stepId`, step.key);
      data.append(`steps[${index}].ordinalNumber`, String(index + 1));
      data.append(`steps[${index}].content`, step.content);

      if (step.images?.additionalImages?.length ?? 0 > 0) {
        step.images?.additionalImages?.forEach(image => {
          data.append(`steps[${index}].Images`, {
            uri: image.uri,
            name: image.name,
            type: image.type
          } as any);
        });
      }

      if (step.images?.deleteUrls?.length ?? 0 > 0) {
        step.images?.deleteUrls?.forEach((deleteUrl, deleteUrlsIndex) => {
          data.append(`steps[${index}].deleteUrls[${deleteUrlsIndex}]`, deleteUrl ?? "");
        });
      }
    });

    selectedTags.forEach((tag, index) => {
      data.append(`tagValues[${index}]`, tag.value);
    });

    try {
      const { data: _response } = await protectedAxiosInstance.post(
        "/api/recipe/update-recipe",
        data,
        {
          headers: {
            "Content-Type": "multipart/form-data"
          }
        }
      );
      Alert.alert(t("formTitle.updateSuccessfully"));
      queryClient.invalidateQueries({ queryKey: ["recipe", id] });
      router.back();
    } catch (error) {
      Alert.alert(t("formTitle.updateError"));
      console.error("Error submitting recipe:", error);
    } finally {
      setIsLoading(false);
    }
  };

  const handleAddMoreIngredient = useCallback(() => {
    appendIngredient({ key: uuid.v4(), value: "" });
  }, [appendIngredient]);

  const handleAddMoreStep = useCallback(() => {
    appendStep({
      key: uuid.v4(),
      content: "",
      images: {
        defaultImages: [],
        deleteUrls: [],
        additionalImages: []
      }
    });
  }, []);

  const handleCancel = () => {
    Alert.alert(t("confirmModal.title"), t("confirmModal.description"), [
      {
        text: t("confirmModal.cancel")
      },
      {
        text: t("confirmModal.ok"),
        onPress: () => {
          router.back();
        }
      }
    ]);
  };

  const renderStepItem = useCallback(
    ({ item, drag, isActive, getIndex }: RenderItemParams<UpdateStepType>) => {
      return (
        <ScaleDecorator>
          <UpdateDraggableStep
            key={item.key}
            stepKey={item.key}
            content={item.content}
            images={item.images}
            drag={drag}
            index={getIndex()!}
            remove={removeStep}
          />
        </ScaleDecorator>
      );
    },
    [setSelectedTags]
  );

  useEffect(() => {
    setValue("title", recipeDetailData?.recipe.title!);
    setValue("description", recipeDetailData?.recipe.description!);
    setValue("cookTime", recipeDetailData?.recipe.cookTime!);
    setValue("serves", recipeDetailData?.recipe.serves!);

    setImages(prev => {
      return {
        ...prev,
        defaultImages: [recipeDetailData?.recipe.imageUrl ?? ""]
      };
    });

    setValue(
      "ingredients",
      recipeDetailData?.recipe.ingredients.map(ingredient => {
        return {
          key: uuid.v4(),
          value: ingredient
        };
      }) ?? []
    );

    setValue(
      "steps",
      recipeDetailData?.recipe.steps.map(step => ({
        key: step.id,
        content: step.content,
        images: {
          defaultImages: step.attachedImageUrls,
          deleteUrls: [],
          additionalImages: []
        }
      })) ?? []
    );

    setSelectedTags(
      recipeDetailData?.tags?.map(tag => {
        return {
          id: tag.id,
          code: tag.code,
          value: tag.value
        };
      }) || []
    );
  }, [isLoadingRecipeDetail]);

  if (isLoadingRecipeDetail) {
    return <Loading />;
  }

  return (
    <SafeAreaView
      style={{ backgroundColor: c(white.DEFAULT, black[100]), height: "100%" }}
    >
      <StatusBar backgroundColor={c(white.DEFAULT, black[100])} />
      <View className='size-full flex-col'>
        <KeyboardAvoidingView
          style={{ flex: 1 }}
          behavior={Platform.OS === "ios" ? "padding" : "height"}
        >
          <View
            style={{ marginTop: StatusBar.currentHeight }}
            className='flex-between mb-4 h-[60px] flex-row border-b-[0.6px] border-gray-400 px-6'
          >
            <TouchableWithoutFeedback onPress={handleCancel}>
              <View>
                <AntDesign
                  name='close'
                  size={20}
                  color={c(black.DEFAULT, white.DEFAULT)}
                />
              </View>
            </TouchableWithoutFeedback>

            <View className='absolute left-1/2 -translate-x-1/3 items-center'>
              <Text className='text-black_white paragraph-medium'>
                {t("screens.title")}
              </Text>
            </View>

            <TouchableWithoutFeedback
              onPress={handleSubmit(onSubmit)}
              disabled={isLoading}
            >
              <View className='items-center'>
                {isLoading ? (
                  <ActivityIndicator
                    size={"small"}
                    color={globalStyles.color.primary}
                  />
                ) : (
                  <Text className='paragraph-medium text-primary'>
                    {t("screens.action")}
                  </Text>
                )}
              </View>
            </TouchableWithoutFeedback>
          </View>

          {/* Form */}
          <View className='relative flex-1 px-6'>
            {isLoading && (
              <View className='flex-center absolute left-6 z-10 size-full bg-transparent'>
                <ActivityIndicator
                  size={"large"}
                  color={globalStyles.color.primary}
                />
              </View>
            )}

            <FormProvider {...formUpdateRecipe}>
              <DraggableFlatList
                key={"draggable-flat-list-create-steps"}
                data={stepsFields}
                style={{ height: "100%" }}
                onDragEnd={({ data }) => setValue("steps", data)}
                keyExtractor={item => item.key}
                renderItem={renderStepItem}
                showsVerticalScrollIndicator={false}
                ListHeaderComponent={() => {
                  return (
                    <View className='gap-4'>
                      <UpdateFormHeader
                        images={images}
                        setImages={setImages}
                        formControl={formControl}
                        errors={errors}
                      />

                      {/* Ingredient */}
                      <View>
                        <FlatList
                          scrollEnabled={false}
                          key={"draggable-flat-list-create-ingredients"}
                          data={ingredientsFields}
                          keyExtractor={item => item.key}
                          renderItem={({ item, index }) => (
                            <UpdateIngredient
                              key={item.key}
                              value={item.value}
                              index={index}
                              remove={removeIngredient}
                            />
                          )}
                          showsVerticalScrollIndicator={false}
                          ListHeaderComponent={() => {
                            return (
                              <Text className='body-semibold text-black_white mb-2'>
                                {t("formTitle.ingredients")}
                              </Text>
                            );
                          }}
                          ListFooterComponent={() => {
                            return (
                              <View className='flex-center mt-2'>
                                <TouchableWithoutFeedback
                                  onPress={handleAddMoreIngredient}
                                >
                                  <View className='flex-center flex-row gap-1'>
                                    <Entypo
                                      name='plus'
                                      size={24}
                                      color={c(black.DEFAULT, white.DEFAULT)}
                                    />
                                    <Text className='body-semibold text-black_white'>
                                      {t("formTitle.ingredients")}
                                    </Text>
                                  </View>
                                </TouchableWithoutFeedback>
                              </View>
                            );
                          }}
                        />
                      </View>

                      <View className='mt-4'>
                        <Text className='body-semibold text-black_white mb-2'>
                          {t("formTitle.method")}
                        </Text>
                      </View>
                    </View>
                  );
                }}
                ListFooterComponent={() => {
                  return (
                    <View>
                      <View className='flex-center mt-2'>
                        <TouchableWithoutFeedback onPress={handleAddMoreStep}>
                          <View className='flex-center flex-row gap-1'>
                            <Entypo
                              name='plus'
                              size={24}
                              color={c(black.DEFAULT, white.DEFAULT)}
                            />
                            <Text className='body-semibold text-black_white'>
                              {t("formTitle.step")}
                            </Text>
                          </View>
                        </TouchableWithoutFeedback>
                      </View>

                      {/* Tag */}
                      <View className='my-4'>
                        <Text className='body-semibold text-black_white'>
                          {t("formTitle.tag")}
                        </Text>
                        <TagList
                          selectedTags={selectedTags}
                          setSelectedTags={setSelectedTags}
                        />
                      </View>
                    </View>
                  );
                }}
              />
            </FormProvider>
          </View>
        </KeyboardAvoidingView>
      </View>
    </SafeAreaView>
  );
};

export default UpdateRecipe;
