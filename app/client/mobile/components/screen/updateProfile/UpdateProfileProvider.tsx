import { Dispatch, ReactNode, SetStateAction, createContext } from "react";

export type UpdateProfileContext = {
  triggerSubmit?: () => void;
  setTriggerSubmit?: Dispatch<SetStateAction<(() => void) | undefined>>;
  onChangeGenderValue?: (newValue: string) => void;
  setOnChangeGenderValue?: Dispatch<SetStateAction<((newValue: string) => void) | undefined>>;
};

type UpdateProfileProviderProps = {
  children: ReactNode;
  value: UpdateProfileContext;
};

export const UpdateProfileContext = createContext<UpdateProfileContext | undefined>(
  undefined
);

export const UpdateProfileProvider = ({
  children,
  value
}: UpdateProfileProviderProps) => {
  return (
    <UpdateProfileContext.Provider value={value}>
      {children}
    </UpdateProfileContext.Provider>
  );
};

export default UpdateProfileProvider;
