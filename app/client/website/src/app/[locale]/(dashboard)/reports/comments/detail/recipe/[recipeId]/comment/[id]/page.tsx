"use client";

import { useGetCommentReport } from "@/api/comment";
import Loading from "@/app/[locale]/(dashboard)/users/[id]/loading";
import CommentDetail from "@/components/screen/report/comment/CommentDetail";
import ReportList from "@/components/screen/report/common/ReportList";
import SomethingWentWrong from "@/components/shared/common/Error";
import { ReportType } from "@/constants/reports";
import { Link } from "@/i18n/navigation";
import { CommentDetailParamProps } from "@/types/link";
import { ChevronRight } from "lucide-react";
import { useLocale } from "next-intl";

export default function Page({ params }: CommentDetailParamProps) {
  const { id, recipeId } = params;
  const lang = useLocale();

  const { data, isError, isLoading } = useGetCommentReport({
    commentId: id,
    recipeId,
    options: {
      lang
    }
  });

  if (isError) return <SomethingWentWrong />;
  if (isLoading || !data) return <Loading />;
  const { comment, reports } = data;

  return (
    <div className='flex flex-col gap-10'>
      <div className='flex gap-2'>
        <span className='text-gray-500'>Administer Reports</span>
        <ChevronRight className='text-black_white' />
        <Link href='/reports/comments'>
          <span className='text-black_white'>Comment</span>
        </Link>
        <ChevronRight className='text-black_white' />
        <span className='text-black_white'>Detail</span>
      </div>
      <div className='flex-center container flex-col gap-10'>
        <CommentDetail
          comment={comment}
          recipeId={recipeId}
        />
        <ReportList
          reports={reports}
          targetId={comment.id}
          recipeId={recipeId}
          reportType={ReportType.COMMENT}
          className='w-full'
        />
      </div>
    </div>
  );
}
