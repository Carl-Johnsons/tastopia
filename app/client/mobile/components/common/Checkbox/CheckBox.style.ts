import { StyleSheet } from "react-native";

export default StyleSheet.create({
  checkBoxWrapper: {
    flexDirection: "column"
  },
  checkBoxContainer: {
    backgroundColor: "transparent",
    padding: 0,
    marginBottom: 0
  },
  container: {
    flexDirection: "row",
    alignItems: "center"
  },
  wrapperCheckBoxLabel: {
    flexDirection: "row",
    alignItems: "center",
    marginBottom: 8
  },
  label: {
    marginLeft: 6,
    color: "#387562",
    fontWeight: 700,
    fontSize: 12
  },
  errorContainer: {
    marginTop: -13,
    marginBottom: 12
  }
});
