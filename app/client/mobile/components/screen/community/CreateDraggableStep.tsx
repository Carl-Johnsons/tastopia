import {
  StyleSheet,
  View,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  Alert
} from "react-native";
import { AntDesign, MaterialCommunityIcons } from "@expo/vector-icons";
import { useCallback, useState } from "react";
import { globalStyles } from "@/components/common/GlobalStyles";
import { useTranslation } from "react-i18next";
import UploadImage from "@/components/common/UploadImage";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";
import { Controller, useFormContext } from "react-hook-form";
import { CreateRecipeFormValue } from "@/schemas/create-recipe";

interface CreateDraggableStepProps {
  index: number;
  stepKey: string;
  content: string;
  images: ImageFileType[];
  drag: () => void;
  remove: (index: number) => void;
}

const CreateDraggableStep = ({
  index,
  stepKey,
  content,
  images,
  drag,
  remove
}: CreateDraggableStepProps) => {
  const { c } = useColorizer();
  const { black, white } = colors;

  const { control, getValues, setValue } = useFormContext();
  const { t } = useTranslation("createRecipe");
  const [inputValue, setInputValue] = useState(content);
  const [isFocused, setIsFocused] = useState(false);

  const handleRemoveItem = useCallback(() => {
    if (index !== undefined) {
      remove(index);
    }
  }, [index, remove]);

  const confirmRemoveItem = () => {
    if (inputValue !== "") {
      Alert.alert(
        t("removeIngredientAlert.title"),
        t("removeIngredientAlert.description"),
        [
          {
            text: t("removeIngredientAlert.cancel")
          },
          {
            text: t("removeIngredientAlert.delete"),
            onPress: () => {
              handleRemoveItem();
            }
          }
        ]
      );
    } else {
      handleRemoveItem();
    }
  };

  const onFileChange = (files: ImageFileType[]) => {
    const steps = getValues("steps") ?? [];
    const updatedSteps: CreateRecipeFormValue["steps"] = steps.map(
      (step: NonNullable<CreateRecipeFormValue["steps"]>[number]) =>
        step.key === stepKey ? { ...step, images: files } : step
    );
    setValue("steps", updatedSteps);
  };

  return (
    <View style={[styles.container, { backgroundColor: c(white.DEFAULT, black[200]) }]}>
      <TouchableOpacity
        style={styles.iconContainer}
        onPress={confirmRemoveItem}
      >
        <AntDesign
          name='close'
          size={20}
          color={globalStyles.color.primary}
        />
      </TouchableOpacity>

      <View style={styles.inputContainer}>
        <Controller
          control={control}
          name={`steps.${index}.content`}
          render={({ field: { onChange, onBlur, value } }) => (
            <TextInput
              onFocus={() => setIsFocused(true)}
              onBlur={() => {
                onBlur();
                setIsFocused(false);
              }}
              style={
                (isFocused ? [styles.input, styles.inputFocused] : styles.input,
                { color: `${c(black.DEFAULT, white.DEFAULT)}` })
              }
              value={value}
              onChangeText={onChange}
              placeholder={t("formPlaceholder.steps")}
              placeholderTextColor='#9CA3AF'
            />
          )}
        />

        <UploadImage
          onFileChange={onFileChange}
          selectionLimit={3}
          defaultImages={images}
        />
      </View>

      <TouchableWithoutFeedback onLongPress={drag}>
        <View style={styles.dragHandle}>
          <MaterialCommunityIcons
            name='drag-vertical'
            size={24}
            color='#6B7280'
          />
        </View>
      </TouchableWithoutFeedback>
    </View>
  );
};

export default CreateDraggableStep;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: "row",
    alignItems: "flex-start",
    borderRadius: 8,
    marginVertical: 4,
    paddingHorizontal: 12,
    paddingVertical: 10,
    shadowOffset: {
      width: 0,
      height: 2
    },
    shadowOpacity: 0.1,
    shadowRadius: 3,
    elevation: 3
  },
  iconContainer: {
    padding: 8,
    justifyContent: "center",
    alignItems: "center"
  },
  inputContainer: {
    marginTop: 8,
    flex: 1,
    gap: 10,
    marginHorizontal: 8,
    paddingBottom: 4
  },
  input: {
    fontSize: 15,
    padding: 4,
    backgroundColor: "transparent",
    borderWidth: 0,
    borderBottomWidth: 1,
    borderBottomColor: globalStyles.color.gray400,
    borderRadius: 0,
    paddingHorizontal: 0
  },
  inputFocused: {
    borderBottomColor: globalStyles.color.primary
  },
  dragHandle: {
    padding: 8,
    justifyContent: "center",
    alignItems: "center"
  }
});
