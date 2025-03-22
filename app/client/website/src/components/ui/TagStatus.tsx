import React from "react";

const ReportStatus = ({ status }: { status: string }) => {
  const renderStatus = (status: string) => {
    switch (status) {
      case "Inactive":
        return (
          <span className='flex items-center gap-1 text-sm text-red-500'>
            <span className='size-2 rounded-full bg-red-500'></span>
            {status}
          </span>
        );
      case "Active":
        return (
          <span className='flex items-center gap-1 text-sm text-green-500'>
            <span className='size-2 rounded-full bg-green-500'></span>
            {status}
          </span>
        );
      case "Pending":
        return (
          <span className='flex items-center gap-1 text-sm text-blue-500'>
            <span className='size-2 rounded-full bg-blue-500'></span>
            {status}
          </span>
        );
    }
  };

  return renderStatus(status);
};

export default ReportStatus;
