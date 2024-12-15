import {
  Pressable,
  StyleProp,
  Text,
  TextStyle,
  TouchableNativeFeedback,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View,
  ViewStyle
} from "react-native";
import React, { ReactElement } from "react";
import { StyleSheet } from "react-native";
import { TouchableHighlight } from "react-native-gesture-handler";
import { globalStyles } from "../GlobalStyles";
import PagoLoader from "../PagoLoader";

type PagoButtonProps = {
  title: string;
  handlePress?: () => void;
  isLoading?: boolean;
  disabled?: boolean;
  type?:
    | "button"
    | "touchableHighlight"
    | "touchableOpacity"
    | "touchableNativeFeedback"
    | "touchableWithoutFeedback"
    | "iconButton";
  color?: "primary" | "secondary" | "danger" | "warning" | "disable";
  leftIcon?: ReactElement;
  rightIcon?: ReactElement;
  iconButton?: boolean;
  onPress?: any;
  buttonStyle?: StyleProp<ViewStyle>;
  containerStyle?: StyleProp<ViewStyle>;
  textStyles?: StyleProp<TextStyle>;
  width?: 25 | 50 | 75 | 100;
};

const PagoButton = ({
  title,
  handlePress,
  type = "button",
  width,
  color,
  buttonStyle,
  textStyles,
  containerStyle,
  isLoading,
  leftIcon,
  rightIcon,
  onPress,
  ...props
}: PagoButtonProps) => {
  let buttonColor = globalStyles.color.primary;
  let underlayColor = globalStyles.color.primaryOpacity;

  switch (color) {
    case "primary":
      buttonColor = globalStyles.color.primary;
      underlayColor = globalStyles.color.secondaryDark;
      break;
    case "secondary":
      buttonColor = globalStyles.color.secondary;
      underlayColor = globalStyles.color.secondaryOpacity;
      break;
    case "warning":
      buttonColor = globalStyles.color.warning;
      underlayColor = globalStyles.color.warningOpacity;
      break;
    case "danger":
      buttonColor = globalStyles.color.danger;
      underlayColor = globalStyles.color.dangerOpacity;
      break;
    case "disable":
      buttonColor = globalStyles.color.disable;
      underlayColor = globalStyles.color.disableOpacity;

      break;
    default:
      buttonColor = globalStyles.color.primary;
      underlayColor = globalStyles.color.primaryOpacity;
      break;
  }

  if (props.disabled) {
    // Turn off button if disabled
    buttonColor = globalStyles.color.disable;
    underlayColor = globalStyles.color.disableOpacity;
  }

  return (
    <View
      style={[
        styles.buttonWrapper,
        width && { width: `${width}%` },
        containerStyle && containerStyle
      ]}
    >
      {type === "iconButton" ? (
        <Pressable
          onPress={onPress}
          {...props}
          disabled={props.disabled || isLoading}
          style={({ pressed }) => [
            {
              opacity: pressed ? 0.8 : 1
            },
            { backgroundColor: buttonColor },
            buttonStyle && buttonStyle
          ]}
        >
          <View style={[styles.iconButton, styles.alignIcon, buttonStyle && buttonStyle]}>
            {isLoading ? (
              <View>
                <PagoLoader
                  useFor='component'
                  loaderStyle={{ backgroundColor: "transparent" }}
                  size='small'
                  loaderColor={globalStyles.color.light}
                />
                <Text style={[styles.buttonText, textStyles && textStyles]}>{}</Text>
              </View>
            ) : (
              <Text style={styles.icon}>{leftIcon || rightIcon}</Text>
            )}
          </View>
        </Pressable>
      ) : (
        <View>
          {type === "touchableHighlight" && (
            <TouchableHighlight
              underlayColor={underlayColor}
              onPress={onPress}
              {...props}
              disabled={props.disabled || isLoading}
              style={[{ padding: 0 }, buttonStyle && buttonStyle]}
            >
              <View
                style={[
                  styles.button,
                  styles.alignIcon,
                  { backgroundColor: buttonColor },
                  buttonStyle && buttonStyle
                ]}
              >
                <PagoButtonTitle
                  isLoading={isLoading}
                  title={title}
                  leftIcon={leftIcon}
                  rightIcon={rightIcon}
                  textStyles={textStyles}
                />
              </View>
            </TouchableHighlight>
          )}

          {type === "touchableOpacity" && (
            <TouchableOpacity
              onPress={onPress}
              {...props}
              disabled={props.disabled || isLoading}
            >
              <View
                style={[
                  styles.button,
                  styles.alignIcon,
                  { backgroundColor: buttonColor },
                  buttonStyle && buttonStyle
                ]}
              >
                <PagoButtonTitle
                  isLoading={isLoading}
                  title={title}
                  leftIcon={leftIcon}
                  rightIcon={rightIcon}
                  textStyles={textStyles}
                />
              </View>
            </TouchableOpacity>
          )}

          {type === "touchableWithoutFeedback" && (
            <TouchableWithoutFeedback
              onPress={onPress}
              style={[buttonStyle && buttonStyle]}
              {...props}
              disabled={props.disabled || isLoading}
            >
              <View
                style={[
                  styles.button,
                  styles.alignIcon,
                  { backgroundColor: buttonColor },
                  buttonStyle && buttonStyle
                ]}
              >
                <PagoButtonTitle
                  isLoading={isLoading}
                  title={title}
                  leftIcon={leftIcon}
                  rightIcon={rightIcon}
                  textStyles={textStyles}
                />
              </View>
            </TouchableWithoutFeedback>
          )}

          {type === "touchableNativeFeedback" && (
            <TouchableNativeFeedback
              onPress={onPress}
              style={[buttonStyle && buttonStyle]}
              {...props}
              disabled={props.disabled || isLoading}
            >
              <View
                style={[
                  styles.button,
                  styles.alignIcon,
                  { backgroundColor: buttonColor },
                  buttonStyle && buttonStyle
                ]}
              >
                <PagoButtonTitle
                  isLoading={isLoading}
                  title={title}
                  leftIcon={leftIcon}
                  rightIcon={rightIcon}
                  textStyles={textStyles}
                />
              </View>
            </TouchableNativeFeedback>
          )}

          {type === "button" && (
            <Pressable
              onPress={onPress}
              {...props}
              disabled={props.disabled || isLoading}
              style={({ pressed }) => [
                {
                  opacity: pressed ? 0.8 : 1
                }
              ]}
            >
              <View
                style={[
                  styles.button,
                  styles.alignIcon,
                  { backgroundColor: buttonColor },
                  buttonStyle && buttonStyle
                ]}
              >
                <PagoButtonTitle
                  isLoading={isLoading}
                  title={title}
                  leftIcon={leftIcon}
                  rightIcon={rightIcon}
                  textStyles={textStyles}
                />
              </View>
            </Pressable>
          )}
        </View>
      )}
    </View>
  );
};

const PagoButtonTitle = ({
  isLoading,
  title,
  leftIcon,
  rightIcon,
  textStyles
}: PagoButtonProps) => {
  return (
    <>
      {isLoading ? (
        <View>
          <PagoLoader
            useFor='component'
            loaderStyle={{ backgroundColor: "transparent" }}
            size='small'
            loaderColor={globalStyles.color.light}
          />
          <Text
            style={[
              styles.buttonText,
              textStyles && textStyles,
              { color: "transparent" }
            ]}
          >
            {title}
          </Text>
        </View>
      ) : (
        <>
          {leftIcon}
          {title && (
            <Text style={[styles.buttonText, textStyles && textStyles]}>{title}</Text>
          )}
          {rightIcon}
        </>
      )}
    </>
  );
};

const styles = StyleSheet.create({
  buttonWrapper: {
    shadowColor: "#ccc",
    borderRadius: 7,
    shadowOffset: { width: 2, height: 1.5 },
    shadowOpacity: 1,
    overflow: "hidden"
  },
  button: {
    paddingHorizontal: 5,
    paddingVertical: 5,
    alignItems: "center"
  },
  buttonText: {
    textAlign: "center",
    paddingHorizontal: 5,
    fontSize: 18,
    color: "white"
  },
  alignIcon: {
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center"
  },
  iconButton: {
    width: 30,
    height: 30,
    borderRadius: 50
  },
  icon: {
    padding: 1,
    textAlign: "center"
  }
});

export default PagoButton;
