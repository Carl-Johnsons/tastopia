import DataTable from "@/components/screen/users/DataTable";

export default async function Users() {
  return (
    <div className='flex size-full flex-col justify-center gap-4'>
      <DataTable />
    </div>
  );
}
