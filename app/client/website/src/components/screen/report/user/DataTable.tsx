"use client";

import Loader from "@/components/ui/Loader";
import NoRecord from "@/components/ui/NoRecord";
import { ActionButtons, columnFieldMap, reportColumns } from "./DataTableColumns";
import ReactDataTable, { SortOrder, TableColumn } from "react-data-table-component";
import { IAdminUserReportResponse } from "@/generated/interfaces/user.interface";
import { ChangeEvent, useEffect, useState } from "react";
import { useGetUserReports } from "@/api/user";
import useDebounce from "@/hooks/useDebounce";
import { customStyles } from "@/constants/styles";
import SearchBar from "../../users/SearchBar";
import { ReportStatus } from "@/constants/reports";
import { ChevronRight } from "lucide-react";

const DataTable = () => {
  const [skip, setSkip] = useState(0);
  const [limit, setLimit] = useState(10);
  const [sortBy, setSortBy] = useState("");
  const [keyword, setKeyword] = useState("");
  const [sortOrder, setSortOrder] = useState("asc");
  const debouncedValue = useDebounce(keyword, 300);
  const [updatedReportStatuses, setUpdatedReportStatuses] = useState<
    Record<string, string>
  >({});

  const { data, isLoading, refetch } = useGetUserReports(
    skip,
    sortBy,
    sortOrder,
    debouncedValue,
    limit
  );

  const tableData =
    data?.paginatedData.slice(0, limit).map(report => ({
      ...report,
      status:
        updatedReportStatuses[report.reportId] !== undefined
          ? updatedReportStatuses[report.reportId]
          : report.status
    })) || [];

  const handleStatusUpdate = (reportId: string, status: string) => {
    setUpdatedReportStatuses(prev => ({
      ...prev,
      [reportId]: status
    }));
  };

  const handlePageChange = (page: number) => {
    setSkip(page - 1);
  };

  const handlePerRowsChange = (newPerPage: number) => {
    setLimit(newPerPage);
  };

  const handleSort = (
    selectedColumn: TableColumn<IAdminUserReportResponse>,
    sortDirection: SortOrder
  ) => {
    const apiField = columnFieldMap[selectedColumn.name as string];

    if (!apiField) return;
    setSortBy(apiField);
    setSortOrder(sortDirection);
  };

  const handleSearch = (e: ChangeEvent<HTMLInputElement>) => {
    setKeyword(e.target.value);
    setSkip(0);
  };

  useEffect(() => {
    refetch();
  }, [skip, sortBy, sortOrder, debouncedValue, limit]);

  return (
    <>
      <div className='flex-center mt-4 flex-col gap-4'>
        <SearchBar
          onChange={handleSearch}
          isLoading={isLoading}
        />
        <div className='flex gap-2 self-start'>
          <span className='text-gray-500'>Administer Reports</span>
          <ChevronRight className='text-black_white' />
          <span className='text-black_white'>User</span>
        </div>
      </div>
      <ReactDataTable
        columns={reportColumns.map(column => {
          if (column.name === "Action") {
            return {
              ...column,
              cell: (report: IAdminUserReportResponse) => (
                <ActionButtons
                  key={report.reportId}
                  reportId={report.reportId}
                  reportedId={report.reportedId}
                  status={report.status as ReportStatus}
                  reportedIsActive={report.reportedIsActive}
                  onStatusUpdate={handleStatusUpdate}
                />
              )
            };
          }
          return column;
        })}
        data={tableData.slice(0, limit)}
        customStyles={customStyles}
        striped
        highlightOnHover
        progressPending={isLoading}
        pagination
        paginationServer
        paginationTotalRows={data?.metadata?.totalRow}
        onChangePage={handlePageChange}
        onChangeRowsPerPage={handlePerRowsChange}
        onSort={handleSort}
        progressComponent={<Loader />}
        noDataComponent={<NoRecord />}
      />
    </>
  );
};

export default DataTable;
