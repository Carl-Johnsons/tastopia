"use client";
import DataTable from "@/components/screen/tags/DataTable";
import { TagsProvider } from "@/components/screen/tags/TagsContext";

export default function Tags() {
  return (
    <div className='flex size-full flex-col justify-center gap-4'>
      <TagsProvider>
        <DataTable />
      </TagsProvider>
    </div>
  );
}
