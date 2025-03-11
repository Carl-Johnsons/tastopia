"use client";

import { useParams, useRouter } from "next/navigation";

export default function Users() {
  const router = useRouter();
  const params = useParams<{ id: string }>();
  console.log("params", params.id);

  const handleClick = () => {
    router.push("/users/bb06e4ec-f371-45d5-804e-22c65c77f67d");
  };
  return (
    <div className="flex size-full justify-center">
      <div className="flex w-full flex-col gap-4">hi users</div>
      <button onClick={handleClick}>handleClick</button>
    </div>
  );
}
