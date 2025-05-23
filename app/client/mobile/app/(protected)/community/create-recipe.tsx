import {
  FormCreateRecipeType,
  schema as createRecipeSchema
} from "@/schemas/create-recipe";
import { AntDesign, Entypo } from "@expo/vector-icons";
import { router } from "expo-router";
import { useCallback, useState } from "react";
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
  FlatList,
  ScrollView
} from "react-native";
import { yupResolver } from "@hookform/resolvers/yup";
import { protectedAxiosInstance } from "@/constants/host";
import { globalStyles } from "@/components/common/GlobalStyles";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";
import CreateIngredient from "@/components/screen/community/CreateIngredient";
import TagList from "@/components/screen/community/TagList";
import CreateDraggableStep from "@/components/screen/community/CreateDraggableStep";
import CreateFormHeader from "@/components/screen/community/CreateFormHeader";
import { useRecipesFeed } from "@/api/recipe";
import { useErrorHandler } from "@/hooks/useErrorHandler";

const CreateRecipe = () => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const { refetch } = useRecipesFeed("All");

  const { t } = useTranslation("createRecipe");
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [images, setImages] = useState<ImageFileType[]>([]);
  const [selectedTags, setSelectedTags] = useState<SelectedTag[]>([]);
  const { handleError } = useErrorHandler();

  const formCreateRecipe = useForm<FormCreateRecipeType>({
    resolver: yupResolver(createRecipeSchema),
    defaultValues: {
      title: "",
      description: "",
      cookTime: "",
      ingredients: [{ key: uuid.v4(), value: "" }],
      steps: [{ key: uuid.v4(), content: "", images: [] }]
    }
  });

  const {
    control: formControl,
    formState: { errors },
    handleSubmit,
    getValues,
    setValue
  } = formCreateRecipe;

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

  const onSubmit: SubmitHandler<FormCreateRecipeType> = async formData => {
    const isInputIngredient = formData.ingredients?.some(ingredient => {
      return ingredient.value;
    });

    const isInputStep = formData.steps?.some(step => {
      return step.content;
    });

    if (images.length < 1) {
      Alert.alert(t("validation.image"));
      return;
    }

    if (!isInputIngredient) {
      Alert.alert(t("validation.ingredients.itemRequired"));
      return;
    }

    if (formData?.ingredients?.length && formData?.ingredients?.length > 50) {
      Alert.alert(t("validation.ingredients.max"));
      return;
    }

    if (!isInputStep) {
      Alert.alert(t("validation.step"));
      return;
    }

    if (formData?.steps?.length && formData?.steps?.length > 15) {
      Alert.alert(t("validation.steps.max"));
      return;
    }

    setIsLoading(true);
    const data = new FormData();
    data.append("title", formData.title);
    data.append("description", formData.description);
    data.append("serves", formData.serves.toString());
    data.append("cookTime", formData.cookTime);
    const image = images[0];

    data.append(`recipeImage`, {
      uri: image.uri,
      name: image.name,
      type: image.type || "image/jpeg"
    } as unknown as Blob);

    formData.ingredients?.forEach((ingredient, index) => {
      data.append(`ingredients[${index}]`, ingredient.value);
    });

    formData.steps?.forEach((step, index) => {
      console.log("step", step);
      data.append(`steps[${index}].ordinalNumber`, String(index + 1));
      data.append(`steps[${index}].content`, step.content);
      if (step?.images?.length && step.images.length > 0) {
        step.images.forEach(image => {
          data.append(`steps[${index}].Images`, {
            uri: image.uri,
            name: image.name,
            type: image.type
          } as any);
        });
      }
    });
    selectedTags.forEach((tag, index) => {
      data.append(`tagValues[${index}]`, tag.value);
    });

    try {
      const { data: response } = await protectedAxiosInstance.post(
        "/api/recipe/create-recipe",
        data,
        {
          headers: {
            "Content-Type": "multipart/form-data"
          }
        }
      );
      Alert.alert(t("formTitle.createSuccessfully"));
      refetch();
      router.back();
    } catch (error) {
      handleError(error),
        // Alert.alert(t("formTitle.createError"));
        console.error("Error submitting recipe:", error);
    } finally {
      setIsLoading(false);
    }
  };

  const handleAddMoreIngredient = useCallback(() => {
    appendIngredient({ key: uuid.v4(), value: "" });
  }, [appendIngredient]);

  const handleAddMoreStep = useCallback(() => {
    appendStep({ key: uuid.v4(), content: "", images: [] });
  }, []);

  const onFileChange = useCallback((files: ImageFileType[]) => {
    setImages(files);
  }, []);

  const handleCancel = () => {
    if (
      getValues("cookTime") ||
      getValues("description") ||
      getValues("serves") ||
      getValues("title") ||
      images.length > 0
    ) {
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
    } else {
      router.back();
    }
  };

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
          <View className='flex-between mb-4 h-[60px] flex-row border-b-[0.6px] border-gray-400 px-6'>
            <TouchableWithoutFeedback onPress={handleCancel}>
              <View>
                <AntDesign
                  name='close'
                  size={20}
                  color={c(black.DEFAULT, white.DEFAULT)}
                />
              </View>
            </TouchableWithoutFeedback>

            <View className='items-center'>
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
              <View className='flex-center absolute left-6 z-10 size-full'></View>
            )}

            <FormProvider {...formCreateRecipe}>
              <ScrollView
                showsVerticalScrollIndicator={false}
                style={{ flex: 1 }}
                keyboardShouldPersistTaps='handled'
              >
                {/* Header Content */}
                <View className='gap-4'>
                  <CreateFormHeader
                    images={images}
                    onFileChange={onFileChange}
                    formControl={formControl}
                    errors={errors}
                  />
                  {/* Ingredient */}
                  <View>
                    <FlatList
                      scrollEnabled={false}
                      key={"flat-list-create-ingredients"}
                      data={ingredientsFields}
                      keyExtractor={item => item.key ?? uuid.v4()}
                      renderItem={({ item, index }) => (
                        <CreateIngredient
                          key={item.key}
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
                            <TouchableWithoutFeedback onPress={handleAddMoreIngredient}>
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

                {stepsFields.map((item, index) => {
                  const stepImages = item.images || [];
                  return (
                    <CreateDraggableStep
                      key={item.key}
                      stepKey={item.key}
                      index={index}
                      images={stepImages}
                      remove={removeStep}
                      content={item.content}
                    />
                  );
                })}

                {/* Footer Content */}
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
                  <View className='mb-4 mt-4'>
                    <Text className='body-semibold text-black_white mb-2'>
                      {t("formTitle.tag")}
                    </Text>
                    <TagList
                      selectedTags={selectedTags}
                      setSelectedTags={setSelectedTags}
                    />
                  </View>
                </View>
              </ScrollView>
            </FormProvider>
          </View>
        </KeyboardAvoidingView>
      </View>
    </SafeAreaView>
  );
};

export default CreateRecipe;
