import { Alert, Pressable, Text, View } from "react-native";
import React, { useState } from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { router } from "expo-router";
import SignUpForm, { SignUpFormFields } from "@/components/SignUpForm";
import { useAppDispatch } from "@/store/hooks";
import { saveAuthData } from "@/slices/auth.slice";
import { ZodError } from "zod";
import { register } from "@/api/user";
import { saveUserData } from "@/slices/user.slice";
import Logo from "../../assets/logo.svg";
import { useColorModeValue } from "@/hooks/alternator";

const SignUp = () => {
    const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
    const dispatch = useAppDispatch();

    const signUp = async (data: SignUpFormFields) => {
        setIsSubmitting(true);

        try {
            const res = await register(data);
            const jwtToken = res.jwtToken;
            const user = res.user;

            console.log("data in signup", JSON.stringify(user, null, 2));

            dispatch(saveAuthData({ jwtToken }));
            dispatch(saveUserData(user));

            const route = "/(protected)";
            console.log("navigating to route", route);

            router.navigate(route);
        } catch (error: any) {
            if (error instanceof ZodError) {
                const firstErr = error.issues[0];
                return Alert.alert("Error", firstErr.message);
            }

            Alert.alert("Error", error.message);
        } finally {
            setIsSubmitting(false);
        }
    };

    const navigateToSignInScreen = () => {
        router.navigate("/sign-in");
    };

    return (
        <SafeAreaView>
            <View className="flex h-full justify-center gap-2 p-4 dark:bg-black-200">
                <View className="items-center">
                    <Logo width={100} height={100} fill={useColorModeValue("black", "white")} />
                </View>
                <SignUpForm onSubmit={signUp} isLoading={isSubmitting} className="mt-10" />
                <Pressable>
                    <Text className="active:color-gray text-center active:underline dark:text-white" onPress={navigateToSignInScreen}>
                        I wanna sign in instead ☝️
                    </Text>
                </Pressable>
            </View>
        </SafeAreaView>
    );
};

export default SignUp;
