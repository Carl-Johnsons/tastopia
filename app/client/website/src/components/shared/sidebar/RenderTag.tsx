import React from "react";
import { Link } from "@/i18n/navigation";
import { Badge } from "@/components/ui/badge";

interface Props {
  _id: string;
  name: string;
  totalQuestions?: number;
  showCount?: boolean;
  wrapperClasses?: string;
}

const RenderTag = ({
  _id,
  name,
  totalQuestions,
  showCount,
  wrapperClasses = ""
}: Props) => {
  return (
    <Link
      href={`/tags/${_id}`}
      className={`hover:background-light800_dark400 flex items-center justify-between gap-2 rounded-lg ${wrapperClasses || ""}`}
    >
      <Badge className='subtle-medium background-light600_dark300 text-light400_light500 hover:bg-light-500 hover:text-primary-800 dark:hover:bg-dark-450 rounded-full border-none px-4 py-2 uppercase'>
        {name}
      </Badge>

      {showCount && (
        <p className='small-medium text-dark500_light700'>{totalQuestions}</p>
      )}
    </Link>
  );
};

export default RenderTag;
