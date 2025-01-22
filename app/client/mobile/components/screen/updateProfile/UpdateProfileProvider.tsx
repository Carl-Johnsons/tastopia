import { Dispatch, ReactNode, SetStateAction, createContext } from "react";

export type UpdateProfileContext = {
  avatar?: ImageFileType;
  setAvatar: Dispatch<SetStateAction<ImageFileType | undefined>>;
  background?: ImageFileType;
  setBackground: Dispatch<SetStateAction<ImageFileType | undefined>>;
  triggerSubmit?: () => void;
  setTriggerSubmit?: Dispatch<SetStateAction<(() => void) | undefined>>;
  isLoading: boolean;
  setIsLoading: Dispatch<SetStateAction<boolean>>;
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
