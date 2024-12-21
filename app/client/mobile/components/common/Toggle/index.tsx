import { Animated, Easing, Text, TouchableWithoutFeedback, View } from "react-native";
import { ReactNode, useRef, useState } from "react";
import { useTranslation } from "react-i18next";
import styles from "./Toggle.style";

type ToggleProps = {
  title: string;
  handleToggle?: () => void;
  icon?: ReactNode;
};

const Toggle = ({ title, handleToggle, icon }: ToggleProps) => {
  const [checked, setChecked] = useState<boolean>(false);
  const { t } = useTranslation("component");
  const toggleX = useRef(new Animated.Value(-9)).current;

  const toggleStyle = [
    styles["toggle-label"],
    checked ? styles["toggle-on"] : styles["toggle-off"]
  ];

  const handlePress = () => {
    setChecked(!checked);
    handleToggle && handleToggle();

    Animated.timing(toggleX, {
      toValue: checked ? -9 : 9,
      duration: 250,
      easing: Easing.inOut(Easing.ease),
      useNativeDriver: false
    }).start();
  };

  return (
    <View style={styles.container}>
      <View style={styles["switch-holder"]}>
        <View style={styles["toggle-title"]}>
          <Text
            style={[
              {
                marginRight: 5,
                color: checked ? "#39636B" : "#646464"
              }
            ]}
          >
            {icon}
          </Text>
          <Text
            style={{
              color: checked ? "#39636B" : "#646464",
              marginLeft: 2
            }}
          >
            {title}
          </Text>
        </View>

        <View>
          {/* customized checkbox */}
          <TouchableWithoutFeedback
            style={styles["switch-toggle-holder"]}
            onPress={handlePress}
          >
            <View
              style={{
                backgroundColor: "#ffffff",
                marginHorizontal: 30,
                // width: 40,
                height: 15,
                borderRadius: 25,
                overflow: "hidden"
              }}
            >
              <View style={styles["inset-shadow"]} />
              <Animated.View
                style={[
                  toggleStyle,
                  {
                    transform: [{ translateX: toggleX }]
                  }
                ]}
              >
                <Text
                  style={[
                    {
                      lineHeight: 9,
                      fontSize: 6,
                      textAlign: "center",
                      color: checked ? "#ffffff" : "#545454"
                    }
                  ]}
                >
                  {checked ? t("Toggle.on") : t("Toggle.off")}
                  {/* {checked ? "ON" : "OFF"} */}
                </Text>
              </Animated.View>
            </View>
          </TouchableWithoutFeedback>
        </View>
      </View>
    </View>
  );
};
export default Toggle;
