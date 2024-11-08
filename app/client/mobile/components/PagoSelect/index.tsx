import React, { useEffect, useState } from "react";
import { View, Text, TextInput, StyleProp, TextStyle } from "react-native";
import { Dropdown, SelectCountry, MultiSelect } from "react-native-element-dropdown";
import { Feather } from "@expo/vector-icons";
import { Controller, ControllerRenderProps, FieldValues } from "react-hook-form";

import PagoFragment from "../PagoFragment";
import ErrorValidationMessages from "../ErrorValidationMessages";
import styles from "./PagoSelect.style";
import { globalStyles } from "../GlobalStyles";
import PagoLoader from "../PagoLoader";

interface Option {
  label: string;
  value: string;
  image?: { uri: string };
}
interface PagoSelectTypes {
  options: Option[];
  defaultValue?: Option[];
  name?: string;
  control?: any;
  disabled?: boolean;
  isLoading?: boolean;
  onChange?: (event: any, field?: ControllerRenderProps<FieldValues, string>) => void;
  value?: any;
  placeholder?: string;
  searchPlaceholder?: string;
  search?: boolean;
  itemWithImage?: boolean;
  multi?: boolean;
  label?: string;
  labelStyle?: StyleProp<TextStyle>;
  selectStyle?: StyleProp<TextStyle>;
  errors?: string[];
  [key: string]: any;
}

interface CustomProps {
  onChange?: (event: any, field?: ControllerRenderProps<FieldValues, string>) => void;
  value?: any;
}

const PagoSelect = ({
  options,
  defaultValue,
  name,
  control,
  disabled = false,
  isLoading,
  onChange,
  value,
  placeholder,
  searchPlaceholder,
  search,
  itemWithImage = false,
  multi = false,
  label,
  labelStyle,
  selectStyle,
  errors,
  ...restProps
}: PagoSelectTypes) => {
  const [selected, setSelected] = useState<Option[]>([]);
  const [valueState, setValueState] = useState("");
  const [isFocus, setIsFocus] = useState(false);

  useEffect(() => {
    if (!multi && defaultValue && defaultValue.length > 0) {
      const isDefaultValueValid = options.some(
        option =>
          option.value === defaultValue[0].value && option.label === defaultValue[0].label
      );

      setValueState(isDefaultValueValid ? defaultValue[0].value : "");
    } else if (defaultValue && defaultValue.length > 0) {
      const isAllDefaultValueValid = defaultValue.every(defaultValue =>
        options.some(
          option =>
            option.value === defaultValue.value && option.label === defaultValue.label
        )
      );

      setSelected(isAllDefaultValueValid ? defaultValue : []);
    } else if (value) {
      setValueState(value);
    }
  }, [multi, defaultValue, options, value]);

  let SelectType: any = Dropdown;
  if (multi && !itemWithImage) {
    SelectType = MultiSelect;
  } else if ((itemWithImage && !multi) || (itemWithImage && multi)) {
    SelectType = SelectCountry;
  }

  const customProps: CustomProps = {};

  if (onChange !== undefined) {
    customProps.onChange = onChange!;
  }
  if (value !== undefined) {
    customProps.value = value!;
  }

  const renderInputSearch = (onSearch: any) => (
    <View>
      <TextInput
        style={[styles.inputSearch]}
        placeholder={searchPlaceholder}
        placeholderTextColor='#939393'
        onChangeText={onSearch}
      />
    </View>
  );

  return (
    <PagoFragment>
      {label && <Text style={[styles.label, labelStyle]}>{label}</Text>}

      <View
        style={[
          {
            backgroundColor: "#FFF"
          },
          selectStyle && selectStyle
        ]}
      >
        {control && name ? (
          <Controller
            control={control}
            rules={{ required: true }}
            name={name}
            render={({ field }) => (
              <SelectType
                disable={disabled || isLoading}
                style={[
                  styles.dropdown,
                  isFocus && styles.dropdownFocused,
                  valueState !== "" && itemWithImage && styles.dropdownWithImage,
                  selectStyle && selectStyle,
                  disabled && styles.disableSelect
                ]}
                activeColor={globalStyles.color.primary}
                placeholderStyle={styles.placeholderStyle}
                selectedTextStyle={styles.selectedTextStyle}
                renderItem={(item: any, selected: any) => (
                  <View style={[styles.item, selected && styles.selectedItemWrapper]}>
                    <Text
                      style={[styles.itemTextStyle, selected && styles.selectedItemText]}
                    >
                      {item.label}
                    </Text>
                  </View>
                )}
                search={search}
                renderInputSearch={renderInputSearch}
                renderRightIcon={() => {
                  return (
                    <>
                      {isLoading && (
                        <View>
                          <Text>
                            <PagoLoader useFor='component' />
                          </Text>
                        </View>
                      )}
                      <Feather
                        style={styles.iconDropDown}
                        name='chevron-down'
                        size={16}
                        color='#9B9B9B'
                      />
                      <View style={styles.separate}></View>
                    </>
                  );
                }}
                data={options}
                maxHeight={200}
                labelField='label'
                valueField='value'
                imageStyle={styles.imageStyle}
                imageField='image'
                placeholder={placeholder}
                value={(() => {
                  return multi && !itemWithImage
                    ? field.value
                      ? field.value
                      : []
                    : field.value
                      ? field.value
                      : {};
                })()}
                onFocus={() => setIsFocus(true)}
                onBlur={() => setIsFocus(false)}
                onChange={(item: any) => {
                  setValueState(item.value);
                  setIsFocus(false);

                  if (multi && !itemWithImage) {
                    if (item.length !== 0) {
                      field.onChange(item);
                    } else {
                      field.onChange(undefined);
                    }
                  } else {
                    field.onChange(item.value);
                  }

                  customProps.onChange?.(item, field);
                }}
                {...restProps}
              />
            )}
          />
        ) : (
          <SelectType
            disable={disabled || isLoading}
            style={[
              styles.dropdown,
              isFocus && styles.dropdownFocused,
              valueState !== "" && itemWithImage && styles.dropdownWithImage,
              selectStyle && selectStyle,
              disabled && styles.disableSelect
            ]}
            placeholder={placeholder}
            activeColor={globalStyles.color.primary}
            placeholderStyle={styles.placeholderStyle}
            selectedTextStyle={styles.selectedTextStyle}
            renderItem={(item: any, selected: any) => (
              <View style={[styles.item, selected && styles.selectedItemWrapper]}>
                <Text style={[styles.itemTextStyle, selected && styles.selectedItemText]}>
                  {item.label}
                </Text>
              </View>
            )}
            search={search}
            renderInputSearch={renderInputSearch}
            renderRightIcon={() => {
              return (
                <>
                  {isLoading && (
                    <View>
                      <Text>
                        <PagoLoader
                          useFor='component'
                          loaderStyle={{ backgroundColor: "transparent" }}
                        />
                      </Text>
                    </View>
                  )}
                  <Feather
                    style={styles.iconDropDown}
                    name='chevron-down'
                    size={16}
                    color='#9B9B9B'
                  />
                  <View style={styles.separate}></View>
                </>
              );
            }}
            labelField='label'
            valueField='value'
            data={options}
            onFocus={() => setIsFocus(true)}
            onBlur={() => setIsFocus(false)}
            value={(() => {
              return multi && !itemWithImage ? selected : valueState;
            })()}
            onChange={(item: any) => {
              setValueState(item.value);
              setIsFocus(false);
              customProps.onChange?.(item);
            }}
            {...restProps}
          />
        )}
      </View>

      <View style={styles.errorContainer}>
        {errors && <ErrorValidationMessages errorMessages={errors} />}
      </View>
    </PagoFragment>
  );
};

export default PagoSelect;
