import {
  GestureResponderEvent,
  Platform,
  StyleProp,
  StyleSheet,
  TouchableHighlight,
  View,
  ViewStyle
} from "react-native";
import React, { ReactNode, useEffect, useState } from "react";
import { Feather } from "@expo/vector-icons";

import { globalStyles } from "../GlobalStyles";
import { addAlpha } from "@/utils/color";

type OnPressParam = {
  event: GestureResponderEvent;
  selected: boolean;
};

type Props = {
  // Required
  renderItem: ReactNode;
  // Variants
  bordered?: boolean;
  disableSelected?: boolean;
  selected?: boolean;
  selectedIcon?: ReactNode;
  shadow?: boolean;
  // Events
  onPress?: (param: OnPressParam) => void;
  // Style
  containerStyle?: StyleProp<ViewStyle>;
  itemContainerStyle?: StyleProp<ViewStyle>;
  selectItemStyle?: StyleProp<ViewStyle>;
};

const SelectableItem = ({
  // Required
  renderItem,
  // Variants
  bordered = false,
  disableSelected = false,
  selected = false,
  selectedIcon = (
    <Feather
      name='check'
      size={60}
      color='#FFF'
    />
  ),
  shadow = false,
  // Events
  onPress = () => {},
  // Style
  containerStyle,
  itemContainerStyle,
  selectItemStyle
}: Props) => {
  const [isSelected, setIsSelected] = useState<boolean>(selected);

  useEffect(() => {
    setIsSelected(selected);
  }, [selected]);

  return (
    <View
      style={[
        styles.container,
        { ...(bordered && { borderWidth: 2 }) },
        {
          ...(shadow && {
            ...(Platform.OS === "ios"
              ? {
                  shadowOffset: { width: 0, height: 5 },
                  shadowOpacity: 0.1,
                  shadowRadius: 3
                }
              : {
                  elevation: 15
                })
          })
        },
        containerStyle
      ]}
    >
      <TouchableHighlight
        onPress={e => {
          if (!disableSelected && !selected) {
            setIsSelected(prev => !prev);
          }
          onPress({ event: e, selected: isSelected });
        }}
        underlayColor='#D9D9D9'
        {...(disableSelected && { activeOpacity: 1 })}
      >
        <>
          <View style={[styles.itemContainer, itemContainerStyle]}>{renderItem}</View>
          <View
            style={[styles.overlay, isSelected ? styles.selectItem : {}, selectItemStyle]}
          >
            {selectedIcon}
          </View>
        </>
      </TouchableHighlight>
    </View>
  );
};

export default SelectableItem;

const styles = StyleSheet.create({
  container: {
    width: 100,
    height: 100,
    marginBottom: 10,
    borderRadius: 8,
    overflow: "hidden",
    borderColor: "#D9D9D9",
    backgroundColor: "#fff"
  },

  itemContainer: {
    width: "100%",
    height: "100%",
    display: "flex",
    alignItems: "center",
    justifyContent: "center"
  },

  overlay: {
    ...StyleSheet.absoluteFillObject,
    opacity: 0,
    backgroundColor: addAlpha(globalStyles.color.secondary, 0.8),
    justifyContent: "center",
    alignItems: "center"
  },

  selectItem: { opacity: 1 }
});
