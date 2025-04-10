import {
  handleError as baseHanlder,
  handleBareError as baseBareHanlder
} from "@/lib/error/errorHanler";
import { ErrorResponse, Response } from "@/types/common";
import { useTranslations } from "next-intl";
import { useCallback } from "react";

type QueryFn<T> = () => Promise<Response<T>>;
type QueryOptions<T> = {
  onSuccess?: (data: T) => void;
  onFailure?: (error: ErrorResponse) => void;
};

export const useErrorHandler = () => {
  const t = useTranslations("error");

  const handleError = useCallback(
    (error: any) => {
      baseHanlder(error, t);
    },
    [t]
  );

  /**
   * Use this when you call an axios request directly in client code, without
   * using React Query.
   */
  const withBareErrorHanler = useCallback(
    async <T>(queryFn: QueryFn<T>, options?: QueryOptions<T>) => {
      const res = await queryFn();

      if (!res.ok) {
        baseBareHanlder(res, t);
        options?.onFailure?.(res);
        return;
      }

      options?.onSuccess?.(res.data);
    },
    [t]
  );

  return { handleError, withBareErrorHanler };
};
