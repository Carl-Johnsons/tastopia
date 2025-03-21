import { ReportStatus } from "@/constants/reports";

export const ReportStatusText = ({
  status,
  coloring
}: {
  status: ReportStatus;
  coloring?: boolean;
}) => {
  return (
    <div className='flex min-w-[75px] items-center gap-1 text-sm'>
      {status === ReportStatus.Done ? (
        <>
          <div className='size-2 rounded-full bg-green-500' />
          <span className={`font-medium ${coloring && "text-green-500"}`}>Done</span>
        </>
      ) : (
        <>
          <div className='size-2 rounded-full bg-red-500' />
          <span className={`font-medium ${coloring && "text-red-500"}`}>Pending</span>
        </>
      )}
    </div>
  );
};

export const ItemStatusText = ({
  isActive,
  coloring
}: {
  isActive: boolean;
  coloring?: boolean;
}) => {
  return (
    <div className='flex-center flex min-w-[80px] gap-2'>
      {isActive ? (
        <>
          <div className='size-2.5 rounded-full bg-green-500' />
          <span className={`font-medium ${coloring && "text-green-500"}`}>Active</span>
        </>
      ) : (
        <>
          <div className='size-2.5 rounded-full bg-red' />
          <span className={`font-medium ${coloring && "text-red"}`}>Inactive</span>
        </>
      )}
    </div>
  );
};

export default ReportStatusText;
