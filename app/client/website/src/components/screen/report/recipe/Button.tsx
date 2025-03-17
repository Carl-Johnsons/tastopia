import { BanIcon } from "@/components/shared/icons";
import { Button } from "@/components/ui/button";
import { Check, RotateCw, Search } from "lucide-react";
import { useRouter } from "next/navigation";
import { ReactNode, useCallback } from "react";

type TableDataButtonProps = {
  title: string;
  targetId: string;
  onSuccess?: () => void;
  className?: string;
};

export const ViewDetailButton = ({
  title,
  onSuccess,
  targetId,
  className
}: TableDataButtonProps) => {
  const router = useRouter();

  const handleClick = useCallback(() => {
    router.push(`/reports/recipes/detail/${targetId}`);
    onSuccess && onSuccess();
  }, [onSuccess, targetId]);

  return (
    <InteractiveButton
      title={title}
      icon={<Search className='text-white_black' />}
      onClick={handleClick}
      className={className}
      noText
      toolTip
    />
  );
};

export const RestoreButton = ({
  title,
  targetId,
  onSuccess,
  className
}: TableDataButtonProps) => {
  const handleClick = useCallback(() => {
    onSuccess && onSuccess();
  }, [onSuccess, targetId]);

  return (
    <InteractiveButton
      title={title}
      icon={<RotateCw className='text-white_black' />}
      onClick={handleClick}
      className={`bg-green-400 hover:bg-green-500 ${className}`}
      noText
      toolTip
    />
  );
};

export const MarkAsCompletedButton = ({
  title,
  targetId,
  onSuccess,
  className
}: TableDataButtonProps) => {
  const handleClick = useCallback(() => {
    onSuccess && onSuccess();
  }, [onSuccess, targetId]);

  return (
    <InteractiveButton
      title={title}
      icon={<Check className='text-white_black' />}
      onClick={handleClick}
      className={`bg-purple-400 hover:bg-purple-500 ${className}`}
      noText
      toolTip
    />
  );
};

export const RejectButton = ({
  title,
  targetId,
  onSuccess,
  className
}: TableDataButtonProps) => {
  const handleClick = useCallback(() => {
    onSuccess && onSuccess();
  }, [onSuccess, targetId]);

  return (
    <InteractiveButton
      title={title}
      icon={<BanIcon className='text-white_black' />}
      onClick={handleClick}
      className={`bg-red-400 hover:bg-red-500 ${className}`}
      noText
      toolTip
    />
  );
};

export const DeleteButton = ({
  title,
  targetId,
  onSuccess,
  className
}: TableDataButtonProps) => {
  const handleClick = useCallback(() => {
    onSuccess && onSuccess();
  }, [onSuccess, targetId]);

  return (
    <InteractiveButton
      title={title}
      icon={<BanIcon className='text-white_black' />}
      onClick={handleClick}
      className={`bg-red-400 hover:bg-red-500 ${className}`}
      noText
      toolTip
    />
  );
}

type InteractiveButtonProps = {
  icon: ReactNode;
  title: string;
  onClick?: () => void;
  className?: string;
  noTruncateText?: boolean;
  noText?: boolean;
  toolTip?: boolean;
};

export const InteractiveButton = ({
  icon,
  title,
  onClick,
  className,
  noTruncateText,
  noText,
  toolTip
}: InteractiveButtonProps) => {
  return (
    <Button
      className={`group relative flex items-center gap-1 ${className}`}
      onClick={onClick}
    >
      {icon}

      {!noText && (
        <span
          className={`${!noTruncateText && "hidden 2xl:inline"} text-white_black font-medium text-sm`}
        >
          {title}
        </span>
      )}

      {toolTip && (
        <div className='absolute bottom-[110%] left-1/2 z-[-999] mb-2 w-max -translate-x-1/2 rounded-md bg-gray-900 px-3 py-1 text-sm text-white opacity-0 transition-opacity duration-300 group-hover:z-[999] group-hover:opacity-100'>
          <span>{title}</span>
        </div>
      )}
    </Button>
  );
};

export default InteractiveButton;
