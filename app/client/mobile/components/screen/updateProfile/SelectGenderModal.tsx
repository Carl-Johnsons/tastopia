import { Text, View } from "react-native";
import Button from "../../Button";
import { RefObject, forwardRef, useCallback, useEffect } from "react";
import {
  CheckCircleIcon,
  CloseIcon,
  UnitedKingdomFlagIcon,
  VietnamFlagIcon
} from "@/constants/icons";
import { colors } from "@/constants/colors";
import Animated from "react-native-reanimated";
import {
  BottomSheetBackdrop,
  BottomSheetModal,
  BottomSheetView,
  BottomSheetBackdropProps,
  useBottomSheetModal
} from "@gorhom/bottom-sheet";

import { BottomSheetMethods } from "@gorhom/bottom-sheet/lib/typescript/types";
import { useTranslation } from "react-i18next";
import useColorizer from "@/hooks/useColorizer";
import { ItemCard } from "@/components/SettingModal";
import { GENDER, selectUser } from "@/slices/user.slice";
import { useAppDispatch } from "@/store/hooks";
import {
  saveUpdateProfileData,
  selectUpdateProfile
} from "@/slices/menu/profile/updateProfileForm.slice";

type Item = {
  /**
   * The item's text that is showing to the user. If omited, the select will render the value instead.
   */
  label?: string;

  /**
   * The item's that is used for state management.
   */
  value: string;

  /**
   * Determine if the item is a placeholder.
   */
  isPlaceholder?: boolean;
};

type SelectGenderModalProps = {
  ref: RefObject<BottomSheetMethods>;
  onClose: () => void;
};

const SelectGenderModal = forwardRef<BottomSheetModal, SelectGenderModalProps>(
  (_props, ref) => {
    const { t } = useTranslation("updateProfile", { keyPrefix: "gender" });
    const { dismiss } = useBottomSheetModal();
    const { c } = useColorizer();
    const { black, white } = colors;

    const closeModal = useCallback(() => {
      dismiss();
    }, [dismiss]);

    const renderBackdrop = useCallback(
      (props: BottomSheetBackdropProps) => (
        <BottomSheetBackdrop
          {...props}
          disappearsOnIndex={-1}
          appearsOnIndex={0}
          pressBehavior='close'
          onPress={closeModal}
        />
      ),
      []
    );

    return (
      <BottomSheetModal
        ref={ref}
        backdropComponent={renderBackdrop}
        handleIndicatorStyle={{
          backgroundColor: c(colors.black.DEFAULT, colors.white.DEFAULT),
          display: "none"
        }}
        backgroundStyle={{
          backgroundColor: c(colors.white.DEFAULT, colors.black[100])
        }}
        enableContentPanningGesture={false}
      >
        <BottomSheetView>
          <Animated.View className='w-full rounded-t-lg bg-white dark:bg-black-100'>
            <View className='relative flex-row items-center justify-center px-5 pb-4'>
              <View className='absolute left-5 top-1'>
                <Button onPress={closeModal}>
                  <CloseIcon
                    color={c(black.DEFAULT, white.DEFAULT)}
                    width={22}
                    height={22}
                  />
                </Button>
              </View>
              <Text className='text-black_white font-semibold text-2xl'>
                {t("title")}
              </Text>
              <View />
            </View>

            <SelectSection />
          </Animated.View>
        </BottomSheetView>
      </BottomSheetModal>
    );
  }
);

const SelectSection = () => {
  const { t } = useTranslation("updateProfile", { keyPrefix: "gender" });
  const { gender: initialValue } = selectUser();
  const { gender } = selectUpdateProfile();
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (initialValue) {
      dispatch(saveUpdateProfileData({ gender: initialValue }));
    }
  }, [initialValue]);

  const handleChangeValue = (value: GENDER) => {
    dispatch(saveUpdateProfileData({ gender: value }));
  };

  return (
    <>
      <ItemCard
        title={t("male")}
        className='justify-between'
        additionalIcon={gender === GENDER.MALE ? CheckCircleIcon : undefined}
        onPress={() => handleChangeValue(GENDER.MALE)}
      />
      <ItemCard
        title={t("female")}
        className='justify-between'
        additionalIcon={gender === GENDER.FEMALE ? CheckCircleIcon : undefined}
        onPress={() => handleChangeValue(GENDER.FEMALE)}
      />
    </>
  );
};

export default SelectGenderModal;
