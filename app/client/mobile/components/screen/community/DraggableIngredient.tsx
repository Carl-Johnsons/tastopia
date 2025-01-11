import { StyleSheet, Text, View, TextInput, TouchableOpacity } from "react-native";
import { AntDesign, MaterialCommunityIcons } from "@expo/vector-icons";
import { useState } from "react";

interface DraggableIngredientProps {
  count: number;
  onRemove?: (index: number) => void;
  onChangeText?: (text: string, index: number) => void;
  value?: string;
}

const DraggableIngredient = ({
  count,
  onRemove,
  onChangeText,
  value = ""
}: DraggableIngredientProps) => {
  const [inputValue, setInputValue] = useState(value);

  const handleChangeText = (text: string) => {
    setInputValue(text);
    onChangeText?.(text, count);
  };

  return (
    <View style={styles.container}>
      <TouchableOpacity
        style={styles.iconContainer}
        onPress={() => onRemove?.(count)}
      >
        <AntDesign
          name='close'
          size={20}
          color='#FF4B4B'
        />
      </TouchableOpacity>

      <View style={styles.inputContainer}>
        <TextInput
          style={styles.input}
          value={inputValue}
          onChangeText={handleChangeText}
          placeholder='Enter ingredient...'
          placeholderTextColor='#9CA3AF'
        />
      </View>

      <View style={styles.dragHandle}>
        <MaterialCommunityIcons
          name='drag-vertical'
          size={24}
          color='#6B7280'
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
    fontSize: 16,
    color: "#1F2937",
    padding: 8,
    backgroundColor: "#F9FAFB",
    borderRadius: 6
  },
  dragHandle: {
    padding: 8,
    justifyContent: "center",
    alignItems: "center"
  }
});
