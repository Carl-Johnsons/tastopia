import { ReactNode, createContext, useContext } from "react";

export type FormContextValue = {
  submitForm?: () => void;
};

type Props = {
  children: ReactNode;
  value: Omit<FormContextValue, "formStates">;
};

const FormContext = createContext<FormContextValue | undefined>(undefined);

export const FormProvider = ({ children, value }: Props) => {
  return <FormContext.Provider value={value}>{children}</FormContext.Provider>;
};

export const useFormContext = () => {
  const context = useContext(FormContext);

  if (context === undefined) {
    throw new Error("useFormContext must be used within a FormProvider");
  }

  return context;
};

export default FormProvider;
