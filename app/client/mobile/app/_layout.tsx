import { useEffect } from "react";
import i18n from "@/i18n/i18next";
import { useFonts } from "expo-font";
import { Provider } from "react-redux";
import { persistor, store } from "@/store";
import { I18nextProvider } from "react-i18next";
import { SplashScreen, Stack } from "expo-router";
import { StatusBar } from "react-native";
import { getColorSchemeValue } from "@/hooks/alternator";
import { PersistGate } from "redux-persist/integration/react";
import { SafeAreaProvider } from "react-native-safe-area-context";
import { GestureHandlerRootView } from "react-native-gesture-handler";
import { QueryClient, QueryClientProvider } from "react-query";
// import { GlobalProvider } from "@/context/GlobalProvider";
import { BottomSheetModalProvider } from "@gorhom/bottom-sheet";
import { useColorScheme } from "nativewind";
import { colors } from "@/constants/colors";

import("./global.css");

SplashScreen.preventAutoHideAsync();

const RootLayout = () => {
  const queryClient = new QueryClient();
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
  const { colorScheme } = useColorScheme();
  const bgColor = getColorSchemeValue(
    colorScheme,
    colors.black.DEFAULT,
    colors.white.DEFAULT
  );

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
          <QueryClientProvider client={queryClient}>
            <I18nextProvider i18n={i18n}>
              <SafeAreaProvider>
                <BottomSheetModalProvider>
                  <StatusBar backgroundColor={bgColor} />
                  <Stack
                    screenOptions={{
                      headerShown: false
                    }}
                  >
                    <Stack.Screen name='(public)' />
                    <Stack.Screen name='(protected)' />
                    <Stack.Screen
                      name='(modals)'
                      options={{ presentation: "modal" }}
                    />
                    <Stack.Screen name='+not-found' />
                  </Stack>
                </BottomSheetModalProvider>
              </SafeAreaProvider>
            </I18nextProvider>
          </QueryClientProvider>
        </PersistGate>
      </Provider>
    </GestureHandlerRootView>
  );
};

export default RootLayout;
