import { CheckBox } from "@rneui/themed";
import { StyleProp, Text, TextStyle, View } from "react-native";
import { Controller } from "react-hook-form";
import React, { useState } from "react";

import Fragment from "../Fragment";
import ErrorValidationMessages from "../ErrorValidationMessages";
import styles from "./CheckBox.style";

interface CheckboxTypes {
  isChecked?: boolean;
  value?: any;
  control?: any;
  name?: string;
  label: string;
  labelStyle?: StyleProp<TextStyle>;
  onPress?: (event: any) => void;
  errors?: string[];
  disable?: boolean;
  children?: React.ReactNode;
  [key: string]: any;
}

interface CustomProps {
  onPressProp?: (event: any) => void;
  value?: any;
}

const CheckBox = ({
  isChecked,
  label,
  labelStyle,
  value,
  control,
  name,
  onPress,
  errors,
  disable = false,
  children,
  ...restProps
}: CheckboxTypes) => {
  const [isCheck, setIsChecked] = useState(false);

  const customProps: CustomProps = {};

  if (onPress !== undefined) {
    customProps.onPressProp = onPress!;
  }
  if (value !== undefined) {
    customProps.value = value!;
  }

  return (
    <View style={styles.checkBoxWrapper}>
      <View style={[styles.wrapperCheckBoxLabel]}>
        <Fragment>
          {control && name ? (
            <Controller
              name={name}
              control={control}
              rules={{ required: true }}
              render={({ field: { onChange } }) => (
                <CheckBox
                  containerStyle={[styles.checkBoxContainer]}
                  checked={isCheck}
                  disabled={disable}
                  onPress={e => {
                    const valueCheckBox = value ? value : !isCheck;
                    setIsChecked(!isCheck);
                    customProps.onPressProp?.(e);
                    if (!isCheck) {
                      onChange(valueCheckBox);
                    } else {
                      onChange(undefined);
                    }
                  }}
                  {...customProps}
                  {...restProps}
                  aria-label={!children ? label || "checkbox" : undefined}
                >
                  {children ? children : <View></View>}
                </CheckBox>
              )}
            />
          ) : (
            <CheckBox
              containerStyle={[styles.checkBoxContainer]}
              checked={isCheck}
              disabled={disable}
              onPress={e => {
                setIsChecked(!isCheck);
                customProps.onPressProp?.(e);
              }}
              {...customProps}
              {...restProps}
              aria-label={!children ? label || "checkbox" : undefined}
            >
              {children ? children : <View></View>}
            </CheckBox>
          )}
          {label && (
            <Text
              style={[
                styles.label,
                labelStyle,
                { color: isCheck ? "#387562" : "#939393" }
              ]}
            >
              {label}
            </Text>
          )}
        </Fragment>
      </View>

      <View style={styles.errorContainer}>
        {errors && <ErrorValidationMessages errorMessages={errors} />}
      </View>
    </View>
  );
};

export default CheckBox;
