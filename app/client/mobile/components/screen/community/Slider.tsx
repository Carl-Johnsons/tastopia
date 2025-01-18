import { Dimensions, FlatList, StyleSheet, Text, View } from "react-native";
import SliderItem from "./SliderItem";
import Animated, {
  scrollTo,
  useAnimatedRef,
  useAnimatedScrollHandler,
  useDerivedValue,
  useSharedValue
} from "react-native-reanimated";
import { useEffect, useRef, useState } from "react";

type SliderProps = {
  data: string[];
};

const INTERVAL = 4000;
const { width } = Dimensions.get("screen");

const Slider = ({ data }: SliderProps) => {
  const scrollX = useSharedValue(0);
  const ref = useAnimatedRef<Animated.FlatList<any>>();
  const [isAutoPlay, setIsAutoPlay] = useState(true);
  const interval = useRef<NodeJS.Timeout>();
  const offset = useSharedValue(0);

  const onScrollHandler = useAnimatedScrollHandler({
    onScroll: e => {
      scrollX.value = e.contentOffset.x;
    },
    onMomentumEnd: e => {
      offset.value = e.contentOffset.x;
    }
  });

  useEffect(() => {
    if (isAutoPlay) {
      interval.current = setInterval(() => {
        const nextOffset = offset.value + width;

        if (nextOffset >= data.length * width) {
          offset.value = 0;
        } else {
          offset.value = nextOffset;
        }
      }, INTERVAL);
    } else {
      clearInterval(interval.current);
    }

    return () => {
      clearInterval(interval.current);
    };
  }, [isAutoPlay, offset, width]);

  useDerivedValue(() => {
    scrollTo(ref, offset.value, 0, true);
  });

  return (
    <View>
      <Animated.FlatList
        ref={ref}
        data={data}
        horizontal
        showsHorizontalScrollIndicator={false}
        pagingEnabled
        renderItem={({ item, index }) => (
          <SliderItem
            item={item}
            index={index}
            scrollX={scrollX}
          />
        )}
        onScroll={onScrollHandler}
        scrollEventThrottle={16}
        onEndReachedThreshold={0.5}
        onScrollBeginDrag={() => {
          setIsAutoPlay(false);
        }}
        onScrollEndDrag={() => {
          setIsAutoPlay(true);
        }}
      />
    </View>
  );
};

export default Slider;

const styles = StyleSheet.create({});
