import { UpdateProfileContext } from "@/components/screen/updateProfile/UpdateProfileProvider";
import { useContext } from "react";

/**
 * Provide the general states for the update profile screen.
 */
export default function useUpdateProfile(): UpdateProfileContext {
  const context = useContext(UpdateProfileContext);

  if (!context) {
    throw new Error("useUpdateProfile must be used within an UpdateProfileProvider");
  }

  return { ...context };
}
