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
import { FONTS } from "@/constants/fonts";
import useColorizer from "@/hooks/useColorizer";

import("./global.css");

SplashScreen.preventAutoHideAsync();

const RootLayout = () => {
  const queryClient = new QueryClient();
  const [fontsLoaded, error] = useFonts(FONTS);
  const { white, black } = colors;
  const { c } = useColorizer();
  const bgColor = c(white.DEFAULT, black.DEFAULT);
  const barStyle = c("dark-content", "light-content");

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
                  <StatusBar
                    backgroundColor={bgColor}
                    barStyle={barStyle}
                  />
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
