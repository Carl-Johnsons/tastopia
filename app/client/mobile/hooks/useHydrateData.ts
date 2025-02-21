import { filterUniqueItems, PaginatedData } from "@/utils/dataFilter";
import { Dispatch, SetStateAction, useEffect, useState } from "react";
import { InfiniteData } from "react-query";

type UseHydrateDataParams<T> = {
  source: InfiniteData<PaginatedData<T>> | undefined;
  setter: Dispatch<SetStateAction<T[] | undefined>>;
};

type Data = {
  id: string;
};

type UseHydrateDataResult = {
  /** Whether the data is being hydrated. */
  isHydrating: boolean;
};

/**
 * Hydrates the state with the source data if it is not empty.
 *
 * @param source The source data
 * @param setter The state setter
 */
export const useHydrateData = <T extends Data>({
  source,
  setter
}: UseHydrateDataParams<T>): UseHydrateDataResult => {
  const [isHydrating, setIsHydrating] = useState<boolean>(false);

  useEffect(() => {
    if (source?.pages) {
      try {
        setIsHydrating(true);
        const data = filterUniqueItems(source.pages);
        setter(data);
      } catch (error) {
        throw new Error("Failed to hydrate data");
      } finally {
        setIsHydrating(false);
      }
    }
  }, [source, setter]);

  return { isHydrating };
};

export default useHydrateData;
