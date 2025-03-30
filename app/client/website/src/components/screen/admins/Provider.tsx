import { ReactNode, createContext, useContext } from "react";

export type OnChangeActiveFn = (params: { id: string; value: boolean }) => void;

export type DataTableContextValue = {
  onChangeActive: OnChangeActiveFn;
};

type Props = {
  children: ReactNode;
  value: Omit<DataTableContextValue, "formStates">;
};

const DataTableContext = createContext<DataTableContextValue | undefined>(undefined);

export const DataTableProvider = ({ children, ...props }: Props) => {
  const value: DataTableContextValue = {
    ...props.value
  };

  return <DataTableContext.Provider value={value}>{children}</DataTableContext.Provider>;
};

export const useAdminsContext = () => {
  const context = useContext(DataTableContext);

  if (context === undefined) {
    throw new Error("useAdmins must be used within a DataTableProvider");
  }

  return context;
};

export default DataTableProvider;
