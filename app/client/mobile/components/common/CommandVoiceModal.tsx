import { Text, TouchableHighlight, View } from "react-native";
import { forwardRef, ReactNode } from "react";
import BottomSheet, { BottomSheetBackdrop, BottomSheetView } from "@gorhom/bottom-sheet";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { AntDesign } from "@expo/vector-icons";
import { useTranslation } from "react-i18next";

type Props = {
  content: string;
};

const CommandVoiceModal = forwardRef<BottomSheet, Props>((props, ref) => {
  const { c } = useColorizer();
  const { black, white } = colors;
  const { t } = useTranslation("component");

  const closeModal = () => {};

  return (
    <BottomSheet
      ref={ref}
      index={-1}
      enablePanDownToClose={true}
      keyboardBehavior='interactive'
      keyboardBlurBehavior='restore'
      android_keyboardInputMode='adjustPan'
      handleIndicatorStyle={{
        backgroundColor: c(colors.black.DEFAULT, colors.white.DEFAULT)
      }}
      backgroundStyle={{
        backgroundColor: c(colors.white.DEFAULT, colors.black[100])
      }}
      backdropComponent={props => (
        <BottomSheetBackdrop
          {...props}
          disappearsOnIndex={-1}
          appearsOnIndex={0}
          pressBehavior='close'
        />
      )}
    >
      <BottomSheetView>
        <View className='mt-2 px-5 pb-4'>
          <BottomSheetItem
            title={t("settingRecipe.editRecipe")}
            icon={
              <AntDesign
                name='edit'
                size={20}
                color={c(black.DEFAULT, white.DEFAULT)}
              />
            }
            onPress={() => {}}
          />
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

export default CommandVoiceModal;
