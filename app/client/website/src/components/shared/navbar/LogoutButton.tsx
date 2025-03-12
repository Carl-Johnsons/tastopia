import { handleSignOut } from "@/actions/auth";

const LogoutButton = () => {
  return (
    <form action={handleSignOut} className="flex items-center gap-2">
      <button type="submit">
        <span className="text-black_white">Sign out</span>
      </button>
    </form>
  );
};

export default LogoutButton;
