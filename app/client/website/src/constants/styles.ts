import { colors } from "./colors";

export const customStyles = {
  headCells: {
    style: {
      backgroundColor: colors.primary,
      color: colors.white.DEFAULT,
      fontWeight: "bold",
      paddingLeft: "30px"
    }
  },
  tableWrapper: {
    style: {
      borderRadius: "10px",
      paddingBottom: "0px"
    }
  },
  rows: {
    style: {
      backgroundColor: "white",
      color: "black",
      paddingLeft: "10px"
    },
    stripedStyle: {
      backgroundColor: "rgba(254, 114, 76, 0.15)"
    },
    highlightOnHoverStyle: {
      backgroundColor: "rgba(254, 114, 76, 0.3)"
    }
  },
  table: {
    style: {
      borderRadius: "10px"
    }
  },
  headRow: {
    style: {
      backgroundColor: "#FE724C !important"
    }
  },
  pagination: {
    style: {
      marginTop: "0px",
      backgroundColor: colors.white.DEFAULT
    }
  }
};
