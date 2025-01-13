import { StyleSheet, View, TextInput, TouchableOpacity, Alert } from "react-native";
import { AntDesign } from "@expo/vector-icons";
import { Dispatch, SetStateAction, useEffect, useState } from "react";
import { globalStyles } from "@/components/common/GlobalStyles";
import { useTranslation } from "react-i18next";
import useDebounce from "@/hooks/useDebounce";

interface DraggableIngredientProps {
  ingredientKey: string;
  value: string;
  setIngredients: Dispatch<SetStateAction<CreateIngredientType[]>>;
}

const DraggableIngredient = ({
  ingredientKey,
  value,
  setIngredients
}: DraggableIngredientProps) => {
  const { t } = useTranslation("createRecipe");
  const [inputValue, setInputValue] = useState(value);
  const [isFocused, setIsFocused] = useState(false);
  const debouncedValue = useDebounce(inputValue, 800);

  const handleChangeText = (text: string) => {
    setInputValue(text);
  };

  const handleRemoveItem = (key: string) => {
    setIngredients(prev => {
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
              handleRemoveItem(ingredientKey);
            }
          }
        ]
      );
    } else {
      handleRemoveItem(ingredientKey);
    }
  };

  useEffect(() => {
    if (debouncedValue !== value) {
      setIngredients(prevIngredients =>
        prevIngredients.map(ingredient =>
          ingredient.key === ingredientKey
            ? { ...ingredient, value: debouncedValue }
            : ingredient
        )
      );
    }
  }, [debouncedValue, ingredientKey, setIngredients]);

  return (
    <View style={styles.container}>
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
          style={isFocused ? [styles.input, styles.inputFocused] : styles.input}
          value={inputValue}
          onChangeText={handleChangeText}
          placeholder={t("formPlaceholder.ingredients")}
          placeholderTextColor='#9CA3AF'
        />
      </View>
    </View>
  );
};

export default DraggableIngredient;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: "row",
    alignItems: "center",
    backgroundColor: "white",
    borderRadius: 8,
    marginVertical: 4,
    paddingHorizontal: 12,
    paddingVertical: 8,
    shadowColor: "#000",
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
    flex: 1,
    marginHorizontal: 8
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
