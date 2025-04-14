"use client";

import Loader from "@/components/ui/Loader";
import { IAdminRecipeResponse } from "@/generated/interfaces/recipe.interface";
import useDebounce from "@/hooks/useDebounce";
import { ChangeEvent, useCallback, useEffect, useMemo, useState } from "react";
import DataTable, { SortOrder, TableColumn } from "react-data-table-component";
import NoRecord from "@/components/ui/NoRecord";
import DataTableProvider, { OnChangeActiveFn } from "./Provider";
import { useGetRecipes } from "@/api/recipe";
import SearchBar from "../users/SearchBar";
import useDataTableStyles from "@/hooks/table/useDataTableStyle";
import useLocaleTable from "@/hooks/table/useLocaleTable";
import { useLocale, useTranslations } from "next-intl";
import useRecipeTableColumns from "@/hooks/table/useRecipeTableColumns";

export default function Table() {
  const [limit, setLimit] = useState(10);
  const [skip, setSkip] = useState(0);
  const [sortBy, setSortBy] = useState("createdAt");
  const [sortOrder, setSortOrder] = useState("DESC");
  const lang = useLocale();
  const [keyword, setKeyword] = useState("");
  const debouncedValue = useDebounce(keyword, 800);
  const t = useTranslations("administerRecipes");

  const { columns, columnFieldMap } = useRecipeTableColumns();

  const {
    data: fetchedData,
    isLoading,
    isFetching,
    refetch
  } = useGetRecipes({
    skip,
    sortBy,
    sortOrder,
    limit,
    lang,
    keyword: debouncedValue
  });

  const { tableStyles } = useDataTableStyles();
  const tableLocale = useLocaleTable();

  const totalRow = useMemo(
    () => (fetchedData?.metadata?.totalRow ? fetchedData.metadata.totalRow : 0),
    [fetchedData]
  );
  const [data, setData] = useState<IAdminRecipeResponse[]>([]);

  const handleChangeRowPerPage = useCallback((numOfRows: number) => {
    setLimit(numOfRows);
  }, []);

  const handleChangePage = useCallback((page: number) => {
    setSkip(page - 1);
  }, []);

  const onSort = useCallback(
    (selectedColumn: TableColumn<IAdminRecipeResponse>, sortDirection: SortOrder) => {
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

  const onChangeActive = useCallback<OnChangeActiveFn>(({ recipeId, value }) => {
    setData(prev => {
      return prev.map(item => {
        if (item.id !== recipeId) return item;

        return {
          ...item,
          isActive: value
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
    setData(fetchedData?.paginatedData || []);
  }, [fetchedData]);

  return (
    <>
      <div className='flex-center mt-4 flex-col gap-4'>
        <SearchBar
          onChange={handleSearch}
          isLoading={isLoading || isFetching}
        />
        <p className='text-black_white base-medium flex w-full flex-col gap-4'>
          {t("title")}
        </p>
      </div>

      <DataTableProvider value={contextValue}>
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
