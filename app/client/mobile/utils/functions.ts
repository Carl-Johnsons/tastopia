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
    android: uri?.replace("localhost", "10.0.2.2")
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
