using NJsonSchema;
using NJsonSchema.Generation;

namespace API;

/// <summary>
/// Marks every non-nullable C# property as required in the OpenAPI document,
/// so the generated TypeScript client gets `recipeName: string` instead of `recipeName?: string`.
/// </summary>
public class RequireNonNullablePropertiesSchemaProcessor : ISchemaProcessor
{
    public void Process(SchemaProcessorContext context)
    {
        foreach (var (name, property) in context.Schema.ActualProperties)
        {
            if (!property.IsNullable(SchemaType.OpenApi3) && !context.Schema.RequiredProperties.Contains(name))
                context.Schema.RequiredProperties.Add(name);
        }
    }
}
