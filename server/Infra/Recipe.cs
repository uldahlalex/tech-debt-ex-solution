using LinqToDB.Mapping;

namespace Infra;

public class Recipe
{
    [PrimaryKey] public required string RecipeId { get; set; }
    [NotNull] public required string RecipeName { get; set; }
    public int CookingMinutes { get; set; }
    public Course Course { get; set; }
    [NotNull] public required string ChefId { get; set; }
    [Association(ThisKey = nameof(ChefId), OtherKey = nameof(Infra.Chef.ChefId), CanBeNull = false)]
    public Chef Chef { get; set; } = null!;
}

public class Chef
{
    [PrimaryKey] public required string ChefId { get; set; }
    [NotNull] public required string ChefName { get; set; }
    [Association(ThisKey = nameof(ChefId), OtherKey = nameof(Recipe.ChefId))]
    public List<Recipe> RecipesByChef { get; set; } = [];
}

public enum Course
{
    Starter,
    MainCourse,
    Dessert,
    Drink
}
