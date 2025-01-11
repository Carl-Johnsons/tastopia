import { View, Dimensions } from "react-native";
import Animated, {
  useAnimatedStyle,
  useSharedValue,
  withSpring,
  runOnJS,
  SharedValue,
  useAnimatedReaction
} from "react-native-reanimated";
import { Gesture, GestureDetector } from "react-native-gesture-handler";

const ITEM_HEIGHT = 70;
const SPRING_CONFIG = {
  damping: 15,
  stiffness: 150
};

interface DraggableProps {
  children?: React.ReactNode;
  boxBounds: {
    width: number;
    height: number;
  };
  positions: SharedValue<{ [key: string]: number }>;
  id: number;
  itemCount: number;
}

export default function Draggable({
  children,
  positions,
  id,
  itemCount
}: DraggableProps) {
  const translateY = useSharedValue(id * ITEM_HEIGHT);
  const contextY = useSharedValue(0);
  const isGestureActive = useSharedValue(false);

  const clampPosition = (value: number, min: number, max: number) => {
    "worklet";
    return Math.min(Math.max(value, min), max);
  };

  const pushItems = (
    positions: { [key: string]: number },
    fromPos: number,
    toPos: number
  ) => {
    "worklet";
    const newPositions = { ...positions };

    if (toPos < fromPos) {
      Object.keys(positions).forEach(key => {
        const currentPos = positions[key];
        if (currentPos >= toPos && currentPos < fromPos) {
          newPositions[key] = currentPos + 1;
        }
      });
    } else if (toPos > fromPos) {
      Object.keys(positions).forEach(key => {
        const currentPos = positions[key];
        if (currentPos <= toPos && currentPos > fromPos) {
          newPositions[key] = currentPos - 1;
        }
      });
    }

    newPositions[id] = toPos;
    return newPositions;
  };

  const onDrag = Gesture.Pan()
    .onBegin(() => {
      contextY.value = translateY.value;
      isGestureActive.value = true;
    })
    .onChange(event => {
      const newY = event.translationY + contextY.value;
      translateY.value = clampPosition(newY, 0, (itemCount - 1) * ITEM_HEIGHT);

      const newPosition = Math.round(translateY.value / ITEM_HEIGHT);

      if (newPosition !== positions.value[id]) {
        positions.value = pushItems(positions.value, positions.value[id], newPosition);
      }
    })
    .onFinalize(() => {
      isGestureActive.value = false;
      translateY.value = withSpring(positions.value[id] * ITEM_HEIGHT, SPRING_CONFIG);
    });

  useAnimatedReaction(
    () => positions.value[id],
    (currentPosition, previousPosition) => {
      if (currentPosition !== previousPosition && !isGestureActive.value) {
        translateY.value = withSpring(currentPosition * ITEM_HEIGHT, SPRING_CONFIG);
      }
    }
  );

  const animatedStyle = useAnimatedStyle(() => ({
    transform: [
      { translateY: translateY.value },
      { scale: isGestureActive.value ? 1.05 : 1 }
    ],
    zIndex: isGestureActive.value ? 1000 : 1
  }));

  return (
    <GestureDetector gesture={onDrag}>
      <Animated.View
        style={[
          {
            position: "absolute",
            width: "100%",
            height: ITEM_HEIGHT - 10,
            paddingHorizontal: 10
          },
          animatedStyle
        ]}
      >
        {children}
      </Animated.View>
    </GestureDetector>
  );
}
