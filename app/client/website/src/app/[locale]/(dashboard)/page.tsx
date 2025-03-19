import { redirect } from "next/navigation";

export default function AdminPage() {
  redirect("/statistics/income");
  return null;
}
