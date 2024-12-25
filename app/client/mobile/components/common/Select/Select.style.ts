import { globalStyles } from "../GlobalStyles";
import { StyleSheet } from "react-native";

export default StyleSheet.create({
  label: {
    fontSize: 12,
    marginBottom: 8,
    fontWeight: 600,
    color: globalStyles.color.secondary
  },
  container: {
    backgroundColor: "white",
    padding: 16,
    marginBottom: 12
  },
  dropdown: {
    borderColor: "#D9D9D9",
    borderWidth: 1,
    borderRadius: 4,
    paddingVertical: 12,
    paddingLeft: 12,
    paddingRight: 40,
    fontSize: 12,
    fontWeight: 700,
    color: "#939393"
  },
  dropdownWithImage: {
    paddingVertical: 6
  },
  dropdownFocused: {
    borderColor: "#3b7079",
    shadowColor: "0 0 0 4" + globalStyles.color.primaryMoreOpacity
  },
  placeholderStyle: {
    fontSize: 12,
    lineHeight: 16,
    fontWeight: 700,
    color: "#939393"
  },
  selectedTextStyle: {
    fontSize: 12,
    color: "#515252",
    fontWeight: 700
  },
  iconDropDown: {
    right: "-160%",
    color: "#2E2E2E"
  },
  separate: {
    position: "absolute",
    borderLeftWidth: 1.5,
    borderColor: "#D9D9D9",
    height: 25,
    top: "50%",
    transform: [{ translateY: -13 }],
    right: "1%"
  },
  itemTextStyle: {
    fontSize: 12,
    color: "#515252",
    lineHeight: 16,
    fontWeight: "700"
  },
  inputSearchWrapper: {
    borderColor: "red"
  },
  inputSearch: {
    fontSize: 12,
    color: "#515252",
    lineHeight: 16,
    paddingVertical: 5,
    paddingHorizontal: 12,
    marginHorizontal: 8,
    marginVertical: 6,
    borderWidth: 1.5,
    borderColor: "#D9D9D9"
  },
  errorContainer: {
    marginTop: 0,
    marginBottom: 8
  },
  imageStyle: {
    width: 20,
    height: 20,
    marginRight: 10
  },
  item: {
    padding: 10
  },
  selectedItemWrapper: {
    backgroundColor: globalStyles.color.success
  },
  selectedItemText: {
    color: "#fff"
  },
  disableSelect: {
    backgroundColor: "#e9ecef"
  }
});
