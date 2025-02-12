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
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";
import UpdateImage from "@/components/common/UpdateImage";
import { isOnlineImage } from "@/utils/file";
import { Controller, useFormContext } from "react-hook-form";
import { UpdateRecipeFormValue } from "@/schemas/update-recipe";

interface UpdateDraggableStepProps {
  index: number;
  stepKey: string;
  content: string;
  images: UpdateImage;
  drag: () => void;
  remove: (index: number) => void;
}

const UpdateDraggableStep = ({
  index,
  stepKey,
  content,
  images,
  drag,
  remove
}: UpdateDraggableStepProps) => {
  const { c } = useColorizer();
  const { black, white } = colors;

  const { control, getValues, setValue } = useFormContext();
  const { t } = useTranslation("createRecipe");
  const [isFocused, setIsFocused] = useState(false);
  const steps = getValues("steps");

  const handleRemoveItem = useCallback(() => {
    if (index !== undefined) {
      remove(index);
    }
  }, [index, remove]);

  const confirmRemoveItem = () => {
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
  };

  const onAddImage = (files: ImageFileType[]) => {
    const updatedSteps: UpdateRecipeFormValue["steps"] = steps.map(
      (step: NonNullable<UpdateRecipeFormValue["steps"]>[number]) =>
        step.key === stepKey
          ? { ...step, images: { ...step.images, additionalImages: files } }
          : step
    );
    setValue("steps", updatedSteps);
  };

  const onDeleteImage = (deleteImageUrl: string) => {
    if (isOnlineImage(deleteImageUrl)) {
      const updatedSteps: UpdateRecipeFormValue["steps"] = steps.map(
        (step: NonNullable<UpdateRecipeFormValue["steps"]>[number]) =>
          step.key === stepKey
            ? {
                ...step,
                images: {
                  ...step.images,
                  deleteUrls: [...step.images.deleteUrls!, deleteImageUrl]
                }
              }
            : step
      );
      setValue("steps", updatedSteps);
    } else {
      const updatedSteps: UpdateRecipeFormValue["steps"] = steps.map(
        (step: NonNullable<UpdateRecipeFormValue["steps"]>[number]) =>
          step.key === stepKey
            ? {
                ...step,
                images: {
                  ...step.images,
                  additionalImages: step.images?.additionalImages?.filter(
                    image => image.uri !== deleteImageUrl
                  )
                }
              }
            : step
      );
      setValue("steps", updatedSteps);
    }
  };

  return (
    <View style={[styles.container, { backgroundColor: c(white.DEFAULT, black[200]) }]}>
      {steps.length > 1 && (
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
      )}

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

        <UpdateImage
          onAddImage={onAddImage}
          onDeleteImage={onDeleteImage}
          selectionLimit={3}
          images={images}
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

export default UpdateDraggableStep;

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
