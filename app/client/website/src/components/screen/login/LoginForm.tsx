import { handleSignIn } from "@/actions/auth";

export default function LoginForm() {
  return (
    <form
      action={handleSignIn}
      className='rounded-lg border border-gray-200 p-1 hover:bg-gray-200'
    >
      <button type='submit'>
        Sign in with <span className='text-primary'>Tastopia account</span>
      </button>
    </form>
  );
}
