using Microsoft.AspNetCore.Mvc;

namespace API;

public static class ProblemResponses
{
    /// <summary>
    /// Makes request-binding errors (missing fields, unknown enum values, malformed JSON)
    /// look like every other validation error: a 400 ProblemDetails with a readable title.
    /// </summary>
    public static IActionResult FromInvalidModelState(ActionContext context)
    {
        // JSON errors are keyed by their JSON path, e.g. "$.course".
        var invalidJsonField = context.ModelState
            .Where(e => e.Key.StartsWith("$.") && e.Value is { Errors.Count: > 0 })
            .Select(e => e.Key[2..])
            .FirstOrDefault();

        var firstError = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .FirstOrDefault(m => !string.IsNullOrWhiteSpace(m));

        return new BadRequestObjectResult(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = invalidJsonField is not null
                ? $"Invalid value for '{invalidJsonField}'"
                : firstError ?? "The request is invalid"
        });
    }
}
