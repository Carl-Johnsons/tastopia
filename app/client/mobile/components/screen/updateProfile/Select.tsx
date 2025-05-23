import { BottomSheetModal } from "@gorhom/bottom-sheet";
import { useCallback, useEffect, useRef } from "react";
import { Keyboard, Pressable, PressableProps, Text } from "react-native";
import { StyleProp } from "react-native";
import { ArrowDownIcon } from "@/constants/icons";
import { colors } from "@/constants/colors";
import SelectGenderModal from "./SelectGenderModal";
import { selectUpdateProfile } from "@/slices/menu/profile/updateProfileForm.slice";
import { useTranslation } from "react-i18next";
import Animated, {
  useAnimatedStyle,
  useSharedValue,
  withTiming
} from "react-native-reanimated";

type SelectProps<T> = {
  /**
   * The select's current value.
   */
  value?: T;

  /**
   * Callback triggered upon value changes.
   */
  onChangeValue: (newValue: T | undefined) => void;

  /**
   * The class names of the wrapper.
   */
  className?: string;

  /**
   * The style object of the wrapper.
   */
  style?: StyleProp<PressableProps>;
};

const Select = ({ value, onChangeValue, className, ...props }: SelectProps<string>) => {
  const { gray } = colors;
  const modalRef = useRef<BottomSheetModal>(null);
  const { gender } = selectUpdateProfile();
  const { t } = useTranslation("updateProfile", { keyPrefix: "gender" });
  const rotate = useSharedValue("0deg");

  useEffect(() => {
    const handleGenderChange = () => {
      if (gender) {
        onChangeValue(gender.toString());
      }
    };

    handleGenderChange();
  }, [gender]);

  const openModal = useCallback(() => {
    Keyboard.dismiss();
    rotate.value = withTiming("180deg");
    modalRef.current?.present();
  }, [modalRef]);

  const closeModal = useCallback(() => {
    rotate.value = withTiming("0deg");
    modalRef.current?.close();
  }, [modalRef]);

  const iconStyle = useAnimatedStyle(() => ({
    transform: [{ rotate: rotate.value }]
  }));

  return (
    <>
      <Pressable
        {...props}
        className={`flex-row items-center justify-between rounded-lg border-transparent bg-gray-100 px-4 py-3 focus:border-primary dark:bg-black-100 ${className}`}
        onPress={openModal}
      >
        {value ? (
          <Text className='text-black_white font-sans'>{t(value.toLowerCase())}</Text>
        ) : (
          <Text className='font-sans text-gray-500'>{t("prompt")}</Text>
        )}
        <Animated.View style={iconStyle}>
          <ArrowDownIcon
            width='16'
            height='16'
            color={gray[500]}
          />
        </Animated.View>
      </Pressable>

      <SelectGenderModal
        ref={modalRef}
        onClose={closeModal}
      />
    </>
  );
};

export default Select;
