import {
  Dispatch,
  ReactNode,
  SetStateAction,
  createContext,
  useContext,
  useState
} from "react";

export type OnChangeActiveFn = (params: { id: string; value: boolean }) => void;
export type FormStates = {
  isSubmitting: boolean;
  setIsSubmitting: Dispatch<SetStateAction<boolean>>;
  submitForm?: () => void;
  setSubmitForm: Dispatch<SetStateAction<() => void>>;
};

export type DataTableContextValue = {
  onChangeActive: OnChangeActiveFn;
  formStates: FormStates;
};

type Props = {
  children: ReactNode;
  value: Omit<DataTableContextValue, "formStates">;
};

const DataTableContext = createContext<DataTableContextValue | undefined>(undefined);

export const DataTableProvider = ({ children, ...props }: Props) => {
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [submitForm, setSubmitForm] = useState<() => void>();

  const value: DataTableContextValue = {
    ...props.value,
    formStates: {
      isSubmitting,
      setIsSubmitting,
      submitForm,
      setSubmitForm
    }
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
