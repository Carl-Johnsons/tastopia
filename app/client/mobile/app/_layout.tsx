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
    "Poppins-Black": require("../assets/fonts/Poppins-Black.ttf"),
    "Poppins-Bold": require("../assets/fonts/Poppins-Bold.ttf"),
    "Poppins-ExtraBold": require("../assets/fonts/Poppins-ExtraBold.ttf"),
    "Poppins-ExtraLight": require("../assets/fonts/Poppins-ExtraLight.ttf"),
    "Poppins-Light": require("../assets/fonts/Poppins-Light.ttf"),
    "Poppins-Medium": require("../assets/fonts/Poppins-Medium.ttf"),
    "Poppins-Regular": require("../assets/fonts/Poppins-Regular.ttf"),
    "Poppins-SemiBold": require("../assets/fonts/Poppins-SemiBold.ttf"),
    "Poppins-Thin": require("../assets/fonts/Poppins-Thin.ttf")
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
