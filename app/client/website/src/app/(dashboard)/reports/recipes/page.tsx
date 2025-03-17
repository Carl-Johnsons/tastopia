import Table from "@/components/screen/report/recipe/DataTable";
import { ChevronRight } from "lucide-react";

export default async function Recipe() {
  return (
    <div className='flex size-full flex-col justify-center gap-10'>
      <div className='flex gap-2'>
        <span className='text-gray-500'>Administer Reports</span>
        <ChevronRight className='text-black_white' />
        <span className='text-black_white'>Recipe</span>
      </div>
      <Table />
    </div>
  );
}
