import {
  handleError as baseHandler,
  handleBareError as baseBareHanlder,
  ApiError
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

  /**
   * Handle ApiError, with translations.
   */
  const handleError = useCallback(
    (error: ApiError) => {
      baseHandler(error, t);
    },
    [t]
  );

  /**
   * Use this when you call an axios request with React Query.
   */
  const withErrorProcessor = useCallback(async <T>(queryFn: QueryFn<T>): Promise<T> => {
    const res = await queryFn();

    if (!res.ok) {
      throw new ApiError(res);
    }

    return res.data;
  }, []);

  /**
   * Use this when you call an axios request directly in client code, without
   * using React Query.
   */
  const withBareErrorHandler = useCallback(
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

  return { handleError, withErrorProcessor, withBareErrorHandler };
};
