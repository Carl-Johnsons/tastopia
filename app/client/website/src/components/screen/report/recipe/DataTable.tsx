"use client";

import { useGetRecipeReports } from "@/api/recipe";
import Loader from "@/components/ui/Loader";
import { colors } from "@/constants/colors";
import { IAdminReportRecipeResponse } from "@/generated/interfaces/recipe.interface";
import useDebounce from "@/hooks/useDebounce";
import { format } from "date-fns";
import Image from "next/image";
import { useCallback, useMemo, useState } from "react";
import DataTable, { SortOrder, TableColumn, TableStyles } from "react-data-table-component";
import {
  MarkAsCompletedButton,
  RejectButton,
  RestoreButton,
  ViewDetailButton
} from "./Button";
import { customStyles } from "@/constants/styles";

const columns: TableColumn<IAdminReportRecipeResponse>[] = [
  {
    name: "Recipe Name",
    selector: row => row.recipeTitle,
    sortable: true,
  },
  {
    name: "Recipe Owner",
    selector: row => row.recipeOwnerUsername,
    hide: 1200,
    sortable: true,
  },
  {
    name: "Recipe Image",
    compact: true,
    hide: 1400,
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
    compact: true,
    hide: 1200,
    sortable: true,
  },
  {
    name: "Report Reason",
    hide: 1050,
    sortable: true,
    cell: ({ reportReason }) => {
      return (
        <span className='w-full overflow-hidden text-ellipsis text-nowrap text-sm'>
          {reportReason}
        </span>
      );
    }
  },
  {
    name: "Created Date",
    sortable: true,
    cell: ({ createdAt }) => {
      return (
        <span className='w-full overflow-hidden text-ellipsis text-nowrap text-sm'>
          {format(new Date(createdAt), "dd/MM/yyyy")}
        </span>
      );
    }
  },
  {
    name: "Status",
    sortable: true,
    selector: row => row.status,
    cell: ({ status }) => {
      return <StatusText status={status as any} />;
    }
  },
  {
    name: "Actions",
    center: true,
    cell: ({ recipeId, status }) => {
      return <ActionButtons recipeId={recipeId} isActive={true} />;
    }
  }
];

type ActionButtonsProps = {
  recipeId: string;
  isActive: boolean;
};

const ActionButtons = ({ recipeId, isActive }: ActionButtonsProps) => {
  return (
    <div className='flex gap-2'>
      <ViewDetailButton
        title='Detail'
        targetId={recipeId}
      />
      <RestoreButton
        title='Restore'
        targetId={recipeId}
      />
      <MarkAsCompletedButton
        title='Mark as completed'
        targetId={recipeId}
      />
      <RejectButton
        title='Reject'
        targetId={recipeId}
      />
    </div>
  );
};

export type Status = "Done" | "Pending";

export const StatusText = ({
  status,
  coloring
}: {
  status: Status;
  coloring?: boolean;
}) => {
  return (
    <div className='flex-center flex gap-2'>
      {status === "Done" ? (
        <>
          <div className='size-3 rounded-full bg-green-500' />
          <span className={`font-medium text-sm ${coloring && "text-green-500"}`}>
            Done
          </span>
        </>
      ) : (
        <>
          <div className='size-3 rounded-full bg-red-500' />
          <span className={`font-medium text-sm ${coloring && "text-red-500"}`}>
            Pending
          </span>
        </>
      )}
    </div>
  );
};

export const columnFieldMap: Record<string, keyof IAdminReportRecipeResponse> = {
  "Recipe Name": "recipeTitle",
  "Recipe Owner": "recipeOwnerUsername",
  "Recipe Image": "recipeImageURL",
  "Reporter": "reporterUsername",
  "Report Reason": "reportReason",
  "Created Date": "createdAt",
  "Status": "status"
};

export default function Table() {
  const [limit, setLimit] = useState(10);
  const [skip, setSkip] = useState(1);
  const [sortBy, setSortBy] = useState("createdAt");
  const [sortOrder, setSortOrder] = useState("DESC");
  const [lang, setLang] = useState("vi");
  const [keyword, setKeyword] = useState("");
  const debouncedValue = useDebounce(keyword, 800);

  const { data, isLoading } = useGetRecipeReports({
    skip,
    sortBy,
    sortOrder,
    limit,
    lang
  });

  const reports = useMemo(() => {
    return data?.paginatedData || [];
  }, [data]);

  const handleChangeRowPerPage = useCallback((numOfRows: number, currentPage: number) => {
    setLimit(numOfRows);
  }, []);

  const handleChangePage = useCallback((page: number) => {
    setSkip(page);
  }, []);

  const onSort = useCallback((selectedColumn: TableColumn<IAdminReportRecipeResponse>, sortDirection: SortOrder) => {
    const sortBy = columnFieldMap[selectedColumn.name as string]
    const sortOrder = sortDirection.toString().toUpperCase();

    setSortBy(sortBy);
    setSortOrder(sortOrder);
  }, [])

  console.log("total row", data?.metadata?.totalRow);

  return (
    <DataTable
      customStyles={customStyles}
      columns={columns}
      data={reports}
      responsive
      striped
      highlightOnHover
      progressPending={isLoading}
      progressComponent={<Loader />}
      pagination
      paginationServer
      onChangeRowsPerPage={handleChangeRowPerPage}
      onChangePage={handleChangePage}
      paginationTotalRows={data?.metadata?.totalRow}
      sortServer
      onSort={onSort}
    />
  );
}
