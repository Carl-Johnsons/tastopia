import Table from "@/components/screen/report/recipe/DataTable";
import { ChevronRight } from "lucide-react";

export default async function Recipe() {
  return (
    <div className="flex size-full flex-col justify-center gap-4">
      <Table />
    </div>
  );
}
