import { useEffect } from "react";
import { useFonts } from "expo-font";
import { SplashScreen, Stack } from "expo-router";
import { SafeAreaProvider } from "react-native-safe-area-context";
import { GestureHandlerRootView } from "react-native-gesture-handler";
import { Provider } from "react-redux";
import { persistor, store } from "@/store";
import { PersistGate } from "redux-persist/integration/react";
import { StatusBar, StyleSheet } from "react-native";
import { useColorModeValue } from "@/hooks/alternator";
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
  const textColor = useColorModeValue("black", "white");
  const statusBarStyle = useColorModeValue("dark-content", "light-content");

  useEffect(() => {
    if (error) throw error;

    if (fontsLoaded) SplashScreen.hideAsync();
  }, [fontsLoaded, error]);

  if (!fontsLoaded && !error) return null;

  const styles = StyleSheet.create({
    statusBar: {
      backgroundColor: bgColor
    },
    header: {
      backgroundColor: bgColor
    }
  });

  return (
    <GestureHandlerRootView>
      <Provider store={store}>
        <PersistGate
          loading={null}
          persistor={persistor}
        >
          <SafeAreaProvider>
            <StatusBar barStyle={statusBarStyle} />
            <Stack screenOptions={{ headerShown: false }}>
              <Stack.Screen name='(public)' />
              <Stack.Screen
                name='(modals)/createPost'
                options={{ presentation: "modal" }}
              />
              <Stack.Screen name='(protected)' />
              <Stack.Screen name='+not-found' />
            </Stack>
          </SafeAreaProvider>
        </PersistGate>
      </Provider>
    </GestureHandlerRootView>
  );
};

export default RootLayout;
