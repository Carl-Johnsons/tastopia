// import { z } from "zod";

// export const loginSchema = z.object({
//   username: z.string().min(1, "Please enter username."),
//   password: z.string().min(1, "Please enter password.")
// });

// export const signUpSchema = z.object({
//   name: z.string().min(1, "Please enter name."),
//   username: z.string().min(1, "Please enter username."),
//   email: z
//     .string()
//     .regex(
//       /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
//       "Invalid email address."
//     ),
//   password: z.string().min(6, "Password must be at least 6 characters.")
// });
