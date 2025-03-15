"use client"

import Loader from '@/components/ui/Loader';
import NoRecord from '@/components/ui/NoRecord';
import { columnFieldMap, usersColumns } from "./DataTableColumns"
import ReactDataTable, { SortOrder, TableColumn } from 'react-data-table-component';
import { IAdminGetUserResponse } from '@/generated/interfaces/user.interface';
import { ChangeEvent, useEffect, useState } from 'react';
import { useGetAdminUsers } from '@/api/user';
import { colors } from '@/constants/colors';
import { Loader2, Search } from 'lucide-react';
import useDebounce from '@/hooks/useDebounce';

const customStyles = {
  headCells: {
    style: {
      backgroundColor: colors.primary,
      color: "white",
      fontWeight: "bold",
      paddingLeft: "30px"
    },
  },
  rows: {
    style: {
      backgroundColor: "white",
      color: "black",
      paddingLeft: "10px"
    },
  },
  headRow: {
    style: {
      backgroundColor: "#FE724C !important",
    },
  },
};

const DataTable = () => {
  const [skip, setSkip] = useState(0);
  const [limit, setLimit] = useState(10);
  const [sortBy, setSortBy] = useState("");
  const [keyword, setKeyword] = useState("");
  const [sortOrder, setSortOrder] = useState("asc")
  const debouncedValue = useDebounce(keyword, 800);

  const { data, isLoading, refetch } =
  useGetAdminUsers(skip, sortBy, sortOrder, debouncedValue, limit);

  const handlePageChange = (page: number) => {
    setSkip(page - 1);
  }

  const handlePerRowsChange = (newPerPage: number) => {
    setLimit(newPerPage)
  }

  const handleSort = (selectedColumn: TableColumn<IAdminGetUserResponse>, sortDirection: SortOrder) => {
    console.log('handleSort', selectedColumn.name)
    const apiField = columnFieldMap[selectedColumn.name as string];

    if (!apiField) return;
    setSortBy(apiField)
    setSortOrder(sortDirection)
  }

  const handleSearch = (e: ChangeEvent<HTMLInputElement>) => {
    setKeyword(e.target.value);
    setSkip(0);
  };

  useEffect(() => {
    refetch();
  }, [skip, sortBy, sortOrder, debouncedValue, limit]);
  
  console.log("skip", skip)
  
  return (
     <div className='flex-center mt-4 flex-col gap-4'>
        <div className="relative flex w-full max-w-lg items-center gap-2 rounded-2xl bg-primary/15 px-4 py-3">
          <Search className="text-primary" size={20} />
          <input
            type="text"
            placeholder="Search by username, name, gmail,..."
            className="w-full bg-transparent text-primary outline-none placeholder:text-primary/50"
            onChange={handleSearch}
          />
          {debouncedValue && isLoading && <div className='absolute right-3 top-3.5'>
            <Loader2 className="size-5 animate-spin text-primary" />
          </div>}
      </div>

      <p className="text-black_white base-medium flex w-full flex-col gap-4">Administer Users</p>

        <div className='dark:bg-black'>
          <ReactDataTable
            columns={usersColumns}
            data={data?.paginatedData.slice(0, limit) || []}
            customStyles={customStyles}
            striped
            highlightOnHover
            progressPending={isLoading}
            pagination
            paginationServer
            paginationTotalRows={data?.metadata.totalRow}
            onChangePage={handlePageChange}
            onChangeRowsPerPage={handlePerRowsChange}
            onSort={handleSort}
            progressComponent={<Loader />}
            noDataComponent={<NoRecord />}
          />
        </div>
     </div>
  );
}

export default DataTable