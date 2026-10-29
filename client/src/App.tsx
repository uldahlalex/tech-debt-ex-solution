import "./index.css";
import { useCallback, useEffect, useState } from "react";
import toast from "react-hot-toast";
import type { ChefDto, CreateRecipeRequestDto, RecipeDto } from "../Api.ts";
import { api, getErrorMessage } from "./api.ts";
import { RECIPES_PER_PAGE } from "./config.ts";
import { RecipeList } from "./components/RecipeList.tsx";
import { Pagination } from "./components/Pagination.tsx";
import { CreateRecipeForm } from "./components/CreateRecipeForm.tsx";
import { CreateChefForm } from "./components/CreateChefForm.tsx";

export function App() {
  const [recipes, setRecipes] = useState<RecipeDto[]>([]);
  const [chefs, setChefs] = useState<ChefDto[]>([]);
  const [page, setPage] = useState(1);

  const loadRecipes = useCallback(async () => {
    try {
      const response = await api.getRecipes.cookbookGetRecipes({ page, resultsPerPage: RECIPES_PER_PAGE });
      setRecipes(response.data);
    } catch (e) {
      toast.error(getErrorMessage(e));
    }
  }, [page]);

  const loadChefs = useCallback(async () => {
    try {
      const response = await api.getChefs.cookbookGetChefs();
      setChefs(response.data);
    } catch (e) {
      toast.error(getErrorMessage(e));
    }
  }, []);

  useEffect(() => { void loadRecipes(); }, [loadRecipes]);
  useEffect(() => { void loadChefs(); }, [loadChefs]);

  async function createRecipe(request: CreateRecipeRequestDto): Promise<boolean> {
    try {
      const response = await api.createRecipe.cookbookCreateRecipe(request);
      toast.success(`Created "${response.data.recipeName}"`);
      await loadRecipes();
      return true;
    } catch (e) {
      toast.error(getErrorMessage(e));
      return false;
    }
  }

  async function createChef(chefName: string): Promise<boolean> {
    try {
      const response = await api.createChef.cookbookCreateChef({ chefName });
      toast.success(`Created chef "${response.data.chefName}"`);
      await loadChefs();
      return true;
    } catch (e) {
      toast.error(getErrorMessage(e));
      return false;
    }
  }

  return (
    <main className="app">
      <h1>Cookbook</h1>
      <RecipeList recipes={recipes} />
      <Pagination page={page} hasNextPage={recipes.length === RECIPES_PER_PAGE} onPageChange={setPage} />
      <CreateRecipeForm chefs={chefs} onCreate={createRecipe} />
      <CreateChefForm onCreate={createChef} />
    </main>
  );
}

export default App;
