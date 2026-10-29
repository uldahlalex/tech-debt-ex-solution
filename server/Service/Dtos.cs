using Facet;
using Infra;

namespace Service;

[Facet(sourceType: typeof(Chef), exclude: nameof(Chef.RecipesByChef))]
public partial class ChefDto;

[Facet(sourceType: typeof(Recipe), NestedFacets = [typeof(ChefDto)])]
public partial class RecipeDto;

public record CreateRecipeRequestDto(string RecipeName, int CookingMinutes, string ChefId, Course Course);

public record CreateChefRequestDto(string ChefName);
