import { ReactNode, useLayoutEffect, useMemo, useRef, useState } from "react";
import { ViewStyle } from "react-native";
import {
  View,
  StyleSheet,
  Modal,
  TouchableWithoutFeedback,
  StyleProp
} from "react-native";

import { Dimensions } from "react-native";

type DropDownType = {
  // required
  renderBtn: () => ReactNode;
  /* 
    - If the content has the <ScrollView> component, you have to do it like this
    to prevent the <TouchableWithoutFeedBack> override the scroll event
    ```
    <ScrollView>
      <View 
        onStartShouldSetResponder={() => true}
      >
        // Your content here
      </View>
    </ScrollView>
    ```
  */
  renderDropDownContent: () => ReactNode;
  // events
  onCancel?: () => void;
  // variants
  align?: "left" | "right" | "center";
  dropDownGap?: number;
  show?: boolean;
  // Custom styles
  btnStyle?: StyleProp<ViewStyle>;
  listStyle?: StyleProp<ViewStyle>;
};

type ElementLayout = {
  width: number;
  height: number;
  x: number;
  y: number;
};

const DropDown = ({
  // required
  renderBtn = () => <></>,
  renderDropDownContent = () => <></>,
  // events
  onCancel = () => {},
  // variants
  align = "left",
  dropDownGap = 10,
  show = false,
  // Custom styles
  btnStyle,
  listStyle
}: DropDownType) => {
  const screenWidth = Dimensions.get("window").width;
  const [isRendered, setIsRendered] = useState(false);

  const btnRef = useRef<View>(null);
  const listItemRef = useRef<View>(null);

  const [btnLayout, setBtnLayout] = useState<ElementLayout>({
    width: 0,
    height: 0,
    x: 0,
    y: 0
  });

  const [listItemLayout, setListItemLayout] = useState<ElementLayout>({
    width: 0,
    height: 0,
    x: 0,
    y: 0
  });

  const calculatedListItemLayout = useMemo<ElementLayout>(() => {
    if (
      listItemRef.current &&
      btnRef.current &&
      btnLayout.width !== 0 &&
      listItemLayout.width !== 0
    ) {
      setIsRendered(true);
    } else {
      setIsRendered(false);
    }

    switch (align) {
      case "center":
        return {
          width: btnLayout.width,
          height: 0,
          x:
            screenWidth - (btnLayout.x + listItemLayout.width / 2) < 0
              ? btnLayout.x -
                Math.abs(screenWidth - (btnLayout.x + listItemLayout.width / 2)) -
                Math.abs(screenWidth - (btnLayout.x + btnLayout.width / 2))
              : btnLayout.x + btnLayout.width / 2 - listItemLayout.width / 2,
          y: btnLayout.y + btnLayout.height + dropDownGap
        };
      case "right":
        return {
          width: btnLayout.width,
          height: 0,
          x:
            screenWidth - (btnLayout.x + listItemLayout.width) < 0
              ? btnLayout.x -
                Math.abs(screenWidth - (btnLayout.x + listItemLayout.width)) -
                Math.abs(screenWidth - (btnLayout.x + btnLayout.width))
              : btnLayout.x + btnLayout.width - listItemLayout.width,
          y: btnLayout.y + btnLayout.height + dropDownGap
        };
      default: // left
        return {
          width: btnLayout.width,
          height: 0,
          x: btnLayout.x,
          y: btnLayout.y + btnLayout.height + dropDownGap
        };
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [btnLayout, listItemLayout]);

  useLayoutEffect(() => {
    if (btnRef.current) {
      btnRef.current.measureInWindow(
        (x: number, y: number, width: number, height: number) => {
          setBtnLayout({ width, height, x, y });
        }
      );
    }
  }, [show]);

  useLayoutEffect(() => {
    if (listItemRef.current) {
      listItemRef.current.measureInWindow(
        (x: number, y: number, width: number, height: number) => {
          setListItemLayout({ width, height, x, y });
        }
      );
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [listItemRef.current]);

  return (
    <View>
      <TouchableWithoutFeedback>
        <View
          style={btnStyle}
          ref={btnRef}
        >
          {renderBtn()}
        </View>
      </TouchableWithoutFeedback>
      {show && (
        <Modal
          visible={show}
          transparent
        >
          <TouchableWithoutFeedback
            onPress={() => {
              onCancel();
            }}
          >
            <View style={styles.container}>
              <TouchableWithoutFeedback onPress={() => {}}>
                <View
                  ref={listItemRef}
                  style={[
                    styles.itemList,
                    {
                      width: calculatedListItemLayout.width,
                      left: calculatedListItemLayout.x,
                      top: calculatedListItemLayout.y
                    },
                    listStyle,
                    !isRendered && { opacity: 0 }
                  ]}
                >
                  {renderDropDownContent()}
                </View>
              </TouchableWithoutFeedback>
            </View>
          </TouchableWithoutFeedback>
        </Modal>
      )}
    </View>
  );
};

export default DropDown;

const styles = StyleSheet.create({
  container: {
    width: "100%",
    height: "100%"
  },

  itemList: {
    position: "absolute",
    top: 0,
    borderColor: "#D9D9D9",

    borderWidth: 1,
    borderStyle: "solid",
    borderRadius: 10,
    width: "100%",
    backgroundColor: "#fff",

    paddingLeft: 14,
    paddingRight: 14,
    paddingTop: 14,
    paddingBottom: 14
  }
});
