module.exports = {
  expo: {
    name: "tastopia",
    slug: "tastopia",
    version: "1.0.0",
    orientation: "portrait",
    icon: "./assets/icon.png",
    scheme: "tastopia",
    userInterfaceStyle: "automatic",
    splash: {
      image: "./assets/splash.png",
      resizeMode: "contain",
      backgroundColor: "#ffffff"
    },
    ios: {
      supportsTablet: true,
      bundleIdentifier: "com.tastopia.app"
    },
    android: {
      adaptiveIcon: {
        foregroundImage: "./assets/splash.png",
        backgroundColor: "#ffffff"
      },
      package: "com.tastopia.app",
      softwareKeyboardLayoutMode: "pan",
      permissions: ["android.permission.RECORD_AUDIO"],
      googleServicesFile: process.env.GOOGLE_SERVICES_JSON
    },
    web: {
      bundler: "metro",
      output: "static",
      favicon: "./assets/favicon.png"
    },
    plugins: [
      "expo-router",
      [
        "expo-image-picker",
        {
          photosPermission:
            "The app accesses your photos to let you share them with your friends."
        }
      ],
      "expo-font",
      "expo-asset"
    ],
    experiments: {
      typedRoutes: true
    },
    newArchEnabled: true,
    extra: {
      router: {
        origin: false
      },
      eas: {
        projectId: "26579d58-8100-4187-a0de-c675e877d2ad"
      }
    },
    owner: "tastopia"
  }
};
