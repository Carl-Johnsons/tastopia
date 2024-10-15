import { StyleSheet } from "react-native";
import { globalStyles } from "../GlobalStyles";

const styles = StyleSheet.create({
  container: {
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center"
  },

  containerActive: {
    borderRadius: 5,
    borderWidth: 3,
    borderColor: globalStyles.color.primary,
    margin: 10
  },

  previewImage: {
    flexDirection: "row",
    flexWrap: "wrap",
    alignItems: "center",
    justifyContent: "center",
    gap: 6,
    padding: 6
  },

  uploadItem: {
    position: "relative",
    width: "31%",
    borderWidth: 2,
    borderRadius: 5,
    borderColor: globalStyles.color.light
  },

  uploadItemImage: {
    width: "100%",
    height: 80
  },

  removeItemButton: {
    position: "absolute",
    top: -2,
    right: -2,
    zIndex: 1
  },

  removeItemButtonIcon: {},

  uploadButton: {
    backgroundColor: globalStyles.color.primary,
    width: 100,
    height: 40,
    marginRight: 10,
    justifyContent: "center",
    alignItems: "center",
    borderRadius: 5
  },

  uploadButtonWrapper: {
    padding: 10,
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
    gap: 6
  },

  uploadButtonText: {
    color: globalStyles.color.light,
    fontWeight: "bold"
  },

  flexOne: {
    flex: 1
  }
});

export default styles;
