import { Loader2 } from "lucide-react";

const Loader = () => {
  return (
    <div className="flex items-center justify-center py-10">
      <Loader2 className="size-6 animate-spin text-primary" />
    </div>
  );
};

export default Loader;
