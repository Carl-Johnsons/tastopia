import { StyleSheet } from "react-native";

export default StyleSheet.create({
  radioContainer: {
    display: "flex",
    flexDirection: "row",
    alignContent: "center"
  },

  radioToggle: {
    width: 20,
    height: 20,
    backgroundColor: "#fff",
    borderRadius: 50
  },

  radioToggleInActive: {
    borderColor: "#D2CDCD",
    borderWidth: 1
  },

  radioToggleActive: {
    borderColor: "#79B668",
    borderWidth: 6.5
  }
});
