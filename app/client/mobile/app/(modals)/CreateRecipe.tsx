import Draggable from "@/components/common/Draggable";
import { globalStyles } from "@/components/common/GlobalStyles";
import Input from "@/components/common/Input";
import UploadImage from "@/components/common/UploadImage";
import DraggableIngredient from "@/components/screen/community/DraggableIngredient";
import { FormCreateRecipeType } from "@/schemas/create-recipe";
import { ImageFileType } from "@/types/image";
import { AntDesign } from "@expo/vector-icons";
import { router } from "expo-router";
import { useState } from "react";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import {
  ScrollView,
  StatusBar,
  Text,
  TouchableWithoutFeedback,
  View,
  Dimensions
} from "react-native";
import { useSharedValue } from "react-native-reanimated";
import { SafeAreaView } from "react-native-safe-area-context";

const INITIAL_ITEMS = 3;
const ITEM_HEIGHT = 50;

const CreateRecipe = () => {
  const { t } = useTranslation("createRecipe");
  const [text, setText] = useState<string>("");
  const [isClosing, setIsClosing] = useState<boolean>(false);
  const [images, setImages] = useState<ImageFileType[]>([]);
  const [ingredients, setIngredients] = useState<Ingredient[]>([{ id: "1", value: "" }]);
  const [ingredientValues, setIngredientValues] = useState<string[]>(
    Array(INITIAL_ITEMS).fill("")
  );

  const positions = useSharedValue(
    Object.assign(
      {},
      ...Array(INITIAL_ITEMS)
        .fill(0)
        .map((_, index) => ({
          [index]: index
        }))
    )
  );

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

  const handleClickCreate = () => {};

  const handlePositionChange = (newPositions: { [key: string]: number }) => {
    console.log("New positions:", newPositions);
  };

  const handleRemoveIngredient = (index: number) => {
    // Handle removal logic here
    console.log(`Remove ingredient at index ${index}`);
  };

  const handleChangeIngredient = (text: string, index: number) => {
    const newValues = [...ingredientValues];
    newValues[index] = text;
    setIngredientValues(newValues);
  };

  return (
    <SafeAreaView>
      <View className={`bg-white_black size-full flex-col`}>
        <View
          style={{ marginTop: StatusBar.currentHeight }}
          className='flex-between mb-4 h-[60px] flex-row border-b-[0.6px] border-gray-400 px-6'
        >
          <TouchableWithoutFeedback
            onPress={() => (text.length > 0 ? setIsClosing(true) : router.back())}
          >
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

          <TouchableWithoutFeedback onPress={handleClickCreate}>
            <View className='items-center'>
              <Text className='paragraph-medium text-primary'>{t("screens.action")}</Text>
            </View>
          </TouchableWithoutFeedback>
        </View>

        {/* Form */}
        <View className='px-6'>
          <ScrollView showsVerticalScrollIndicator={false}>
            <FormProvider {...formCreateRecipe}>
              <View className='d-flex justify-center gap-4'>
                <View className='my-5'>
                  <UploadImage
                    onFileChange={setImages}
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
                  <Text className='body-semibold'>{t("formTitle.ingredients")}</Text>

                  <View
                    style={{
                      width: "100%",
                      height: INITIAL_ITEMS * ITEM_HEIGHT,

                      position: "relative"
                    }}
                  >
                    {Array(INITIAL_ITEMS)
                      .fill(0)
                      .map((_, index) => (
                        <Draggable
                          key={index}
                          id={index}
                          positions={positions}
                          itemCount={INITIAL_ITEMS}
                          boxBounds={{
                            width: Dimensions.get("window").width - 32,
                            height: INITIAL_ITEMS * ITEM_HEIGHT
                          }}
                        >
                          <DraggableIngredient
                            count={index}
                            onRemove={handleRemoveIngredient}
                            onChangeText={handleChangeIngredient}
                            value={ingredientValues[index]}
                          />
                        </Draggable>
                      ))}
                  </View>
                </View>
              </View>
            </FormProvider>
          </ScrollView>
        </View>
      </View>
    </SafeAreaView>
  );
};

export default CreateRecipe;
