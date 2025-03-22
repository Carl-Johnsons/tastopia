import React from "react";

const ReportStatus = ({ status }: { status: string }) => {
  return status === "Inactive" ? (
    <span className='flex items-center gap-1 text-sm text-red-500'>
      <span className='size-2 rounded-full bg-red-500'></span>
      {status}
    </span>
  ) : (
    <span className='flex items-center gap-1 text-sm text-green-500'>
      <span className='size-2 rounded-full bg-green-500'></span>
      {status}
    </span>
  );
};

export default ReportStatus;
