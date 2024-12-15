import { loginSchema, signUpSchema } from "@/lib/validation/auth";
import { fetchApi } from "@/utils/fetch";
import { z } from "zod";

export type LoginParams = z.infer<typeof loginSchema>;

export type User = {
    _id: string;
    name: string;
    email: string;
    username: string;
    bio: string;
    profilePic: string;
    followers: string[];
    following: string[];
};

type LoginResponse = {
    jwtToken: string;
    user: User;
};

export const signIn = async (inputs: LoginParams): Promise<LoginResponse> => {
    const parsedInputs = loginSchema.parse(inputs);

    const res = await fetchApi("/api/users/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(parsedInputs),
    });

    const data = await res.json();
    if (data.error) throw new Error(data.error);

    return data;
};

export type SignUpParams = z.infer<typeof signUpSchema>;

type SignUpResponse = {
    jwtToken: string;
    user: {
        _id: string;
        name: string;
        email: string;
        username: string;
        bio: string;
        profilePic: string;
    };
};

export const register = async (inputs: SignUpParams): Promise<SignUpResponse> => {
    const parsedInputs = signUpSchema.parse(inputs);

    const res = await fetchApi("/api/users/signup", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(parsedInputs),
    });

    const data = await res.json();
    if (data.error) throw new Error(data.error);

    return data;
};
