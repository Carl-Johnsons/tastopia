import { Leaf1Icon, Leaf2Icon, Leaf3Icon } from "@/constants/icons";
import { View } from "react-native";
import { G, Path, Svg } from "react-native-svg";
import { ViewProps } from "react-native-svg/lib/typescript/fabric/utils";

const MenuBg = (props: ViewProps) => {
  return (
    <View
      {...props}
      className={`relative flex w-full flex-row ${props.className}`}
      style={{
        paddingRight: 60
      }}
    >
      <View className='absolute'>
        <Leaf1Icon />
      </View>
      <View
        className='absolute z-[1]'
        style={{
          left: 100,
          zIndex: -1
        }}
      >
        <Leaf2Icon />
      </View>
      <View className='absolute right-0'>
        <Leaf3Icon />
      </View>
    </View>
  );
};

const SvgCircle1 = () => (
  <Svg
    width='198'
    height='119'
    viewBox='0 0 198 119'
    fill='none'
  >
    <Path
      d='M150 65C176.51 65 198 43.5097 198 17C198 -9.50966 176.51 -31 150 -31C123.49 -31 102 -9.50966 102 17C102 43.5097 123.49 65 150 65Z'
      fill='#FE724C'
      fillOpacity='0.1'
    />
    <Path
      d='M123.5 118.499V38.4096C123.5 -29.8496 68.4673 -85.2286 0.5 -85.499V-5.40957C0.5 62.8496 55.5327 118.229 123.5 118.499Z'
      fill='#FFC529'
      stroke='#FFC529'
    />
  </Svg>
);

const SvgCircle2 = () => (
  <Svg
    className='shadow-2xl'
    width='251'
    height='244'
    viewBox='0 0 251 244'
    fill='none'
  >
    <G>
      <Path
        d='M80 164V51.9295C80 -43.5797 157.9 -121 254 -121V-8.92948C254 86.5797 176.1 164 80 164Z'
        fill='#FE724C'
      />
    </G>
  </Svg>
);

export default MenuBg;
