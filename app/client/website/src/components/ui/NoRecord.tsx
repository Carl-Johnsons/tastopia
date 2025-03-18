import { Inbox } from "lucide-react";

const NoRecord = () => {
  return (
    <div className="flex flex-col items-center justify-center py-10">
      <Inbox className="size-10 text-gray-400" />
      <p className="mt-2 text-sm text-gray-500">No records found.</p>
    </div>
  );
};

export default NoRecord;
