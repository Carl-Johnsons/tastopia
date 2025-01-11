import { StyleSheet } from "react-native";
import { globalStyles } from "../GlobalStyles";

const styles = StyleSheet.create({
  container: {
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center"
  },

  wrapper: {
    flexDirection: "column",
    flexWrap: "wrap",
    alignItems: "center",
    justifyContent: "center",
    gap: 6
  },

  previewImage: {
    flexDirection: "row",
    flexWrap: "wrap",
    alignItems: "center",
    justifyContent: "center",
    gap: 6
  },

  uploadItem: {
    position: "relative",
    width: "31%",
    borderWidth: 2,
    borderRadius: 5,
    borderColor: globalStyles.color.light,
    overflow: "hidden"
  },

  uploadItemImage: {
    width: "100%",
    aspectRatio: 1.6
  },

  removeOverlay: {
    position: "absolute",
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    backgroundColor: "rgba(0, 0, 0, 0.7)",
    justifyContent: "center",
    alignItems: "center"
  },

  removeIconWrapper: {
    alignItems: "center",
    justifyContent: "center"
  },

  removeText: {
    color: globalStyles.color.light,
    fontSize: 12,
    fontWeight: "bold"
  },

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
