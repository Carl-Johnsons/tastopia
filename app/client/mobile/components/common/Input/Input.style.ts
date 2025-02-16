import { globalStyles } from "../GlobalStyles";
import { StyleSheet } from "react-native";

export default StyleSheet.create({
  label: {
    fontSize: 12,
    marginBottom: 8,
    fontWeight: 600
  },
  wrapperInput: {
    flexDirection: "row",
    alignItems: "center",
    marginBottom: 8
  },
  passwordInputContainer: {
    position: "relative",
    flex: 1,
    flexDirection: "row",
    alignItems: "center",
    width: "100%",
    borderRadius: 7
  },
  svg: {
    position: "absolute",
    width: 16,
    height: 16,
    top: "-18%",
    right: 14,
    color: "#7e868f"
  },
  input: {
    flex: 1,
    borderColor: "#ccc",
    borderWidth: 1,
    borderRadius: 4,
    paddingVertical: 6,
    paddingHorizontal: 12,
    lineHeight: 16,
    fontSize: 15,
    backgroundColor: "#FFFFFF"
  },
  inputPassword: {
    paddingRight: 30
  },
  errorContainer: {
    marginTop: -8,
    marginBottom: 8
  },
  inputFocused: {
    borderColor: "#3b7079",
    shadowColor: "0 0 0 4" + globalStyles.color.primaryMoreOpacity
  },
  calendarIcon: {
    position: "absolute",
    top: "30%",
    right: 14
  },
  secondaryInput: {
    backgroundColor: "transparent",
    borderWidth: 0,
    borderBottomWidth: 1,
    borderBottomColor: globalStyles.color.gray400,
    borderRadius: 0,
    paddingHorizontal: 0
  },

  secondaryInputFocused: {
    borderBottomColor: globalStyles.color.primary
  }
});
