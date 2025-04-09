import BottomSheet from "@gorhom/bottom-sheet/lib/typescript/components/bottomSheet/BottomSheet";
import { ReactNode, RefObject, createContext, useContext, useRef } from "react";

type HistoryContextType = {
  bottomSheetRef: RefObject<BottomSheet>;
};

const HistoryContext = createContext<HistoryContextType | null>(null);

const HistoryProvider = ({ children }: { children: ReactNode }) => {
  const bottomSheetRef = useRef<BottomSheet>(null);

  const value: HistoryContextType = {
    bottomSheetRef
  };

  return <HistoryContext.Provider value={value}>{children}</HistoryContext.Provider>;
};

const useHistoryContext = () => {
  const context = useContext(HistoryContext) as HistoryContextType;

  if (!context) {
    throw new Error("useHistoryContext must be used within a HistoryProvider");
  }

  return context;
};

export { HistoryProvider, useHistoryContext };
