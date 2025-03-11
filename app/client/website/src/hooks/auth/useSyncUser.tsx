import { getUserDetails } from "@/api/user";
import { saveUserData } from "@/slices/user.slice";
import { useCallback, useState } from "react";
import { useDispatch } from "react-redux";

type Status = "todo" | "loading" | "success" | "error";

const useSyncUser = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [status, setStatus] = useState<Status>("todo");
  const dispatch = useDispatch();

  /**
   * Fetch the user's details and then save it.
   */
  const fetch = useCallback(async () => {
    setIsLoading(true);
    setStatus("loading");

    try {
      const user = await getUserDetails();
      dispatch(saveUserData({ ...user }));
      setStatus("success");
    } catch (error) {
      setStatus("error");
    } finally {
      setIsLoading(false);
    }
  }, [getUserDetails, dispatch]);

  return { fetch, isLoading, status };
};

export default useSyncUser;
