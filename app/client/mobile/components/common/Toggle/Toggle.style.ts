import { Platform, StyleSheet } from "react-native";

export default StyleSheet.create({
  container: {
    width: "28%",
    display: "flex",
    flexWrap: "wrap"
  },

  "switch-holder": {
    width: 115,
    paddingVertical: 8,
    paddingHorizontal: 5,
    borderRadius: 10,
    shadowColor: "black",
    ...(Platform.OS === "ios"
      ? {
          shadowOffset: { width: 0, height: 5 },
          shadowOpacity: 0.1,
          shadowRadius: 3
        }
      : {
          elevation: 5
        }),
    backgroundColor: "white"
  },

  "toggle-title": {
    display: "flex",
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "center",
    marginBottom: 2
  },

  "switch-toggle-holder": {
    display: "flex",
    alignItems: "center",
    justifyContent: "center"
  },

  "toggle-label": {
    position: "absolute",
    ...(Platform.OS === "ios" ? { top: 2.5 } : { top: 2 }),
    left: 11,
    width: 22,
    height: 10,
    borderRadius: 10
  },

  "toggle-on": {
    backgroundColor: "#387562"
  },

  "toggle-off": {
    backgroundColor: "#9E9E9E"
  },

  "inset-shadow": {
    position: "absolute",
    top: -6,
    width: 100,
    height: 2,
    shadowColor: "black",
    ...(Platform.OS === "ios"
      ? {
          shadowOffset: { width: 0, height: 5 },
          shadowOpacity: 0.5
        }
      : {
          elevation: 8
        }),
    backgroundColor: "white"
  }
});
