import { Text, TouchableHighlight, View } from "react-native";
import { forwardRef, ReactNode, useMemo } from "react";
import BottomSheet, { BottomSheetBackdrop, BottomSheetView } from "@gorhom/bottom-sheet";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { AntDesign, Feather, Octicons } from "@expo/vector-icons";
import { router } from "expo-router";

type Props = {
  id: string;
  title: string;
};

const SettingRecipe = forwardRef<BottomSheet, Props>((props, ref) => {
  const { c } = useColorizer();
  const { black, white } = colors;

  const snapPoints = useMemo(() => ["20%"], []);

  const onPressEdit = () => {
    router.push({
      pathname: "/(protected)/community/update-recipe",
      params: { id: props.id }
    });
  };

  const onPressShare = () => {};

  const onPressReport = () => {};
  return (
    <BottomSheet
      ref={ref}
      index={-1}
      snapPoints={snapPoints}
      enablePanDownToClose={true}
      handleIndicatorStyle={{
        backgroundColor: c(colors.black.DEFAULT, colors.white.DEFAULT)
      }}
      backgroundStyle={{
        backgroundColor: c(colors.white.DEFAULT, colors.black[100])
      }}
      backdropComponent={props => <BottomSheetBackdrop {...props} />}
    >
      <BottomSheetView>
        <View className='mt-2 px-5 pb-4'>
          <View>
            <BottomSheetItem
              title='Edit post'
              icon={
                <AntDesign
                  name='edit'
                  size={20}
                  color={c(black.DEFAULT, white.DEFAULT)}
                />
              }
              onPress={onPressEdit}
            />
            <BottomSheetItem
              title='Share'
              icon={
                <AntDesign
                  name='sharealt'
                  size={20}
                  color={c(black.DEFAULT, white.DEFAULT)}
                />
              }
              onPress={onPressShare}
            />
            <BottomSheetItem
              title='Report this post'
              icon={
                <Octicons
                  name='report'
                  size={20}
                  color={c(black.DEFAULT, white.DEFAULT)}
                />
              }
              onPress={onPressReport}
            />
          </View>
        </View>
      </BottomSheetView>
    </BottomSheet>
  );
});

type BottomSheetItemProps = {
  title: string;
  description?: string;
  icon: ReactNode;
  onPress: () => void;
};

const BottomSheetItem = ({ title, description, icon, onPress }: BottomSheetItemProps) => {
  const { c } = useColorizer();

  return (
    <TouchableHighlight
      onPress={onPress}
      underlayColor={c(colors.gray[200], colors.gray[700])}
      style={{
        borderRadius: 8
      }}
    >
      <View className='flex-row items-center gap-4 px-4 py-3'>
        {icon}

        <View className='flex-col'>
          <Text className='text-black_white base-medium'>{title}</Text>
          {description && <Text className='text-black_white'>{description}</Text>}
        </View>
      </View>
    </TouchableHighlight>
  );
};

export default SettingRecipe;
