import { Skeleton } from "@/components/ui/skeleton";

const Loading = () => {
  return (
    <section>
      <Skeleton className='h-32 w-full' />

      <Skeleton className='mb-12 mt-11 h-[700px] w-full' />
    </section>
  );
};

export default Loading;
