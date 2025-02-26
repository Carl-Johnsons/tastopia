import { BottomSheetModal } from "@gorhom/bottom-sheet";
import { useCallback, useRef } from "react";

type UseBottomSheetModalResult = {
  /** Ref of the bottom sheet modal. */
  ref: React.MutableRefObject<BottomSheetModal | null>;

  /** Open the bottom sheet modal. */
  openModal: () => void;

  /** Close the bottom sheet modal. */
  closeModal: () => void;
};

export const useBottomSheetModal = (): UseBottomSheetModalResult => {
  const ref = useRef<BottomSheetModal>(null);

  const openModal = useCallback(() => {
    ref.current?.present();
  }, [ref]);

  const closeModal = useCallback(() => {
    ref.current?.close();
  }, [ref]);

  return { ref, openModal, closeModal };
};

export default useBottomSheetModal;
