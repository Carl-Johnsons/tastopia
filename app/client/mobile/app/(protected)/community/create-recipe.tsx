import CreateRecipeDraggable from "@/components/screen/community/CreateRecipeDraggable";
import Input from "@/components/common/Input";
import UploadImage from "@/components/common/UploadImage";
import {
  FormCreateRecipeType,
  schema as createRecipeSchema
} from "@/schemas/create-recipe";
import { ImageFileType } from "@/types/image";
import { AntDesign } from "@expo/vector-icons";
import { router } from "expo-router";
import { Dispatch, memo, SetStateAction, useCallback, useState } from "react";
import { useTranslation } from "react-i18next";
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import uuid from "react-native-uuid";

import {
  FlatList,
  ScrollView,
  StatusBar,
  Text,
  TouchableWithoutFeedback,
  View,
  SafeAreaView,
  Alert,
  ActivityIndicator,
  KeyboardAvoidingView,
  Platform
} from "react-native";
import DraggableIngredient from "@/components/screen/community/DraggableIngredient";
import { yupResolver } from "@hookform/resolvers/yup";
import { protectedAxiosInstance } from "@/constants/host";
import { useCreateRecipe } from "@/api/recipe";
import { globalStyles } from "@/components/common/GlobalStyles";

type renderIngredientItemProps = CreateIngredientType & {
  setIngredients: Dispatch<SetStateAction<CreateIngredientType[]>>;
};

const CreateRecipe = () => {
  const { t } = useTranslation("createRecipe");
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [images, setImages] = useState<ImageFileType[]>([]);
  const [ingredients, setIngredients] = useState<CreateIngredientType[]>([
    { key: uuid.v4(), value: "" }
  ]);
  const [steps, setSteps] = useState<CreateStepType[]>([
    { key: uuid.v4(), content: "", images: [] }
  ]);

  // console.log("ingredients", ingredients);
  const onFileChange = (files: ImageFileType[]) => {
    setImages(files);
  };

  const formCreateRecipe = useForm<FormCreateRecipeType>({
    resolver: yupResolver(createRecipeSchema),
    defaultValues: {
      title: "",
      description: "",
      serves: "",
      cookTime: ""
    }
  });

  // const { mutate: createRecipe, isLoading } = useCreateRecipe();
  const {
    control: formControl,
    formState: { errors },
    handleSubmit,
    getValues
  } = formCreateRecipe;
  const onSubmit: SubmitHandler<FormCreateRecipeType> = async formData => {
    const isInputIngredient = ingredients.some(ingredient => ingredient.value !== "");
    const isInputSteps = steps.some(step => step.content !== "");

    if (images.length < 1) {
      Alert.alert(t("validation.image"));
      return;
    }

    if (!isInputIngredient) {
      Alert.alert(t("validation.ingredient"));
      return;
    }

    if (!isInputSteps) {
      Alert.alert(t("validation.step"));
      return;
    }

    setIsLoading(true);
    const data = new FormData();

    data.append("title", formData.title);
    data.append("description", formData.description);
    data.append("serves", formData.serves);
    data.append("cookTime", formData.cookTime);

    const image = images[0];
    data.append(`recipeImage`, {
      uri: image.uri,
      name: image.name,
      type: image.type || "image/jpeg"
    } as any);

    ingredients.forEach((ingredient, index) => {
      data.append(`ingredients[${index}]`, ingredient.value);
    });
    steps.forEach((step, index) => {
      data.append(`steps[${index}].ordinalNumber`, String(index + 1));
      data.append(`steps[${index}].content`, step.content);
      if (step.images.length > 0) {
        step.images.forEach(image => {
          data.append(`steps[${index}].Images`, {
            uri: image.uri,
            name: image.name,
            type: image.type
          } as any);
        });
      }
    });

    const tagValues = ["code 0", "code 1"];
    tagValues.forEach((code, index) => {
      data.append(`tagValues[${index}]`, code);
    });

    try {
      const { data: response } = await protectedAxiosInstance.post(
        "http://localhost:5000/api/recipe/create-recipe",
        data,
        {
          headers: {
            "Content-Type": "multipart/form-data"
          }
        }
      );
      // createRecipe(
      //   {
      //     data
      //   },
      //   {
      //     onSuccess: response => {
      //       console.log("data", response);
      //     },
      //     onError: error => {
      //       console.error("Failed to create recipe:", error);
      //       Alert.alert("Fail to create recipe");
      //     }
      //   }
      // );
      Alert.alert("Create recipe successfully!");
      router.back();
    } catch (error) {
      console.error("Error submitting recipe:", error);
    } finally {
      setIsLoading(false);
    }
  };

  const handleCancel = () => {
    if (
      getValues("cookTime") ||
      getValues("description") ||
      getValues("serves") ||
      getValues("title") ||
      images.length > 0 ||
      steps.length > 0 ||
      ingredients.length > 0
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
    <SafeAreaView>
      <View className={`bg-white_black size-full flex-col`}>
        <KeyboardAvoidingView
          style={{ flex: 1 }}
          behavior={Platform.OS === "ios" ? "padding" : "height"}
        >
          <View
            style={{ marginTop: StatusBar.currentHeight }}
            className='flex-between mb-4 h-[60px] flex-row border-b-[0.6px] border-gray-400 px-6'
          >
            <TouchableWithoutFeedback onPress={handleCancel}>
              <View className=''>
                <AntDesign
                  name='close'
                  size={20}
                  color='black'
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
              <View className='flex-center absolute left-6 z-10 size-full bg-transparent'>
                <ActivityIndicator
                  size={"large"}
                  color={globalStyles.color.primary}
                />
              </View>
            )}

            <CreateRecipeDraggable
              ingredients={ingredients}
              setIngredients={setIngredients}
              steps={steps}
              setSteps={setSteps}
              form={
                <View>
                  <FormProvider {...formCreateRecipe}>
                    <View className='d-flex justify-center gap-4'>
                      <View className='my-5'>
                        <UploadImage
                          defaultImages={images}
                          onFileChange={onFileChange}
                          isMultiple={false}
                        />
                      </View>

                      <View>
                        <Input
                          variant='secondary'
                          control={formControl}
                          name='title'
                          placeHolder={t("formPlaceholder.title")}
                          errors={[t(errors.title?.message ?? "")]}
                        />
                      </View>

                      <View>
                        <Input
                          variant='secondary'
                          control={formControl}
                          name='description'
                          placeHolder={t("formPlaceholder.description")}
                          errors={[t(errors.description?.message ?? "")]}
                        />
                      </View>

                      <View className='flex-center flex-row gap-6'>
                        <View className='flex-1'>
                          <Text className='body-semibold'>{t("formTitle.serves")}</Text>
                          <Input
                            variant='secondary'
                            control={formControl}
                            name='serves'
                            placeHolder={t("formPlaceholder.serves")}
                            errors={[t(errors.serves?.message ?? "")]}
                          />
                        </View>

                        <View className='flex-1'>
                          <Text className='body-semibold'>{t("formTitle.cookTime")}</Text>
                          <Input
                            variant='secondary'
                            control={formControl}
                            name='cookTime'
                            placeHolder={t("formPlaceholder.cookTime")}
                            errors={[t(errors.cookTime?.message ?? "")]}
                          />
                        </View>
                      </View>

                      <View>
                        <Text className='body-semibold mb-2'>
                          {t("formTitle.ingredients")}
                        </Text>
                      </View>
                    </View>
                  </FormProvider>
                </View>
              }
            />
          </View>
        </KeyboardAvoidingView>
      </View>
    </SafeAreaView>
  );
};

export default memo(CreateRecipe);
