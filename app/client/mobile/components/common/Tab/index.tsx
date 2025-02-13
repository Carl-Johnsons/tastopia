import { View, StyleSheet } from "react-native";
import React, { ReactElement } from "react";
import { Tab, TabView } from "@rneui/themed";
import { globalStyles } from "../GlobalStyles";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";

type ItemProps = {
  title: string;
  iconStyle?: object;
  titleStyle?: object;
};

type TabProps = {
  variant?: "primary" | "secondary";
  tabItems: ItemProps[];
  tabViews: ReactElement[];
  [key: string]: any; // for TabView props: https://reactnativeelements.com/docs/next/components/tabview
};

const CustomTab = ({ variant = "primary", tabItems, tabViews, ...rest }: TabProps) => {
  const { c } = useColorizer();
  const { white, black, gray } = colors;
  const textColor = c(gray[700], gray[700]);
  const textColorActive = c(black.DEFAULT, white.DEFAULT);
  const [index, setIndex] = React.useState(0);
  return (
    <View style={{ flex: 1 }}>
      <Tab
        value={index}
        onChange={e => setIndex(e)}
        style={{
          justifyContent: "center",
          alignItems: "center"
        }}
        indicatorStyle={{
          opacity: 0
        }}
      >
        {tabItems.map((item, idx) => {
          const isActive = index === idx;
          return (
            <Tab.Item
              key={idx + item.title}
              title={item.title}
              buttonStyle={[
                { borderBottomWidth: 2, borderColor: "transparent" },
                isActive && {
                  borderBottomWidth: 2,
                  borderColor: globalStyles.color.primary
                }
              ]}
              titleStyle={[
                item.titleStyle,
                {
                  color: textColor,
                  textTransform: "capitalize"
                },
                isActive && {
                  color:
                    variant === "primary"
                      ? textColorActive
                      : globalStyles.color.disableOpacity
                }
              ]}
              icon={item.iconStyle}
              style={[
                variant === "primary" && styles.primary,
                isActive &&
                  (variant === "primary" ? styles.primaryActive : styles.secondaryActive)
              ]}
            ></Tab.Item>
          );
        })}
      </Tab>

      <TabView
        value={index}
        onChange={setIndex}
        animationType='spring'
        {...rest}
      >
        {tabViews.map(view => {
          return view;
        })}
      </TabView>
    </View>
  );
};

export default CustomTab;

const styles = StyleSheet.create({
  primary: {},
  primaryActive: {
    borderColor: globalStyles.color.primary
  },
  secondaryActive: {
    backgroundColor: globalStyles.color.primaryOpacity
  }
});
