"use client";

import Loader from "@/components/ui/Loader";
import NoRecord from "@/components/ui/NoRecord";
import { Button } from "@/components/ui/button";
import { columnFieldMap, tagsColumns } from "./DataTableColumns";
import ReactDataTable, { SortOrder, TableColumn } from "react-data-table-component";
import { ChangeEvent, useEffect, useState } from "react";
import useDebounce from "@/hooks/useDebounce";
import SearchBar from "../users/SearchBar";
import { useTags } from "./TagsContext";
import { useGetTags } from "@/api/tag";
import { Tag } from "@/types/tag";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger
} from "@/components/ui/dialog";
import { Plus } from "lucide-react";
import TagForm from "./Form";
import { FORM_TYPE } from "@/constants/form";
import useDataTableStyles from "@/hooks/table/useDataTableStyle";

const DataTable = () => {
  const [skip, setSkip] = useState(0);
  const [limit, setLimit] = useState(10);
  const [sortBy, setSortBy] = useState("");
  const [keyword, setKeyword] = useState("");
  const [sortOrder, setSortOrder] = useState("asc");
  const debouncedValue = useDebounce(keyword, 300);

  const { data, isLoading, refetch } = useGetTags(
    skip,
    sortBy,
    sortOrder,
    debouncedValue,
    limit
  );
  const {
    tags,
    setTags,
    openCreateDialog,
    setOpenCreateDialog,
    openUpdateDialog,
    setOpenUpdateDialog
  } = useTags();

  const { tableStyles } = useDataTableStyles();

  const handlePageChange = (page: number) => {
    setSkip(page - 1);
  };

  const handlePerRowsChange = (newPerPage: number) => {
    setLimit(newPerPage);
  };

  const handleSort = (selectedColumn: TableColumn<Tag>, sortDirection: SortOrder) => {
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
    if (data?.paginatedData) {
      setTags(data.paginatedData);
    }
  }, [data, setTags]);

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
        <p className='text-black_white base-medium flex w-full flex-col gap-4'>
          Administer Tags
        </p>

        <div className='flex-start w-full'>
          <Dialog
            open={openCreateDialog}
            onOpenChange={setOpenCreateDialog}
          >
            <DialogTrigger asChild>
              <Button className='text-white_black bg-primary hover:bg-secondary'>
                <Plus />
                <p className='mt-1 max-sm:hidden'>Create</p>
              </Button>
            </DialogTrigger>
            <DialogContent
              className='bg-white_black200 sm:max-w-[525px]'
              onPointerDownOutside={e => e.preventDefault()}
            >
              <DialogHeader>
                <DialogTitle className='text-black_white'>Create tag</DialogTitle>
              </DialogHeader>

              <div>
                <TagForm type={FORM_TYPE.CREATE} />
              </div>
            </DialogContent>
          </Dialog>

          <Dialog
            open={openUpdateDialog}
            onOpenChange={setOpenUpdateDialog}
          >
            <DialogContent
              className='bg-white_black200 sm:max-w-[525px]'
              onPointerDownOutside={e => e.preventDefault()}
            >
              <DialogHeader>
                <DialogTitle className='text-black_white'>Update tag</DialogTitle>
              </DialogHeader>

              <div>
                <TagForm type={FORM_TYPE.UPDATE} />
              </div>
            </DialogContent>
          </Dialog>
        </div>
      </div>

      <ReactDataTable
        data={tags}
        columns={tagsColumns}
        customStyles={tableStyles}
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
