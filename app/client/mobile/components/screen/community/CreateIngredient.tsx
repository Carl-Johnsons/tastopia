import { StyleSheet, View, TextInput, TouchableOpacity, Alert } from "react-native";
import { AntDesign } from "@expo/vector-icons";
import { Dispatch, SetStateAction, useCallback, useEffect, useState } from "react";
import { globalStyles } from "@/components/common/GlobalStyles";
import { useTranslation } from "react-i18next";
import useDebounce from "@/hooks/useDebounce";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";
import { Control, Controller, FieldErrors, useFormContext } from "react-hook-form";
import ErrorValidationMessages from "@/components/common/ErrorValidationMessages";

interface DraggableIngredientProps {
  index: number;
  remove: (index: number) => void;
}

const CreateIngredient = ({ index, remove }: DraggableIngredientProps) => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const { t } = useTranslation("createRecipe");
  const [isFocused, setIsFocused] = useState(false);

  const { control } = useFormContext();

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
          onPress: handleRemoveItem
        }
      ]
    );
  };

  return (
    <View style={[styles.container, { backgroundColor: c(white.DEFAULT, black[200]) }]}>
      {/* Remove button */}
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

      {/* Input field */}
      <View style={styles.inputContainer}>
        <Controller
          control={control}
          name={`ingredients.${index}.value`}
          render={({ field: { onChange, onBlur, value } }) => (
            <TextInput
              onFocus={() => setIsFocused(true)}
              onBlur={() => {
                onBlur();
                setIsFocused(false);
              }}
              style={[
                styles.input,
                isFocused && styles.inputFocused,
                { color: c(black.DEFAULT, white.DEFAULT) }
              ]}
              value={value}
              onChangeText={onChange}
              placeholder={t("formPlaceholder.ingredients")}
              placeholderTextColor='#9CA3AF'
            />
          )}
        />
      </View>
    </View>
  );
};

export default CreateIngredient;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "center",
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
    justifyContent: "center",
    marginHorizontal: 8,
    minHeight: 34
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
