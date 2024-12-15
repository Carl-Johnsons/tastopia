import { DataTable } from "react-native-paper";
import { ScrollView } from "react-native-gesture-handler";
import {
  ColorValue,
  StyleProp,
  StyleSheet,
  Text,
  TextStyle,
  View,
  ViewStyle
} from "react-native";
import { Fragment, isValidElement, ReactNode, useEffect, useState } from "react";

import { globalStyles } from "../GlobalStyles";
import PagoLoader from "../PagoLoader";
import PagoNoRecord from "../PagoNoRecord";

export type PagoDataTableSelector<T> = {
  // required
  selector: (row: T) => any;
  // variant
  title?: string | ReactNode;
  centered?: boolean;
  width?: number; // only take effect when 'scrollable' is true
  titleColor?: string;
  columnStyle?: StyleProp<ViewStyle>; // Use 'flex' property to expand the width when 'scrollable' not true. Default is flex: 1
};

type PagoDataTableType<T> = {
  // required
  data: T[];
  tableHeaders: PagoDataTableSelector<T>[];
  // events
  onChangePage?: (nextPage: number) => void;
  onRowClicked?: (obj: T) => void;
  // variants
  baseRowColor?: ColorValue;
  headless?: boolean;
  isLoading?: boolean;
  itemsPerPage?: number;
  loaderComponent?: ReactNode;
  multilineText?: boolean;
  noDataComponent?: ReactNode;
  scrollable?: boolean;
  striped?: boolean;
  stripeRowColor?: ColorValue;
  type?: "primary" | "secondary";
  // pagination variant
  isPagination?: boolean;
  paginationTotalRows?: number;
  // Custom styles
  tableCellsStyle?: StyleProp<ViewStyle>;
  tableCellTextStyle?: StyleProp<TextStyle>;
  tableHeadersStyle?: StyleProp<ViewStyle>;
  tableHeaderTextStyle?: StyleProp<TextStyle>;
  tableRowStyle?: StyleProp<ViewStyle>;
};

const PagoDataTable = <T,>({
  // required
  data,
  tableHeaders,
  // event
  onChangePage = (_currentPage: number) => {},
  onRowClicked = (_obj: T) => {},
  // variant
  baseRowColor = globalStyles.color.bgLight,
  headless = false,
  isLoading = false,
  itemsPerPage = 5,
  loaderComponent = <PagoLoader />,
  multilineText = false,
  noDataComponent = <PagoNoRecord />,
  scrollable = false,
  striped = false,
  stripeRowColor = globalStyles.color.primaryOpacity,
  type = "primary",
  // pagination variants
  isPagination = true,
  paginationTotalRows = data.length,
  // Custom style
  tableCellsStyle,
  tableCellTextStyle,
  tableHeadersStyle,
  tableHeaderTextStyle,
  tableRowStyle
}: PagoDataTableType<T>) => {
  const [page, setPage] = useState(0);

  // Derived variable
  const transformData = data.slice(0, itemsPerPage);

  const isEmpty = transformData.length === 0;

  const tableHeaderBGColor =
    type === "secondary" ? globalStyles.color.primary : undefined;

  stripeRowColor =
    type === "secondary" && stripeRowColor === globalStyles.color.primaryOpacity
      ? globalStyles.color.disableOpacity
      : stripeRowColor;

  const DataTableChildrenWrapper = scrollable ? ScrollView : Fragment;
  /* In react-native-paper issue state that, in order to support multiline text,
  View must be used instead of DataTable.Cell */
  const DataTableCellWrapper = multilineText ? View : DataTable.Cell;

  // Side effect
  useEffect(() => {
    setPage(0);
  }, [itemsPerPage]);

  const renderRow = (row: T, index: number) => {
    const isStriped = striped && (index + 1) % 2 === 0;
    return (
      <DataTable.Row
        key={index}
        style={[
          !multilineText && { height: 70 },
          { backgroundColor: isStriped ? stripeRowColor : baseRowColor },
          { width: "100%" },
          tableRowStyle
        ]}
        onPress={() => onRowClicked(row)}
      >
        {tableHeaders.map((tableHeader, index) => {
          const { selector, centered, width } = tableHeader;
          const value = selector(row);
          return (
            <DataTableCellWrapper
              key={index}
              numeric={!isNaN(value)}
              style={[
                centered && styles.flexCenter,
                styles.tableCell,
                { ...(scrollable && { width: width || 100 }) },
                tableHeader.columnStyle,
                tableCellsStyle
              ]}
            >
              {/* Value can be either ReactNode or Text */}
              {isValidElement(value) ? (
                value
              ) : (
                <Text
                  style={[
                    scrollable ? { maxWidth: width || 100 } : {},
                    tableCellTextStyle
                  ]}
                  numberOfLines={1}
                >
                  {value}
                </Text>
              )}
            </DataTableCellWrapper>
          );
        })}
      </DataTable.Row>
    );
  };

  return (
    <DataTable>
      <DataTableChildrenWrapper
        {...(scrollable && {
          horizontal: true,
          contentContainerStyle: { flexDirection: "column" }
        })}
      >
        <DataTable.Header
          style={[
            styles.tableHeaders,
            tableHeaderBGColor ? { backgroundColor: tableHeaderBGColor } : {}
          ]}
        >
          {!headless &&
            tableHeaders.map((tableHeader, index) => (
              <DataTable.Title
                key={index}
                style={[
                  tableHeader.centered && styles.flexCenter,
                  {
                    ...(scrollable && {
                      width: tableHeader.width || 100
                    })
                  },
                  tableHeader.columnStyle,
                  tableHeadersStyle
                ]}
              >
                {/* Title can be either ReactNode or Text */}
                {isValidElement(tableHeader.title) ? (
                  <> {tableHeader.title}</>
                ) : (
                  <Text
                    numberOfLines={1}
                    style={[
                      styles.tableHeaderText,
                      tableHeader.titleColor ? { color: tableHeader.titleColor } : {},
                      tableHeader.columnStyle,
                      tableHeaderTextStyle
                    ]}
                  >
                    {tableHeader.title}
                  </Text>
                )}
              </DataTable.Title>
            ))}
        </DataTable.Header>

        {isLoading ? (
          <View style={styles.loaderContainer}>{loaderComponent}</View>
        ) : (
          <View>{isEmpty ? noDataComponent : transformData.map(renderRow)}</View>
        )}
      </DataTableChildrenWrapper>

      {isPagination && (
        <View style={styles.flexCenter}>
          <DataTable.Pagination
            page={page}
            numberOfPages={Math.ceil(paginationTotalRows / itemsPerPage)}
            onPageChange={page => {
              setPage(page);
              // Notify event on page change
              onChangePage(page);
            }}
            numberOfItemsPerPage={itemsPerPage}
            showFastPaginationControls
            label={`Current page: ${page + 1}`}
          />
        </View>
      )}
    </DataTable>
  );
};

const styles = StyleSheet.create({
  tableHeaders: {
    paddingTop: 5,
    paddingBottom: 5,
    borderColor: "#D9D9D9"
  },

  tableHeaderText: {
    fontSize: 16,
    fontWeight: "600"
  },

  tableCell: {
    borderStyle: "solid",
    borderBottomWidth: 1,
    borderColor: "#D9D9D9",

    paddingTop: 10,
    paddingBottom: 10,
    paddingRight: 10
  },

  flexCenter: {
    display: "flex",
    justifyContent: "center",
    alignItems: "center"
  },

  loaderContainer: {
    height: 100
  }
});

export default PagoDataTable;
