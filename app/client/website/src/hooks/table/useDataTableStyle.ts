"use client";

import { TableStyles } from "react-data-table-component";
import useColorizer from "../theme/useColorizer";
import { useMemo } from "react";
import { colors } from "@/constants/colors";

type UseDataTableStylesResult = {
  /** A customised data table style. */
  tableStyles: TableStyles;
};

/**
 * Return the custom styles for the data table.
 *
 * NOTE: Can only be used in a client component since it have to check
 * the current color scheme in the local storage.
 */
export default function useDataTableStyles(): UseDataTableStylesResult {
  const { c } = useColorizer();
  const { primary, white, black } = colors;

  const tableStyles: TableStyles = useMemo(
    () => ({
      headCells: {
        style: {
          backgroundColor: primary,
          color: white.DEFAULT,
          // fontWeight: "bold",
          fontSize: "14px"
          // paddingLeft: "24px"
        }
      },
      rows: {
        style: {
          backgroundColor: "white",
          color: black.DEFAULT
          // paddingLeft: "10px"
        },
        stripedStyle: {
          backgroundColor: "rgba(247, 195, 193, 0.15)"
        },
        highlightOnHoverStyle: {
          backgroundColor: "rgba(254, 114, 76, 0.3)"
        }
      },
      headRow: {
        style: {
          fontSize: "14px !important",
          backgroundColor: "#FE724C !important"
        }
      },
      progress: {
        style: {
          backgroundColor: "transparent"
        }
      },
      noData: {
        style: {
          backgroundColor: c(white.DEFAULT, black[200])
        }
      }
    }),
    [c, black, primary, white]
  );

  return { tableStyles };
}
