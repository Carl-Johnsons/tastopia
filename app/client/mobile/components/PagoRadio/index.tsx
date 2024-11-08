import { useState } from "react";
import { Controller } from "react-hook-form";
import {
  View,
  Text,
  TouchableOpacity,
  StyleProp,
  ViewStyle,
  TextStyle
} from "react-native";

import styles from "./PagoRadio.style";

type PagoRadioProps = {
  options: { label: string; value: string }[];
  name: string;
  control: any; // To work with react-hook-form
  containerStyle?: StyleProp<ViewStyle>;
  radioElementStyle?: StyleProp<ViewStyle>;
  labelStyle?: StyleProp<TextStyle>;
  onChange?: (event: any) => void;
  onPress?: (event: any) => void;
};

type CustomProps = {
  onPressProp?: (event: any) => void;
  onChange?: (option: any) => void;
};

const PagoRadio = ({
  name,
  options,
  control,
  containerStyle,
  radioElementStyle,
  labelStyle,
  onChange,
  onPress
}: PagoRadioProps) => {
  const customProps: CustomProps = {};
  const [toggleIndex, setToggleIndex] = useState<number>(0);

  if (onPress) {
    customProps.onPressProp = onPress!;
  }
  if (onChange !== undefined) {
    customProps.onChange = onChange!;
  }

  return (
    <View style={containerStyle}>
      <Controller
        name={name}
        control={control}
        render={({ field: { onChange, value } }) => (
          <>
            {options.map((option, index) => (
              <View
                style={radioElementStyle}
                key={index}
              >
                <TouchableOpacity
                  key={index}
                  onPress={() => {
                    setToggleIndex(index);
                    onChange(option);
                    customProps.onChange && customProps.onChange(option);
                    customProps.onPressProp && customProps.onPressProp(option);
                  }}
                >
                  <View style={styles.radioContainer}>
                    <View
                      style={[
                        styles.radioToggle,
                        value && toggleIndex === index
                          ? styles.radioToggleActive
                          : styles.radioToggleInActive
                      ]}
                    ></View>
                    <Text
                      style={[
                        {
                          fontWeight: 700,
                          marginLeft: 8,
                          color: value && toggleIndex === index ? "#387562" : "#939393"
                        },
                        labelStyle
                      ]}
                    >
                      {option.label}
                    </Text>
                  </View>
                </TouchableOpacity>
              </View>
            ))}
          </>
        )}
      />
    </View>
  );
};

export default PagoRadio;
