using LinqToDB;

namespace Infra;

public static class DatabaseSeeder
{
    public static void CreateSchemaAndSeed(MyDatabaseConnection db)
    {
        db.CreateTable<Recipe>(tableOptions: TableOptions.CreateIfNotExists);
        db.CreateTable<Chef>(tableOptions: TableOptions.CreateIfNotExists);

        if (!db.Chefs.Any())
            db.Insert(new Chef { ChefId = "1", ChefName = "alice" });

        if (!db.Recipes.Any())
            db.Insert(new Recipe
            {
                RecipeId = "1",
                RecipeName = "pancakes",
                CookingMinutes = 20,
                Course = Course.Dessert,
                ChefId = "1"
            });
    }
}
