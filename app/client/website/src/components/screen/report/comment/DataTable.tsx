"use client";

import Loader from "@/components/ui/Loader";
import useDebounce from "@/hooks/useDebounce";
import { format } from "date-fns";
import Image from "next/image";
import {
  ChangeEvent,
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState
} from "react";
import DataTable, { SortOrder, TableColumn } from "react-data-table-component";
import { ChevronRight } from "lucide-react";
import NoRecord from "@/components/ui/NoRecord";
import SearchBar from "../../users/SearchBar";
import { ReportStatus } from "@/constants/reports";
import DataTableProvider, {
  DataTableContext,
  DataTableContextValue,
  OnChangeActiveFn
} from "./Provider";
import { CommentReportActionButtonsProps } from "@/types/report";
import { MarkAsCompletedButton, ReopenReportButton, ViewDetailButton } from "./Button";
import { useGetCommentReports } from "@/api/comment";
import { IAdminReportCommentResponse } from "@/generated/interfaces/recipe.interface";
import ReportStatusText from "../common/StatusText";
import useDataTableStyles from "@/hooks/table/useDataTableStyle";
import useLocaleTable from "@/hooks/table/useLocaleTable";
import { useLocale } from "next-intl";

const columns: TableColumn<IAdminReportCommentResponse>[] = [
  {
    name: "Comment Owner",
    selector: row => row.commentOwnerUsername,
    maxWidth: "160px",
    hide: 952,
    sortable: true
  },
  {
    name: "Content",
    sortable: true,
    minWidth: "200px",
    cell: ({ commentContent }) => <span className='py-2'>{commentContent}</span>
  },
  {
    name: "Recipe Image",
    hide: 1668,
    width: "140px",
    center: true,
    cell: ({ recipeImageURL }) => (
      <div className='p-2'>
        <Image
          src={recipeImageURL}
          alt={""}
          width={90}
          height={50}
          className='h-[50px] w-[90px] rounded-lg object-cover'
        />
      </div>
    )
  },
  {
    name: "Reporter",
    selector: row => row.reporterUsername,
    width: "160px",
    hide: 1776,
    sortable: true
  },
  {
    name: "Report Reason",
    hide: 1368,
    selector: row => row.reportReason,
    sortable: true,
    wrap: true
  },
  {
    name: "Created Date",
    sortable: true,
    width: "160px",
    center: true,
    hide: 1476,
    cell: ({ createdAt }) => {
      return (
        <span className='text-ellipsis text-nowrap text-sm'>
          {format(new Date(createdAt), "dd/MM/yyyy")}
        </span>
      );
    }
  },
  {
    name: "Status",
    sortable: true,
    width: "120px",
    center: true,
    hide: 500,
    selector: row => row.status,
    cell: ({ status }) => {
      return (
        <ReportStatusText
          status={status as ReportStatus}
          coloring
        />
      );
    }
  },
  {
    name: "Actions",
    center: true,
    width: "150px",
    cell: ({ commentId, recipeId, reportId, status }) => {
      return (
        <ActionButtons
          reportId={reportId}
          recipeId={recipeId}
          targetId={commentId}
          status={status as ReportStatus}
        />
      );
    }
  }
];

const ActionButtons = ({
  reportId,
  recipeId,
  targetId,
  status
}: CommentReportActionButtonsProps) => {
  const { onChangeActive } = useContext(DataTableContext) as DataTableContextValue;

  return (
    <div className='flex gap-2'>
      <ViewDetailButton
        title='View detail'
        recipeId={recipeId}
        targetId={targetId}
      />
      {status === ReportStatus.Done ? (
        <ReopenReportButton
          title='Restore'
          targetId={reportId}
          onSuccess={() => {
            onChangeActive({ reportId, value: true });
          }}
        />
      ) : (
        <MarkAsCompletedButton
          title='Mark as completed'
          targetId={reportId}
          onSuccess={() => {
            onChangeActive({ reportId, value: false });
          }}
        />
      )}
    </div>
  );
};

export const columnFieldMap: Record<string, keyof IAdminReportCommentResponse> = {
  "Comment Owner": "commentOwnerUsername",
  "Recipe Image": "recipeImageURL",
  Reporter: "reporterUsername",
  "Report Reason": "reportReason",
  "Created Date": "createdAt",
  Status: "status"
};

export default function Table() {
  const [limit, setLimit] = useState(10);
  const [skip, setSkip] = useState(0);
  const [sortBy, setSortBy] = useState("createdAt");
  const [sortOrder, setSortOrder] = useState("DESC");
  const lang = useLocale();
  const [keyword, setKeyword] = useState("");
  const debouncedValue = useDebounce(keyword, 800);

  const {
    data: fetchedData,
    isLoading,
    isFetching,
    refetch
  } = useGetCommentReports({
    skip,
    sortBy,
    sortOrder,
    limit,
    lang,
    keyword: debouncedValue
  });

  const totalRow = useMemo(
    () => (fetchedData?.metadata?.totalRow ? fetchedData.metadata.totalRow : 0),
    [fetchedData]
  );
  const [reports, setReports] = useState<IAdminReportCommentResponse[]>([]);
  const { tableStyles } = useDataTableStyles();
  const tableLocale = useLocaleTable();

  const handleChangeRowPerPage = useCallback((numOfRows: number) => {
    setLimit(numOfRows);
  }, []);

  const handleChangePage = useCallback((page: number) => {
    setSkip(page - 1);
  }, []);

  const onSort = useCallback(
    (
      selectedColumn: TableColumn<IAdminReportCommentResponse>,
      sortDirection: SortOrder
    ) => {
      const sortBy = columnFieldMap[selectedColumn.name as string];
      const sortOrder = sortDirection.toString().toUpperCase();

      setSortBy(sortBy);
      setSortOrder(sortOrder);
    },
    []
  );

  const handleSearch = useCallback((e: ChangeEvent<HTMLInputElement>) => {
    setKeyword(e.target.value.trim());
    setSkip(0);
  }, []);

  const onChangeActive = useCallback<OnChangeActiveFn>(({ reportId, value }) => {
    setReports(prev => {
      return prev.map(report => {
        if (report.reportId !== reportId) return report;

        return {
          ...report,
          status: value ? ReportStatus.Pending : ReportStatus.Done
        };
      });
    });
  }, []);

  const contextValue: DataTableContextValue = useMemo(
    () => ({
      onChangeActive
    }),
    [onChangeActive]
  );

  useEffect(() => {
    refetch();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [skip, sortBy, sortOrder, debouncedValue, limit]);

  useEffect(() => {
    setReports(fetchedData?.paginatedData || []);
  }, [fetchedData]);

  return (
    <>
      <div className='flex-center mt-4 flex-col gap-4'>
        <SearchBar
          onChange={handleSearch}
          isLoading={isLoading || isFetching}
        />

        <div className='flex gap-2 self-start'>
          <span className='text-gray-500'>Administer Reports</span>
          <ChevronRight className='text-black_white' />
          <span className='text-black_white'>Comment</span>
        </div>
      </div>

      <DataTableProvider value={contextValue}>
        <DataTable
          customStyles={tableStyles}
          columns={columns}
          data={reports}
          responsive
          striped
          highlightOnHover
          progressPending={isLoading || isFetching}
          progressComponent={<Loader />}
          noDataComponent={<NoRecord />}
          onChangeRowsPerPage={handleChangeRowPerPage}
          onChangePage={handleChangePage}
          pagination
          paginationServer
          paginationTotalRows={totalRow}
          sortServer
          onSort={onSort}
          paginationComponentOptions={tableLocale}
        />
      </DataTableProvider>
    </>
  );
}
