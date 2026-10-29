import type { RecipeDto } from "../../Api.ts";
import { courseLabels } from "../courses.ts";

export function RecipeList({ recipes }: { recipes: RecipeDto[] }) {
  return (
    <ul aria-label="Recipes">
      {recipes.map(recipe => (
        <li key={recipe.recipeId}>
          {recipe.recipeName} by {recipe.chef.chefName} · {recipe.cookingMinutes} min · {courseLabels[recipe.course]}
        </li>
      ))}
    </ul>
  );
}
