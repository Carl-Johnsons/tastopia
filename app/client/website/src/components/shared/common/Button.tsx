"use client";

import { Button, ButtonProps } from "@/components/ui/button";
import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger
} from "@/components/ui/tooltip";
import useWindowDimensions from "@/hooks/useWindowDimensions";
import { cn } from "@/lib/utils";
import { cva } from "class-variance-authority";
import { ReactNode, useMemo } from "react";
import { LoadingIcon } from "../icons";

const buttonVariants = cva(
  "inline-flex items-center justify-center gap-2 whitespace-nowrap rounded-md text-sm font-medium transition-colors focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring disabled:pointer-events-none disabled:opacity-50 [&_svg]:pointer-events-none [&_svg]:size-4 [&_svg]:shrink-0",
  {
    variants: {
      variant: {
        default: "bg-primary text-primary-foreground shadow hover:bg-primary/90",
        destructive:
          "bg-destructive text-destructive-foreground shadow-sm hover:bg-destructive/90",
        outline:
          "border border-input bg-background shadow-sm hover:bg-accent hover:text-accent-foreground",
        secondary:
          "bg-secondary text-secondary-foreground shadow-sm hover:bg-secondary/80",
        ghost: "hover:bg-accent hover:text-accent-foreground",
        link: "text-primary underline-offset-4 hover:underline"
      },
      size: {
        default: "h-9 px-4 py-2",
        sm: "h-8 rounded-md px-3 text-xs",
        lg: "h-10 rounded-md px-8",
        icon: "h-9 w-9"
      }
    },
    defaultVariants: {
      variant: "default",
      size: "default"
    }
  }
);

export type InteractiveButtonProps = ButtonProps & {
  icon: ReactNode;
  isLoading?: boolean;
  loaderClassName?: string;
  title: string;
  onClick?: () => void;
  className?: string;
  noTruncateText?: boolean;
  noText?: boolean;
  toolTip?: boolean;
};

export const InteractiveButton = ({
  icon,
  isLoading,
  loaderClassName,
  noTruncateText,
  className,
  onClick,
  title,
  noText,
  toolTip,
  disabled,
  ...props
}: InteractiveButtonProps) => {
  const { width } = useWindowDimensions();
  const showToolTip = useMemo(() => width < 768 || toolTip, [width, toolTip]);
  const RenderedContent = useMemo(
    () => (
      <div className={`relative flex items-center gap-1.5`}>
        {isLoading ? (
          <LoadingIcon className={`text-white_black ${loaderClassName}`} />
        ) : (
          icon
        )}

        {!noText && (
          <span
            className={`${!noTruncateText && "hidden md:inline"} text-white_black text-sm font-medium`}
          >
            {title}
          </span>
        )}
      </div>
    ),
    [icon, title, noText, isLoading, noTruncateText, loaderClassName]
  );

  return showToolTip ? (
    <TooltipProvider delayDuration={500}>
      <Tooltip>
        <TooltipTrigger
          onClick={onClick}
          disabled={disabled}
          className={cn(
            buttonVariants({ variant: "default", size: "default", className })
          )}
        >
          {RenderedContent}
        </TooltipTrigger>
        <TooltipContent className='rounded-md bg-gray-900 dark:border dark:border-gray-200'>
          <div className='text-sm text-white'>
            <span>{title}</span>
          </div>
        </TooltipContent>
      </Tooltip>
    </TooltipProvider>
  ) : (
    <Button
      disabled={disabled}
      onClick={onClick}
      className={className}
      {...props}
    >
      {RenderedContent}
    </Button>
  );
};

export const DataTableButton = (props: InteractiveButtonProps) => {
  return (
    <InteractiveButton
      noText
      toolTip
      {...props}
    />
  );
};

export default InteractiveButton;
