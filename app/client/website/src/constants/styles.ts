import { TableStyles } from "react-data-table-component";
import { colors } from "./colors";

export const customStyles: TableStyles = {
  headCells: {
    style: {
      backgroundColor: colors.primary,
      color: colors.white.DEFAULT,
      fontWeight: "bold",
      paddingLeft: "30px"
    }
  },
  rows: {
    style: {
      backgroundColor: "white",
      color: "black",
      paddingLeft: "10px"
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
      backgroundColor: "#FE724C !important"
    }
  }
};
