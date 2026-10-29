using System.ComponentModel.DataAnnotations;
using Infra;
using LinqToDB;

namespace Service;

public class CookbookService(MyDatabaseConnection db)
{
    public List<RecipeDto> GetRecipes(int page, int resultsPerPage)
    {
        if (page < 1)
            throw new ValidationException("Page must be 1 or higher");
        if (resultsPerPage is < 1 or > CookbookRules.MaxResultsPerPage)
            throw new ValidationException($"Results per page must be between 1 and {CookbookRules.MaxResultsPerPage}");

        return db.Recipes
            .LoadWith(r => r.Chef)
            .OrderBy(r => r.RecipeName)
            .ThenBy(r => r.RecipeId)
            .Skip((page - 1) * resultsPerPage)
            .Take(resultsPerPage)
            .ToList()
            .Select(r => new RecipeDto(r))
            .ToList();
    }

    public RecipeDto CreateRecipe(CreateRecipeRequestDto dto)
    {
        var name = dto.RecipeName.Trim();
        if (name.Length == 0)
            throw new ValidationException("Name is required");
        if (name.Length > CookbookRules.MaxRecipeNameLength)
            throw new ValidationException($"Name must be at most {CookbookRules.MaxRecipeNameLength} characters");
        if (dto.CookingMinutes is < CookbookRules.MinCookingMinutes or > CookbookRules.MaxCookingMinutes)
            throw new ValidationException(
                $"Cooking time must be between {CookbookRules.MinCookingMinutes} and {CookbookRules.MaxCookingMinutes} minutes");
        if (!Enum.IsDefined(dto.Course))
            throw new ValidationException("Unknown course");

        var chef = db.Chefs.FirstOrDefault(c => c.ChefId == dto.ChefId)
                   ?? throw new ValidationException("Chef does not exist");

        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid().ToString(),
            RecipeName = name,
            CookingMinutes = dto.CookingMinutes,
            Course = dto.Course,
            ChefId = chef.ChefId,
            Chef = chef
        };
        db.Insert(recipe);
        return new RecipeDto(recipe);
    }

    public List<ChefDto> GetChefs()
    {
        return db.Chefs
            .OrderBy(c => c.ChefName)
            .ToList()
            .Select(c => new ChefDto(c))
            .ToList();
    }

    public ChefDto CreateChef(CreateChefRequestDto dto)
    {
        var name = dto.ChefName.Trim();
        if (name.Length == 0)
            throw new ValidationException("Chef name is required");
        if (name.Length > CookbookRules.MaxChefNameLength)
            throw new ValidationException($"Chef name must be at most {CookbookRules.MaxChefNameLength} characters");

        var chef = new Chef { ChefId = Guid.NewGuid().ToString(), ChefName = name };
        db.Insert(chef);
        return new ChefDto(chef);
    }
}
