import { useEffect } from "react";
import i18n from "@/i18n/i18next";
import { useFonts } from "expo-font";
import { Provider } from "react-redux";
import { persistor, store } from "@/store";
import { I18nextProvider } from "react-i18next";
import { SplashScreen, Stack } from "expo-router";
import { StatusBar, StyleSheet } from "react-native";
import { useColorModeValue } from "@/hooks/alternator";
import { PersistGate } from "redux-persist/integration/react";
import { SafeAreaProvider } from "react-native-safe-area-context";
import { GestureHandlerRootView } from "react-native-gesture-handler";
// import { GlobalProvider } from "@/context/GlobalProvider";

import("./global.css");

SplashScreen.preventAutoHideAsync();

const RootLayout = () => {
  const [fontsLoaded, error] = useFonts({
    "Sofia-Pro-Black": require("../assets/fonts/Sofia-Pro-Black-Az.otf"),
    "Sofia-Pro-Black-Italic": require("../assets/fonts/Sofia-Pro-Black-Italic-Az.otf"),
    "Sofia-Pro-Bold": require("../assets/fonts/Sofia-Pro-Bold-Az.otf"),
    "Sofia-Pro-Bold-Italic": require("../assets/fonts/Sofia-Pro-Bold-Italic-Az.otf"),
    "Sofia-Pro-ExtraLight": require("../assets/fonts/Sofia-Pro-ExtraLight-Az.otf"),
    "Sofia-Pro-ExtraLight-Italic": require("../assets/fonts/Sofia-Pro-ExtraLight-Italic-Az.otf"),
    "Sofia-Pro-Light": require("../assets/fonts/Sofia-Pro-Light-Az.otf"),
    "Sofia-Pro-Light-Italic": require("../assets/fonts/Sofia-Pro-Light-Italic-Az.otf"),
    "Sofia-Pro-Medium": require("../assets/fonts/Sofia-Pro-Medium-Az.otf"),
    "Sofia-Pro-Medium-Italic": require("../assets/fonts/Sofia-Pro-Medium-Italic-Az.otf"),
    "Sofia-Pro-Regular": require("../assets/fonts/Sofia-Pro-Regular-Az.otf"),
    "Sofia-Pro-Regular-Italic": require("../assets/fonts/Sofia-Pro-Regular-Italic-Az.otf"),
    "Sofia-Pro-Semi-Bold": require("../assets/fonts/Sofia-Pro-Semi-Bold-Az.otf"),
    "Sofia-Pro-Semi-Bold-Italic": require("../assets/fonts/Sofia-Pro-Semi-Bold-Italic-Az.otf"),
    "Sofia-Pro-UltraLight": require("../assets/fonts/Sofia-Pro-UltraLight-Az.otf"),
    "Sofia-Pro-UltraLight-Italic": require("../assets/fonts/Sofia-Pro-UltraLight-Italic-Az.otf")
  });
  const bgColor = useColorModeValue("white", "black");

  useEffect(() => {
    if (error) throw error;

    if (fontsLoaded) SplashScreen.hideAsync();
  }, [fontsLoaded, error]);

  if (!fontsLoaded && !error) return null;

  return (
    <GestureHandlerRootView>
      <Provider store={store}>
        <PersistGate
          loading={null}
          persistor={persistor}
        >
          <I18nextProvider i18n={i18n}>
            <SafeAreaProvider>
              <StatusBar backgroundColor={bgColor} />
              <Stack
                screenOptions={{
                  headerShown: false
                }}
              >
                <Stack.Screen name='(public)' />
                <Stack.Screen name='(protected)' />
                <Stack.Screen name='+not-found' />
              </Stack>
            </SafeAreaProvider>
          </I18nextProvider>
        </PersistGate>
      </Provider>
    </GestureHandlerRootView>
  );
};

export default RootLayout;
