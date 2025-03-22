"use client";

import Loader from "@/components/ui/Loader";
import { IAdminRecipeResponse } from "@/generated/interfaces/recipe.interface";
import useDebounce from "@/hooks/useDebounce";
import { format } from "date-fns";
import {
  ChangeEvent,
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState
} from "react";
import DataTable, { SortOrder, TableColumn } from "react-data-table-component";
import { DisableRecipeButton, RestoreRecipeButton, ViewDetailButton } from "./Button";
import NoRecord from "@/components/ui/NoRecord";
import DataTableProvider, {
  DataTableContext,
  DataTableContextValue,
  OnChangeActiveFn
} from "./Provider";
import { ItemStatusText } from "../report/common/StatusText";
import { useGetRecipes } from "@/api/recipe";
import SearchBar from "../users/SearchBar";
import Image from "@/components/shared/common/Image";
import useDataTableStyles from "@/hooks/table/useDataTableStyle";

const columns: TableColumn<IAdminRecipeResponse>[] = [
  {
    name: "Recipe Name",
    selector: row => row.title,
    sortable: true,
    maxWidth: "200px"
  },
  {
    name: "Ingredients",
    hide: 1368,
    sortable: true,
    wrap: true,
    grow: 3,
    cell: ({ ingredients }) => <span className='py-1'>{ingredients}</span>
  },
  {
    name: "Username",
    selector: row => row.authorUsername,
    hide: 1576,
    sortable: true
  },
  {
    name: "Name",
    selector: row => row.authorDisplayName,
    hide: 1576,
    sortable: true
  },
  {
    name: "Created Date",
    sortable: true,
    width: "140px",
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
    center: true,
    cell: ({ isActive }) => {
      return (
        <ItemStatusText
          isActive={isActive}
          coloring
        />
      );
    }
  },
  {
    name: "Recipe Image",
    hide: 1368,
    width: "140px",
    center: true,
    cell: ({ recipeImageUrl }) => (
      <div className='p-2'>
        <Image
          src={recipeImageUrl}
          alt={"Recipe image figure"}
          width={90}
          height={50}
          className='h-[50px] w-[90px] rounded-lg object-cover'
        />
      </div>
    )
  },
  {
    name: "Actions",
    center: true,
    width: "150px",
    cell: ({ id, isActive }) => {
      return (
        <ActionButtons
          recipeId={id}
          isActive={isActive}
        />
      );
    }
  }
];

type ActionButtonsProps = {
  recipeId: string;
  isActive: boolean;
};

const ActionButtons = ({ recipeId, isActive }: ActionButtonsProps) => {
  const { onChangeActive } = useContext(DataTableContext) as DataTableContextValue;

  return (
    <div className='flex gap-2'>
      <ViewDetailButton
        title='View detail'
        targetId={recipeId}
      />
      {!isActive ? (
        <RestoreRecipeButton
          title='Restore'
          targetId={recipeId}
          noText
          toolTip
          onSuccess={() => {
            onChangeActive({ recipeId, value: true });
          }}
        />
      ) : (
        <DisableRecipeButton
          title='Disable'
          targetId={recipeId}
          noText
          toolTip
          onSuccess={() => {
            onChangeActive({ recipeId, value: false });
          }}
        />
      )}
    </div>
  );
};

export const columnFieldMap: Record<string, keyof IAdminRecipeResponse> = {
  "Recipe Name": "title",
  Ingredients: "ingredients",
  Username: "authorUsername",
  Name: "authorDisplayName",
  "Created Date": "createdAt",
  Status: "isActive",
  "Recipe Image": "recipeImageUrl"
};

export default function Table() {
  const [limit, setLimit] = useState(10);
  const [skip, setSkip] = useState(0);
  const [sortBy, setSortBy] = useState("createdAt");
  const [sortOrder, setSortOrder] = useState("DESC");
  const [lang, setLang] = useState("en");
  const [keyword, setKeyword] = useState("");
  const debouncedValue = useDebounce(keyword, 800);

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
    []
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
        <div className='flex gap-2 self-start'>
          <span className='text-gray-500'>Administer Recipes</span>
        </div>
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
        />
      </DataTableProvider>
    </>
  );
}
