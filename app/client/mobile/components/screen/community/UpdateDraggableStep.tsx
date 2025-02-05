import {
  StyleSheet,
  View,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  Alert
} from "react-native";
import { AntDesign, MaterialCommunityIcons } from "@expo/vector-icons";
import { Dispatch, SetStateAction, useEffect, useState } from "react";
import { globalStyles } from "@/components/common/GlobalStyles";
import { useTranslation } from "react-i18next";
import useDebounce from "@/hooks/useDebounce";
import UploadImage from "@/components/common/UploadImage";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";
import UpdateImage from "@/components/common/UpdateImage";

interface UpdateDraggableStepProps {
  stepKey: string;
  content: string;
  images: UpdateImage;
  drag: () => void;
  setSteps: Dispatch<SetStateAction<UpdateStepType[]>>;
}

const UpdateDraggableStep = ({
  stepKey,
  content,
  images,
  drag,
  setSteps
}: UpdateDraggableStepProps) => {
  const { c } = useColorizer();
  const { black, white } = colors;

  const { t } = useTranslation("createRecipe");
  const [inputValue, setInputValue] = useState(content);
  const [isFocused, setIsFocused] = useState(false);
  const debouncedValue = useDebounce(inputValue, 800);

  const handleChangeText = (text: string) => {
    setInputValue(text);
  };

  const handleRemoveItem = (key: string) => {
    setSteps(prev => {
      return prev.filter(item => {
        return item.key !== key;
      });
    });
  };

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
              handleRemoveItem(stepKey);
            }
          }
        ]
      );
    } else {
      handleRemoveItem(stepKey);
    }
  };

  const onAddImage = (files: ImageFileType[]) => {
    setSteps(prevSteps =>
      prevSteps.map(step =>
        step.key === stepKey
          ? { ...step, images: { ...step.images, additionalImages: files } }
          : step
      )
    );
  };

  const onDeleteImage = (deleteImageUrl: string) => {
    setSteps(prevSteps =>
      prevSteps.map(step =>
        step.key === stepKey
          ? {
              ...step,
              images: {
                ...step.images,
                deleteUrls: [...step.images.deleteUrls!, deleteImageUrl]
              }
            }
          : step
      )
    );
  };

  useEffect(() => {
    if (debouncedValue !== content) {
      setSteps(prevSteps =>
        prevSteps.map(step =>
          step.key === stepKey ? { ...step, content: debouncedValue } : step
        )
      );
    }
  }, [debouncedValue, stepKey, setSteps]);

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
        <TextInput
          onFocus={() => setIsFocused(true)}
          onBlur={() => setIsFocused(false)}
          style={
            (isFocused ? [styles.input, styles.inputFocused] : styles.input,
            { color: `${c(black.DEFAULT, white.DEFAULT)}` })
          }
          value={inputValue}
          onChangeText={handleChangeText}
          placeholder={t("formPlaceholder.steps")}
          placeholderTextColor='#9CA3AF'
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
