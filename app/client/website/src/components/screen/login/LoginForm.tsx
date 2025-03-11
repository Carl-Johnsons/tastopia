import { handleSignIn } from "@/actions/auth";

export default function LoginForm() {
  return (
    <form
      action={handleSignIn}
      className="border border-gray-200 hover:bg-gray-200 rounded-md "
    >
      <button type="submit">
        Sign in with <span className="text-primary">Tastopia account</span>
      </button>
    </form>
  );
}
