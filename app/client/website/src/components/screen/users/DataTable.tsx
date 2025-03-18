"use client"

import Loader from '@/components/ui/Loader';
import NoRecord from '@/components/ui/NoRecord';
import { ActionButtons, columnFieldMap, usersColumns } from "./DataTableColumns"
import ReactDataTable, { SortOrder, TableColumn } from 'react-data-table-component';
import { IAdminGetUserResponse } from '@/generated/interfaces/user.interface';
import { ChangeEvent, useEffect, useState } from 'react';
import { useGetAdminUsers } from '@/api/user';
import useDebounce from '@/hooks/useDebounce';
import { customStyles } from '@/constants/styles';
import SearchBar from './SearchBar';

const DataTable = () => {
  const [skip, setSkip] = useState(0);
  const [limit, setLimit] = useState(10);
  const [sortBy, setSortBy] = useState("");
  const [keyword, setKeyword] = useState("");
  const [sortOrder, setSortOrder] = useState("asc")
  const debouncedValue = useDebounce(keyword, 300);
  const [updatedUserStatuses, setUpdatedUserStatuses] = useState<Record<string, boolean>>({});

  const { data, isLoading, refetch } =
  useGetAdminUsers(skip, sortBy, sortOrder, debouncedValue, limit);
  const tableData = data?.paginatedData.slice(0, limit).map(user => ({
    ...user,
    isAccountActive: updatedUserStatuses[user.accountId] !== undefined 
      ? updatedUserStatuses[user.accountId] 
      : user.isAccountActive
  })) || [];

  const handleStatusUpdate = (accountId: string, isActive: boolean) => {
    setUpdatedUserStatuses(prev => ({
      ...prev,
      [accountId]: isActive
    }));
  };

  const handlePageChange = (page: number) => {
    setSkip(page - 1);
  }

  const handlePerRowsChange = (newPerPage: number) => {
    setLimit(newPerPage);
  }

  const handleSort = (selectedColumn: TableColumn<IAdminGetUserResponse>, sortDirection: SortOrder) => {
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
  
  return (
     <>
       <div className='flex-center mt-4 flex-col gap-4'>
          <SearchBar onChange={handleSearch} isLoading={isLoading} />
          <p className="text-black_white base-medium flex w-full flex-col gap-4">Administer Users</p>
        </div>
          <ReactDataTable
              columns={usersColumns.map(column => {
                    if (column.name === "Action") {
                      return {
                        ...column,
                        cell: (user: IAdminGetUserResponse) => (
                          <ActionButtons 
                            accountId={user.accountId} 
                            isActive={user.isAccountActive} 
                            onStatusUpdate={handleStatusUpdate}
                          />
                        )
                      };
                    }
                    return column;
                  })}
              data={tableData}
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
     </>
    
  );
}

export default DataTable
