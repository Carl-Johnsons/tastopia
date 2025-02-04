import { Dispatch, ReactNode, SetStateAction, useCallback } from "react";
import { Text, View, TouchableWithoutFeedback, FlatList } from "react-native";
import DraggableFlatList, {
  RenderItemParams,
  ScaleDecorator
} from "react-native-draggable-flatlist";
import { Entypo } from "@expo/vector-icons";
import { useTranslation } from "react-i18next";
import DraggableIngredient from "./CreateIngredient";
import uuid from "react-native-uuid";
import CreateDraggableStep from "./CreateDraggableStep";
import TagList from "./TagList";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";

type DraggableProps = {
  steps: CreateStepType[];
  setSteps: Dispatch<SetStateAction<CreateStepType[]>>;
  selectedTags: SelectedTag[];
  setSelectedTags: Dispatch<SetStateAction<SelectedTag[]>>;
  form: ReactNode;
};

function CreateStepDraggable({
  steps,
  setSteps,
  selectedTags,
  setSelectedTags
}: DraggableProps) {
  const { c } = useColorizer();
  const { black, white } = colors;
  const { t } = useTranslation("createRecipe");

  const renderStepItem = useCallback(
    ({ item, drag, isActive }: RenderItemParams<CreateStepType>) => {
      return (
        <ScaleDecorator>
          <CreateDraggableStep
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
    [setSelectedTags]
  );

  const handleAddMoreStep = () => {
    setSteps(prev => [...prev, { key: uuid.v4(), content: "", images: [] }]);
  };

  return (
    <View className='mt-4'>
      <Text className='body-semibold text-black_white mb-2'>{t("formTitle.method")}</Text>
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
                    color={c(black.DEFAULT, white.DEFAULT)}
                  />
                  <Text className='body-semibold text-black_white'>
                    {t("formTitle.step")}
                  </Text>
                </View>
              </TouchableWithoutFeedback>
            </View>
          );
        }}
      />
    </View>
  );
}

export default CreateStepDraggable;
