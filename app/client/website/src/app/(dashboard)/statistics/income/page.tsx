import { getUserDetails } from "@/api/user";
import { auth } from "@/auth";

const Income = async () => {
  const session = await auth();
  const accessToken = session?.accessToken;

  if (!accessToken) {
    return (
      <div>
        Loading...
        <br />
      </div>
    );
  }

  const user = await getUserDetails();

  return (
    <>
      <div key={accessToken?.name}>
        <p>Name: {accessToken?.name}</p>
        <p>Value: {accessToken?.value}</p>
      </div>
      <div className="flex flex-col size-full justify-center">
        <div className="flex w-full flex-col gap-4">Hi, income</div>
        {!!user && <div>user: {JSON.stringify(user, null, 2)}</div>}
      </div>
    </>
  );
};

export default Income;
