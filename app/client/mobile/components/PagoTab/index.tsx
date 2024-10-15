import { View, StyleSheet } from "react-native";
import React, { ReactElement } from "react";
import { Tab, TabView } from "@rneui/themed";
import { globalStyles } from "../GlobalStyles";

type ItemProps = {
  title: string;
  iconStyle?: object;
  titleStyle?: object;
};

type PagoTabProps = {
  variant?: "primary" | "secondary";
  tabItems: ItemProps[];
  tabViews: ReactElement[];
  [key: string]: any; // for TabView props: https://reactnativeelements.com/docs/next/components/tabview
};

const PagoTab = ({ variant = "primary", tabItems, tabViews, ...rest }: PagoTabProps) => {
  const [index, setIndex] = React.useState(0);
  return (
    <View style={{ flex: 1 }}>
      <Tab
        value={index}
        onChange={e => setIndex(e)}
        style={{ backgroundColor: "white" }}
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
              titleStyle={[
                item.titleStyle,
                isActive && {
                  color:
                    variant === "primary"
                      ? globalStyles.color.primary
                      : globalStyles.color.secondary
                }
              ]}
              icon={item.iconStyle}
              style={[
                isActive &&
                  (variant === "primary" ? styles.primaryActive : styles.secondaryActive),
                variant === "primary" && styles.primary
              ]}
            />
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

export default PagoTab;

const styles = StyleSheet.create({
  primary: {
    marginHorizontal: 10,
    marginVertical: 6
  },
  primaryActive: {
    borderWidth: 0.2,
    borderColor: globalStyles.color.bsBodyColor,
    borderRadius: 6
  },
  secondaryActive: {
    backgroundColor: globalStyles.color.primaryOpacity
  }
});
