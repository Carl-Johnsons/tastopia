import { getCommentReportById } from "@/actions/comment.action";
import CommentDetail from "@/components/screen/report/comment/CommentDetail";
import ReportList from "@/components/screen/report/common/ReportList";
import { ReportType } from "@/constants/reports";
import { Link } from "@/i18n/navigation";
import { CommentDetailParamProps } from "@/types/link";
import { ChevronRight } from "lucide-react";
import { getLocale } from "next-intl/server";

export default async function Page({ params }: CommentDetailParamProps) {
  const { id, recipeId } = params;
  const lang = await getLocale();

  try {
    const { reports, comment } = await getCommentReportById({
      commentId: id,
      recipeId,
      options: {
        lang
      }
    });

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
            recipeId={recipeId}
            reportType={ReportType.COMMENT}
            className='w-full'
          />
        </div>
      </div>
    );
  } catch (error) {
    console.log(error);
    return <div>Something went wrong. :(</div>;
  }
}
