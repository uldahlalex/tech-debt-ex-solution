namespace Service;

/// <summary>
/// The single source of truth for every business limit in the cookbook.
/// </summary>
public static class CookbookRules
{
    public const int MaxRecipeNameLength = 100;
    public const int MinCookingMinutes = 1;
    public const int MaxCookingMinutes = 24 * 60;
    public const int MaxChefNameLength = 60;
    public const int MaxResultsPerPage = 50;
}
