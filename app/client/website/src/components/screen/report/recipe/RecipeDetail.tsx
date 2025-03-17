import Header from "./Header";
import { IRecipe } from "../../../../../../mobile/generated/interfaces/recipe.interface";
import { getUserById } from "@/actions/user.action";
import { IUser } from "../../../../../../mobile/generated/interfaces/user.interface";
import Content from "./Content";
import Comments from "./Comments";

type Props = {
  recipe: IRecipe;
  className?: string;
};

export default async function RecipeDetail({ recipe, className }: Props) {
  const {
    id,
    title,
    imageUrl,
    description,
    isActive,
    voteDiff,
    authorId,
    ingredients,
    serves,
    steps,
    cookTime,
    comments,
    numberOfComment
  } = recipe;

  const author = (await getUserById(authorId)) as IUser;

  return (
    <div
      className={`bg-white_black100 flex flex-col gap-9 rounded-xl border border-gray-200 p-6 shadow-sm dark:border-gray-600 ${className}`}
    >
      <Header
        recipeId={id}
        title={title}
        imageUrl={imageUrl}
        author={author}
        description={description}
        isActive={isActive}
        voteDiff={voteDiff}
      />
      <div className='w-full border-b border-gray-200 dark:border-gray-600 lg:hidden' />
      <Content
        ingredient={ingredients}
        serves={serves}
        steps={steps}
        cookTime={cookTime}
      />
      <div className='w-full border-b border-gray-200 dark:border-gray-600' />
      <Comments
        comments={comments}
        numberOfComment={numberOfComment}
      />
    </div>
  );
}
