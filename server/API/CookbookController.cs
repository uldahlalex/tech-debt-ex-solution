using Microsoft.AspNetCore.Mvc;
using Service;

namespace API;

[ApiController]
public class CookbookController(CookbookService service) : ControllerBase
{
    [HttpGet(nameof(GetRecipes))]
    public List<RecipeDto> GetRecipes(int page, int resultsPerPage) => service.GetRecipes(page, resultsPerPage);

    [HttpPost(nameof(CreateRecipe))]
    public RecipeDto CreateRecipe(CreateRecipeRequestDto dto) => service.CreateRecipe(dto);

    [HttpGet(nameof(GetChefs))]
    public List<ChefDto> GetChefs() => service.GetChefs();

    [HttpPost(nameof(CreateChef))]
    public ChefDto CreateChef(CreateChefRequestDto dto) => service.CreateChef(dto);
}
