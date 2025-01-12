import CreateRecipeDraggable from "@/components/screen/community/CreateRecipeDraggable";
import Input from "@/components/common/Input";
import UploadImage from "@/components/common/UploadImage";
import { FormCreateRecipeType } from "@/schemas/create-recipe";
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
  SafeAreaView
} from "react-native";
import DraggableIngredient from "@/components/screen/community/DraggableIngredient";

type renderIngredientItemProps = CreateIngredientType & {
  setIngredients: Dispatch<SetStateAction<CreateIngredientType[]>>;
};

const CreateRecipe = () => {
  const { t } = useTranslation("createRecipe");
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
    defaultValues: {
      title: "",
      description: "",
      serves: "",
      cookTime: ""
    }
  });

  const {
    control: formControl,
    formState: { errors },
    handleSubmit,
    resetField,
    getValues,
    setValue,
    setError,
    watch
  } = formCreateRecipe;

  const onSubmit: SubmitHandler<FormCreateRecipeType> = async formData => {
    console.log("go to submit", formData);
    const data = new FormData();

    data.append("title", formData.title);
    data.append("description", formData.description);
  };

  return (
    <SafeAreaView>
      <View className={`bg-white_black bg-red size-full flex-col`}>
        <View
          style={{ marginTop: StatusBar.currentHeight }}
          className='flex-between mb-4 h-[60px] flex-row border-b-[0.6px] border-gray-400 px-6'
        >
          <TouchableWithoutFeedback onPress={() => router.back()}>
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

          <TouchableWithoutFeedback onPress={handleSubmit(onSubmit)}>
            <View className='items-center'>
              <Text className='paragraph-medium text-primary'>{t("screens.action")}</Text>
            </View>
          </TouchableWithoutFeedback>
        </View>

        {/* Form */}
        <View className='flex-1 px-6'>
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
                      <View className='flex gap-9'>
                        <Text className='body-semibold'>{t("formTitle.serves")}</Text>
                        <Text className='body-semibold'>{t("formTitle.cookTime")}</Text>
                      </View>

                      <View className='flex-1 gap-2'>
                        <Input
                          variant='secondary'
                          control={formControl}
                          name='cookTime'
                          placeHolder={t("formPlaceholder.cookTime")}
                          errors={[t(errors.cookTime?.message ?? "")]}
                        />

                        <Input
                          variant='secondary'
                          control={formControl}
                          name='serves'
                          placeHolder={t("formPlaceholder.serves")}
                          errors={[t(errors.serves?.message ?? "")]}
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
      </View>
    </SafeAreaView>
  );
};

export default memo(CreateRecipe);
