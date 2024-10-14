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
