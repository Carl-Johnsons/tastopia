import { redirect } from "next/navigation";

export default function AdminPage() {
  redirect("/statistics");
  return null;
}
