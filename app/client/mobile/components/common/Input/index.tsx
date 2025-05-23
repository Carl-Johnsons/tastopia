import { useCallback, useRef, useState } from "react";
import { Controller } from "react-hook-form";
import {
  StyleProp,
  Text,
  TextInput,
  TextStyle,
  TouchableOpacity,
  View
} from "react-native";
import DateTimePicker from "@react-native-community/datetimepicker";
import Feather from "@expo/vector-icons/Feather";

import FormGroup from "../FormGroup";
import Fragment from "../Fragment";
import ErrorValidationMessages from "../ErrorValidationMessages";
import styles from "./Input.style";
import { FontAwesome6 } from "@expo/vector-icons";
import { globalStyles } from "../GlobalStyles";
import Loader from "../Loader";
import { formatDate } from "@/utils/format-date";
import { isFalsy, isNumeric as checkIfNumber } from "@/utils/functions";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";

/**
 * - value: if value is Date type, must be in type string and in format "yyyy-MM-dd"
 */
interface InputTypes {
  variant?: "primary" | "secondary";
  name: string;
  control?: any;
  disabled?: boolean;
  isLoading?: boolean;
  errors?: string[];
  label?: string;
  formGroup?: boolean;
  placeHolder?: string;
  labelStyle?: StyleProp<TextStyle>;
  inputStyle?: StyleProp<TextStyle>;
  onChange?: (event: any) => void;
  onChangeText?: (event: any) => void;
  onFocus?: (event: any) => void;
  onBlur?: (event: any) => void;
  value?: any;
  isPassword?: boolean;
  isNumeric?: boolean;
  isDate?: boolean;
  [key: string]: any;
}

interface CustomProps {
  onChange?: (event: any) => void;
  onChangeText?: (event: any) => void;
  onFocus?: (event: any) => void;
  onBlur?: (event: any) => void;
  value?: any;
}

const Input = ({
  variant = "primary",
  name,
  control,
  disabled = false,
  isLoading,
  errors,
  label,
  formGroup,
  labelStyle,
  placeHolder,
  inputStyle,
  isPassword,
  isNumeric = false,
  isDate = false,
  onChange,
  onChangeText,
  onFocus,
  onBlur,
  value,
  multiline = false,
  ...restProps
}: InputTypes) => {
  const { c } = useColorizer();
  const { black, white } = colors;

  const [passwordVisible, setPasswordVisible] = useState(false);
  const [isFocused, setIsFocused] = useState(false);
  // For date picker only
  const [showDatePicker, setShowDatePicker] = useState(false);

  const inputRef = useRef<TextInput>(null);

  const HasFormGroupElement = formGroup ? FormGroup : Fragment;
  const WrapperInputPasswordElement = isPassword ? View : Fragment;

  const customProps: CustomProps = {};

  if (onChange !== undefined) {
    customProps.onChange = onChange!;
  }
  if (onChangeText !== undefined) {
    customProps.onChangeText = onChangeText!;
  }
  if (onFocus !== undefined) {
    customProps.onFocus = onFocus!;
  }
  if (onBlur !== undefined) {
    customProps.onBlur = onBlur!;
  }
  if (!isFalsy(value)) {
    customProps.value = value!;
  }

  const handleDateChange = (event: any, selectedDate?: Date) => {
    setShowDatePicker(false);
    setIsFocused(false);
    inputRef.current?.blur();
    const currentDate = selectedDate || value;
    const formattedDate = formatDate((currentDate as Date).toISOString());
    inputRef.current?.setNativeProps({ text: formattedDate });
    customProps.onChange?.(currentDate);
  };

  const getInputStyles = useCallback(() => {
    const baseStyles = [
      styles.input,
      inputStyle && inputStyle,
      isFocused && styles.inputFocused,
      isPassword && styles.inputPassword,
      {
        backgroundColor: disabled ? "#e9ecef" : globalStyles.color.light,
        color: `${c(black.DEFAULT, white.DEFAULT)}`,
        minHeight: 40
      }
    ];

    if (variant === "secondary") {
      baseStyles.push(styles.secondaryInput);
      if (isFocused) {
        baseStyles.push(styles.secondaryInputFocused);
      }
    }

    return baseStyles;
  }, [isFocused]);

  return (
    <HasFormGroupElement>
      {label && (
        <Text
          style={[
            styles.label,
            labelStyle,
            {
              color: disabled ? "#939393" : globalStyles.color.secondary
            }
          ]}
        >
          {label}
        </Text>
      )}

      <View style={[styles.wrapperInput]}>
        {control ? (
          <WrapperInputPasswordElement style={styles.passwordInputContainer}>
            <Controller
              control={control}
              rules={{ required: true }}
              name={name}
              render={({ field: { onChange, onBlur, value } }) => {
                const dateValue = new Date(value) || new Date();

                return (
                  <>
                    {showDatePicker && (
                      <DateTimePicker
                        disabled={disabled}
                        value={isNaN(dateValue.getTime()) ? new Date() : dateValue}
                        mode='date'
                        display='default'
                        onChange={(e, date) => {
                          handleDateChange(e, date);
                          onChange(date);
                        }}
                      />
                    )}
                    <TextInput
                      ref={inputRef}
                      keyboardType={isNumeric ? "numeric" : "default"}
                      style={getInputStyles()}
                      editable={!(disabled || isLoading)}
                      selectTextOnFocus={false}
                      selectionColor={globalStyles.color.primary}
                      cursorColor={globalStyles.color.primary}
                      multiline={multiline}
                      numberOfLines={multiline ? 10 : 1}
                      {...restProps}
                      {...customProps}
                      onChangeText={value => {
                        if (!isDate) {
                          onChange(value);
                        }
                        customProps.onChangeText?.(value);
                      }}
                      onFocus={e => {
                        if (isDate) {
                          inputRef.current?.blur();
                          setShowDatePicker(true);
                        } else {
                          setIsFocused(true);
                        }
                        customProps.onFocus?.(e);
                      }}
                      onBlur={e => {
                        setIsFocused(false);
                        onBlur();
                        customProps.onBlur?.(e);
                      }}
                      defaultValue={
                        isDate
                          ? isFalsy(value)
                            ? ""
                            : formatDate(new Date(value).toISOString())
                          : checkIfNumber(value)
                            ? value.toString()
                            : value
                      }
                      placeholder={disabled ? "" : placeHolder}
                      placeholderTextColor='#939393'
                      secureTextEntry={isPassword === true && !passwordVisible}
                      showSoftInputOnFocus={!isDate}
                    />
                    {isLoading && !disabled && (
                      <View style={{ position: "absolute", right: "7%" }}>
                        <Loader />
                      </View>
                    )}
                  </>
                );
              }}
            />
            {isPassword ? (
              <TouchableOpacity onPress={() => setPasswordVisible(!passwordVisible)}>
                <Feather
                  style={styles.svg}
                  name={passwordVisible ? "eye-off" : "eye"}
                  size={16}
                  color='#9B9B9B'
                />
              </TouchableOpacity>
            ) : isDate ? (
              <View style={styles.calendarIcon}>
                <FontAwesome6
                  name='calendar'
                  size={16}
                  color='black'
                />
              </View>
            ) : undefined}
          </WrapperInputPasswordElement>
        ) : (
          <WrapperInputPasswordElement style={styles.passwordInputContainer}>
            <TextInput
              keyboardType={isNumeric ? "numeric" : "default"}
              style={[
                styles.input,
                inputStyle && inputStyle,
                isFocused && styles.inputFocused,
                isPassword && styles.inputPassword,
                {
                  backgroundColor: disabled ? "#e9ecef" : globalStyles.color.light
                }
              ]}
              editable={!(disabled || isLoading)}
              selectTextOnFocus={false}
              multiline={false}
              numberOfLines={1}
              {...restProps}
              {...customProps}
              onFocus={e => {
                setIsFocused(true);
                customProps.onFocus?.(e);
              }}
              onBlur={e => {
                setIsFocused(false);
                customProps.onBlur?.(e);
              }}
              placeholder={disabled ? "" : placeHolder}
              placeholderTextColor='#939393'
              secureTextEntry={isPassword === true && !passwordVisible}
            />
            {isPassword === true && (
              <TouchableOpacity onPress={() => setPasswordVisible(!passwordVisible)}>
                <Feather
                  style={styles.svg}
                  name={passwordVisible ? "eye-off" : "eye"}
                  size={16}
                  color='#9B9B9B'
                />
              </TouchableOpacity>
            )}
            {isLoading && !disabled && (
              <View style={{ position: "absolute", right: "7%" }}>
                <Loader />
              </View>
            )}
          </WrapperInputPasswordElement>
        )}
      </View>

      <View style={styles.errorContainer}>
        {errors && <ErrorValidationMessages errorMessages={errors} />}
      </View>
    </HasFormGroupElement>
  );
};

export default Input;
