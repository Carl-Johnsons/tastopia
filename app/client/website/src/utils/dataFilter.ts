import { IAdvancePaginatedMetadata } from "@/generated/interfaces/common.interface";

/**
 * Filters the source of an image so that it could be properly handled.
 *
 * @param source The source of the image
 * @returns The source of the image if it is not empty, otherwise undefined
 */
export function filterImageSource(source: string | null | undefined) {
  if (!source) return undefined;
  return source !== "" ? source : undefined;
}

export type PaginatedData<T> = {
  paginatedData: T[];
  metadata?: IAdvancePaginatedMetadata;
};

/**
 * Filters out duplicate items from paginated data based on a unique identifier
 * @param pages - Array of pages containing paginated data
 * @param idKey - Key to use as unique identifier
 * @returns Array of unique items
 */
export const filterUniqueItems = <T extends Record<string, any>>(
  pages: PaginatedData<T>[],
  idKey: keyof T = "id"
): T[] => {
  const seenIds = new Set<string>();

  return pages.flatMap(page =>
    page.paginatedData.filter(item => {
      const id = String(item[idKey]);
      if (seenIds.has(id)) return false;
      seenIds.add(id);
      return true;
    })
  );
};
