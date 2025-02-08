import { Platform } from "react-native";
export type BasicOptionFields = {
  id: string;
  name?: string;
  [key: string]: any;
};

export const transformToSelectOptions = (objectList: BasicOptionFields[]) =>
  objectList?.map(obj => ({
    label: obj?.name!,
    value: obj?.id!
  }));

export const transformListWithIndex = <T>(objectList: T[], startWith: number = 0) =>
  !objectList
    ? []
    : objectList.map(
        (object, i) =>
          ({
            index: i + 1 + startWith,
            ...object
          }) as T & { index: number }
      );

export const transformPlatformURI = (uri: string) => {
  return Platform.select({
    ios: uri,
    android: uri?.replace("localhost", "10.0.2.2.nip.io")
  }) as string;
};

export const isFalsy = (
  value: string | null | undefined
): value is null | undefined | "" | "[]" => {
  return (
    value === null ||
    value === undefined ||
    value === "[]" ||
    (typeof value === "string" && value.trim() === "")
  );
};

export const isNumeric = (n: any) => {
  return !isNaN(parseFloat(n)) && isFinite(n);
};

export const getUniqueItemsWithSet = (arr1: string[], arr2: string[]) => {
  const set1 = new Set(arr1);
  const set2 = new Set(arr2);

  return [
    ...Array.from(set1).filter(item => !set2.has(item)),
    ...Array.from(set2).filter(item => !set1.has(item))
  ];
};
