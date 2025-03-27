"use client";

import { useGetRecipeReports } from "@/api/recipe";
import Loader from "@/components/ui/Loader";
import { IAdminReportRecipeResponse } from "@/generated/interfaces/recipe.interface";
import useDebounce from "@/hooks/useDebounce";
import { ChangeEvent, useCallback, useEffect, useMemo, useState } from "react";
import DataTable, { SortOrder, TableColumn } from "react-data-table-component";
import { ChevronRight } from "lucide-react";
import NoRecord from "@/components/ui/NoRecord";
import SearchBar from "../../users/SearchBar";
import { ReportStatus } from "@/constants/reports";
import DataTableProvider, { OnChangeActiveFn } from "./Provider";
import useDataTableStyles from "@/hooks/table/useDataTableStyle";
import useLocaleTable from "@/hooks/table/useLocaleTable";
import { useLocale, useTranslations } from "next-intl";
import useReportRecipeTableColumns from "@/hooks/table/useReportRecipeTableColumns";

export default function Table() {
  const [limit, setLimit] = useState(10);
  const [skip, setSkip] = useState(0);
  const [sortBy, setSortBy] = useState("createdAt");
  const [sortOrder, setSortOrder] = useState("DESC");
  const lang = useLocale();
  const [keyword, setKeyword] = useState("");
  const debouncedValue = useDebounce(keyword, 800);
  const t = useTranslations("administerReportRecipes");
  const { columns, columnFieldMap } = useReportRecipeTableColumns();

  const {
    data: fetchedData,
    isLoading,
    isFetching,
    refetch
  } = useGetRecipeReports({
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
  const [reports, setReports] = useState<IAdminReportRecipeResponse[]>([]);
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
      selectedColumn: TableColumn<IAdminReportRecipeResponse>,
      sortDirection: SortOrder
    ) => {
      const sortBy = columnFieldMap[selectedColumn.name as string];
      const sortOrder = sortDirection.toString().toUpperCase();

      setSortBy(sortBy);
      setSortOrder(sortOrder);
    },
    [columnFieldMap]
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

  const contextValue = useMemo(
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
          placeholder={t("search")}
        />
        <div className='flex gap-2 self-start'>
          <span className='text-gray-500'>{t("title")}</span>
          <ChevronRight className='text-black_white' />
          <span className='text-black_white'>{t("subtitle")}</span>
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
          pagination
          paginationServer
          onChangeRowsPerPage={handleChangeRowPerPage}
          onChangePage={handleChangePage}
          paginationTotalRows={totalRow}
          sortServer
          onSort={onSort}
          paginationComponentOptions={tableLocale}
        />
      </DataTableProvider>
    </>
  );
}
