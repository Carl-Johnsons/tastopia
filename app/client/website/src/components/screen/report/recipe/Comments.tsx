import { IComment } from "@/generated/interfaces/recipe.interface";

type Props = {
  comments: IComment[];
};

export default function Comments({ comments }: Props) {

  if (comments.length === 0) {
    return (
      <div className='flex-center flex h-[100px] w-full'>
        <span className="text-black_white">This recipe does not have any comment.</span>
      </div>
    );
  }

  return (
    <div className="flex flex-col gap-3">
      <h2 className='text-black_white font-semibold text-2xl'>Comments</h2>

    </div>
  );
}
