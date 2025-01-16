import React, { Dispatch, ReactNode, SetStateAction, useCallback, useState } from "react";
import { Text, View, TouchableWithoutFeedback } from "react-native";
import DraggableFlatList, {
  RenderItemParams,
  ScaleDecorator
} from "react-native-draggable-flatlist";
import { Entypo } from "@expo/vector-icons";
import { useTranslation } from "react-i18next";
import DraggableIngredient from "./DraggableIngredient";
import uuid from "react-native-uuid";
import DraggableStep from "./DraggableStep";
import TagList from "./TagList";

type DraggableProps = {
  ingredients: CreateIngredientType[];
  setIngredients: Dispatch<SetStateAction<CreateIngredientType[]>>;
  steps: CreateStepType[];
  setSteps: Dispatch<SetStateAction<CreateStepType[]>>;
  selectedTags: SelectedTag[];
  setSelectedTags: Dispatch<SetStateAction<SelectedTag[]>>;
  form: ReactNode;
};

export default function CreateRecipeDraggable({
  ingredients,
  setIngredients,
  steps,
  setSteps,
  selectedTags,
  setSelectedTags,
  form
}: DraggableProps) {
  const { t } = useTranslation("createRecipe");

  const [query, setQuery] = useState("");
  const data = [
    {
      key: "1",
      value: 1
    },
    {
      key: "2",
      value: 2
    }
  ];

  const renderIngredientItem = useCallback(
    ({ item, drag, isActive }: RenderItemParams<CreateIngredientType>) => {
      return (
        <ScaleDecorator>
          <DraggableIngredient
            key={item.key}
            ingredientKey={item.key}
            value={item.value}
            setIngredients={setIngredients}
          />
        </ScaleDecorator>
      );
    },
    [setIngredients]
  );

  const renderStepItem = useCallback(
    ({ item, drag, isActive }: RenderItemParams<CreateStepType>) => {
      return (
        <ScaleDecorator>
          <DraggableStep
            key={item.key}
            stepKey={item.key}
            content={item.content}
            images={item.images}
            drag={drag}
            setSteps={setSteps}
          />
        </ScaleDecorator>
      );
    },
    [setIngredients]
  );

  const handleAddMoreIngredient = () => {
    setIngredients(prev => [...prev, { key: uuid.v4(), value: "" }]);
  };

  const handleAddMoreStep = () => {
    setSteps(prev => [...prev, { key: uuid.v4(), content: "", images: [] }]);
  };

  return (
    <View className='gap-4'>
      <DraggableFlatList
        key={"draggable-flat-list-create-ingredients"}
        data={ingredients}
        onDragEnd={({ data }) => setIngredients(data)}
        keyExtractor={item => item.key}
        renderItem={renderIngredientItem}
        showsVerticalScrollIndicator={false}
        ListHeaderComponent={() => {
          return form;
        }}
        ListFooterComponent={() => {
          return (
            <View className='mb-[100px] h-full flex-1'>
              <View className='flex-center mt-2'>
                <TouchableWithoutFeedback onPress={handleAddMoreIngredient}>
                  <View className='flex-center flex-row gap-1'>
                    <Entypo
                      name='plus'
                      size={24}
                      color='black'
                    />
                    <Text className='body-semibold'>{t("formTitle.ingredients")}</Text>
                  </View>
                </TouchableWithoutFeedback>
              </View>

              <View className='mt-4'>
                <Text className='body-semibold mb-2'>{t("formTitle.method")}</Text>
                <DraggableFlatList
                  key={"draggable-flat-list-create-steps"}
                  data={steps}
                  onDragEnd={({ data }) => setSteps(data)}
                  keyExtractor={item => item.key}
                  renderItem={renderStepItem}
                  showsVerticalScrollIndicator={false}
                  ListFooterComponent={() => {
                    return (
                      <View className='flex-center mt-2'>
                        <TouchableWithoutFeedback onPress={handleAddMoreStep}>
                          <View className='flex-center flex-row gap-1'>
                            <Entypo
                              name='plus'
                              size={24}
                              color='black'
                            />
                            <Text className='body-semibold'>{t("formTitle.step")}</Text>
                          </View>
                        </TouchableWithoutFeedback>
                      </View>
                    );
                  }}
                />
              </View>

              <View className='mt-4'>
                <Text className='body-semibold mb-2'>{t("formTitle.tag")}</Text>
                <TagList
                  selectedTags={selectedTags}
                  setSelectedTags={setSelectedTags}
                />
              </View>
            </View>
          );
        }}
      />
    </View>
  );
}
