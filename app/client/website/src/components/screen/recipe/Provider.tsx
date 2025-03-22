import { ReactNode, createContext } from "react";

export type OnChangeActiveFn = (params: { recipeId: string; value: boolean }) => void;
export type DataTableContextValue = {
  onChangeActive: OnChangeActiveFn;
};

type Props = {
  children: ReactNode;
  value: DataTableContextValue;
};

export const DataTableContext = createContext<DataTableContextValue | undefined>(
  undefined
);

export const DataTableProvider = ({ children, value }: Props) => {
  return <DataTableContext.Provider value={value}>{children}</DataTableContext.Provider>;
};

export default DataTableProvider;
