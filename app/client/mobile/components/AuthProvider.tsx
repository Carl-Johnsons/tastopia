import { AuthState } from "@/slices/auth.slice";
import { use } from "i18next";
import {
  Dispatch,
  FC,
  ReactNode,
  SetStateAction,
  createContext,
  useContext,
  useState
} from "react";

export type Tokens = AuthState;
export type AuthContext = {
  tokens?: Tokens;
  setTokens: Dispatch<SetStateAction<Tokens | undefined>>;
  identifier?: string; 
  setIdentifier: Dispatch<SetStateAction<string | undefined>>;
};

const AuthContext = createContext<AuthContext | null>(null);

type AuthProviderProps = {
  children: ReactNode;
};

export const AuthProvider: FC<AuthProviderProps> = ({ children }) => {
  const [tokens, setTokens] = useState<Tokens>();
  const [identifier, setIdentifier] = useState<string>();
  const contextValue: AuthContext = {
    tokens,
    setTokens,
    identifier,
    setIdentifier
  };

  return (
    <AuthContext.Provider value={contextValue}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuthContext = () => {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error("useAuthContext must be used within a AuthProvider");
  }

  return context;
};

export default AuthProvider;
