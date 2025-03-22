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
  openCreateDialog: boolean;
  setOpenCreateDialog: Dispatch<SetStateAction<boolean>>;
  openUpdateDialog: boolean;
  setOpenUpdateDialog: Dispatch<SetStateAction<boolean>>;
  setTagIdToUpdate: Dispatch<SetStateAction<string | undefined>>;
  createTagInContext: (newTag: Tag) => void;
  updateTagInContext: (updatedTag: Tag) => void;
  getTagToUpdate: () => Tag | undefined;
};

const TagsContext = createContext<TagsContextProps | undefined>(undefined);

export const TagsProvider = ({ children }: { readonly children: ReactNode }) => {
  const [tags, setTags] = useState<Tag[]>([]);
  const [openCreateDialog, setOpenCreateDialog] = useState(false);
  const [openUpdateDialog, setOpenUpdateDialog] = useState(false);
  const [tagIdToUpdate, setTagIdToUpdate] = useState<string>();

  const createTagInContext = (newTag: Tag) => {
    setTags(prevTags => [...prevTags, newTag]);
  };

  const updateTagInContext = (updatedTag: Tag) => {
    setTags(prevTags =>
      prevTags.map(tag => (tag.id === updatedTag.id ? updatedTag : tag))
    );
  };

  const getTagToUpdate = () => {
    return tags.find(tag => tag.id === tagIdToUpdate);
  };

  const value = useMemo(() => {
    return {
      tags,
      setTags,
      openCreateDialog,
      setOpenCreateDialog,
      openUpdateDialog,
      setOpenUpdateDialog,
      setTagIdToUpdate,
      createTagInContext,
      updateTagInContext,
      getTagToUpdate
    };
  }, [tags, openCreateDialog, openUpdateDialog, tagIdToUpdate]);

  return <TagsContext.Provider value={value}>{children}</TagsContext.Provider>;
};

export function useTags() {
  const context = useContext(TagsContext);

  if (context === undefined) {
    throw new Error("useTags must be use within a TagProvider");
  }

  return context;
}
