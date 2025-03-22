import { Tag } from "@/types/tag";
import {
  createContext,
  Dispatch,
  ReactNode,
  SetStateAction,
  useContext,
  useMemo,
  useState
} from "react";

type TagsContextProps = {
  tags: Tag[];
  setTags: Dispatch<SetStateAction<Tag[]>>;
};

const TagsContext = createContext<TagsContextProps | undefined>(undefined);

export const TagsProvider = ({ children }: { readonly children: ReactNode }) => {
  const [tags, setTags] = useState<Tag[]>([]);

  const value = useMemo(() => {
    return {
      tags,
      setTags
    };
  }, [tags]);

  return <TagsContext.Provider value={value}>{children}</TagsContext.Provider>;
};

export function useTags() {
  const context = useContext(TagsContext);

  if (context === undefined) {
    throw new Error("useTags must be use within a TagProvider");
  }

  return context;
}
