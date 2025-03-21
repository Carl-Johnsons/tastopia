import { ReactNode, createContext } from "react";

export type OnChangeActiveFn = (params: { reportId: string; value: boolean }) => void;
export type DataTableContext = {
  onChangeActive: OnChangeActiveFn;
  onOpenModal: (index: number) => void;
  onCloseModal: () => void;
};

type Props = {
  children: ReactNode;
  value: DataTableContext;
};

export const DataTableContext = createContext<DataTableContext | undefined>(undefined);

export const DataTableProvider = ({ children, value }: Props) => {
  return <DataTableContext.Provider value={value}>{children}</DataTableContext.Provider>;
};

export default DataTableProvider;
