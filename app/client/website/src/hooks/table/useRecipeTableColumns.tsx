import { ItemStatusText } from "@/components/screen/report/common/StatusText";
import {
  ViewDetailButton,
  DisableRecipeButton,
  RestoreRecipeButton
} from "@/components/screen/recipe/Button";
import {
  DataTableContext,
  DataTableContextValue
} from "@/components/screen/recipe/Provider";
import Image from "@/components/shared/common/Image";
import { IAdminRecipeResponse } from "@/generated/interfaces/recipe.interface";
import { format } from "date-fns";
import { useTranslations } from "next-intl";
import { useContext, useMemo } from "react";
import { TableColumn } from "react-data-table-component";

export default function useRecipeTableColumns() {
  const t = useTranslations("administerRecipes.columns");

  const columns: TableColumn<IAdminRecipeResponse>[] = [
    {
      name: t("recipeName"),
      selector: row => row.title,
      sortable: true,
      width: "200px"
    },
    {
      name: t("ingredients"),
      hide: 1368,
      sortable: true,
      wrap: true,
      grow: 3,
      cell: ({ ingredients }) => <span className='py-2'>{ingredients}</span>
    },
    {
      name: t("username"),
      selector: row => row.authorUsername,
      width: "160px",
      hide: 1576,
      sortable: true
    },
    {
      name: t("name"),
      selector: row => row.authorDisplayName,
      hide: 1576,
      sortable: true
    },
    {
      name: t("createdDate"),
      sortable: true,
      width: "160px",
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
      name: t("status"),
      sortable: true,
      hide: 520,
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
      name: t("recipeImage"),
      hide: 1368,
      width: "160px",
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
      name: t("actions"),
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

  const columnFieldMap: Record<string, keyof IAdminRecipeResponse> = useMemo(
    () => ({
      [t("recipeName")]: "title",
      [t("ingredients")]: "ingredients",
      [t("username")]: "authorUsername",
      [t("name")]: "authorDisplayName",
      [t("createdDate")]: "createdAt",
      [t("status")]: "isActive",
      [t("recipeImage")]: "recipeImageUrl"
    }),
    [t]
  );

  return { columns, columnFieldMap };
}

type ActionButtonsProps = {
  recipeId: string;
  isActive: boolean;
};

const ActionButtons = ({ recipeId, isActive }: ActionButtonsProps) => {
  const { onChangeActive } = useContext(DataTableContext) as DataTableContextValue;
  const t = useTranslations("administerRecipes.actions");

  return (
    <div className='flex gap-2'>
      <ViewDetailButton
        title={t("viewDetail")}
        targetId={recipeId}
      />
      {!isActive ? (
        <RestoreRecipeButton
          title={t("restore")}
          targetId={recipeId}
          noText
          toolTip
          onSuccess={() => {
            onChangeActive({ recipeId, value: true });
          }}
        />
      ) : (
        <DisableRecipeButton
          title={t("disable")}
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
