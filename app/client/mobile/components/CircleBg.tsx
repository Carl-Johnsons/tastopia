import { View } from "react-native";
import { Circle, Svg } from "react-native-svg";
import { ViewProps } from "react-native-svg/lib/typescript/fabric/utils";

const CircleBg = (props: ViewProps) => {
  return (
    <View
      {...props}
      className={`flex flex-row justify-between ${props.className}`}
    >
      <SvgCircle1 />
      <View className='grow' />
      <SvgCircle2 />
    </View>
  );
};

const SvgCircle1 = () => (
  <Svg
    width={160}
    height={75}
    viewBox='0 0 160 75'
    fill='none'
  >
    <Circle
      cx={2}
      cy={27}
      r={30}
      stroke='#FE724C'
      strokeWidth={36}
    />
    <Circle
      cx={77.5}
      cy={-16.5}
      r={82.5}
      fill='#FE724C'
      fillOpacity={0.1}
    />
  </Svg>
);

const SvgCircle2 = () => (
  <Svg
    width='77'
    height='72'
    viewBox='0 0 77 72'
    fill='none'
  >
    <Circle
      cx='90.5'
      cy='-18.5'
      r='90.5'
      fill='#FE724C'
    />
  </Svg>
);

export default CircleBg;
