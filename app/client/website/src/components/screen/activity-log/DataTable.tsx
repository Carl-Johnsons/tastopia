"use client";

import Loader from "@/components/ui/Loader";
import useDebounce from "@/hooks/useDebounce";
import { ChangeEvent, useCallback, useEffect, useMemo, useState } from "react";
import DataTable, { SortOrder, TableColumn } from "react-data-table-component";
import NoRecord from "@/components/ui/NoRecord";
import DataTableProvider, { OnChangeActiveFn } from "./Provider";
import SearchBar from "../users/SearchBar";
import useDataTableStyles from "@/hooks/table/useDataTableStyle";
import { useLocale, useTranslations } from "next-intl";
import { useAppDispatch } from "@/store/hooks";
import { createAdmin } from "@/slices/admin.slice";
import useLocaleTable from "@/hooks/table/useLocaleTable";
import useActivityLogTableColumns from "@/hooks/table/useActivityLogTableColumns";
import { IAdminActivityLogResponse } from "@/generated/interfaces/tracking.interface";
import { useGetActivityLogs } from "@/api/tracking";

export default function Table() {
  const [limit, setLimit] = useState(10);
  const [skip, setSkip] = useState(0);
  const [sortBy, setSortBy] = useState("createdAt");
  const [sortOrder, setSortOrder] = useState("DESC");
  const lang = useLocale();
  const [keyword, setKeyword] = useState("");
  const debouncedValue = useDebounce(keyword, 800);
  const { columns, columnFieldMap } = useActivityLogTableColumns();
  const dispatch = useAppDispatch();
  const tableLocale = useLocaleTable();

  const {
    data: fetchedData,
    isLoading,
    isFetching,
    refetch
  } = useGetActivityLogs({
    skip,
    sortBy,
    sortOrder,
    limit,
    lang,
    keyword: debouncedValue
  });

  const { tableStyles } = useDataTableStyles();
  const t = useTranslations("activityLog");

  const totalRow = useMemo(
    () => (fetchedData?.metadata?.totalRow ? fetchedData.metadata.totalRow : 0),
    [fetchedData]
  );
  const [data, setData] = useState<IAdminActivityLogResponse[]>([]);
  console.log("fetchedData", fetchedData);

  const handleChangeRowPerPage = useCallback((numOfRows: number) => {
    setLimit(numOfRows);
  }, []);

  const handleChangePage = useCallback((page: number) => {
    setSkip(page - 1);
  }, []);

  const onSort = useCallback(
    (selectedColumn: TableColumn<IAdminActivityLogResponse>, sortDirection: SortOrder) => {
      if (!selectedColumn.name) return;

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

  const onChangeActive = useCallback<OnChangeActiveFn>(({ id, value }) => {
    setData(prev => {
      return prev.map(item => {
        if (item.accountId !== id) return item;

        return {
          ...item,
          isActive: value
        };
      });
    });
  }, []);

  const openCreateAdminDialog = useCallback(() => {
    dispatch(createAdmin());
  }, [dispatch]);

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
    setData(fetchedData?.paginatedData || []);
  }, [fetchedData]);

  return (
    <DataTableProvider value={contextValue}>
      <div className='flex-center mt-4 flex-col gap-4'>
        <SearchBar
          onChange={handleSearch}
          isLoading={isLoading || isFetching}
          placeholder={t("search")}
        />
        <div className='flex gap-2 self-start'>
          <span className='text-black_white base-medium flex w-full flex-col gap-4'>
            {t("title")}
          </span>
        </div>
      </div>

      <DataTable
        customStyles={tableStyles}
        columns={columns}
        data={data}
        responsive
        striped
        highlightOnHover
        progressPending={isLoading || isFetching}
        progressComponent={<Loader />}
        noDataComponent={<NoRecord />}
        pagination
        paginationServer
        paginationComponentOptions={tableLocale}
        onChangeRowsPerPage={handleChangeRowPerPage}
        onChangePage={handleChangePage}
        paginationTotalRows={totalRow}
        sortServer
        onSort={onSort}
      />
    </DataTableProvider>
  );
}
