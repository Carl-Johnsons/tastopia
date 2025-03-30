import { handleSignIn, handleSignOut } from "@/actions/auth";
import { auth } from "@/auth";

export default async function LoginForm() {
  const session = await auth();

  if (session) {
    return (
      <form
        action={handleSignOut}
        className='rounded-lg border border-gray-200 p-1 hover:bg-gray-200'
      >
        <button type='submit'>
          Sign out from <span className='text-primary'>Tastopia</span>
        </button>
      </form>
    );
  }

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
