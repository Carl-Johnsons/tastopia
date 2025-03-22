import React, { ReactNode } from "react";
import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger
} from "@/components/ui/tooltip";
import { Button } from "@/components/ui/button";

type TooltipButtonProps = {
  title: string;
  onClick: () => void;
  icon: ReactNode;
  className: string;
  delayDuration?: number;
};

const TooltipButton = ({
  title,
  onClick,
  icon,
  className,
  delayDuration = 100
}: TooltipButtonProps) => {
  return (
    <TooltipProvider>
      <Tooltip delayDuration={delayDuration}>
        <TooltipTrigger asChild>
          <Button
            className={className}
            onClick={onClick}
          >
            {icon}
          </Button>
        </TooltipTrigger>
        <TooltipContent>
          <p className='text-white_black'>{title}</p>
        </TooltipContent>
      </Tooltip>
    </TooltipProvider>
  );
};

export default TooltipButton;
