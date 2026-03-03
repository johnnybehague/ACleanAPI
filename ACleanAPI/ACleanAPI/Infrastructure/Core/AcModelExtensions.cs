namespace ACleanAPI.Infrastructure.Core;

/// <summary>
/// Extension methods for models in the infrastructure layer.
/// </summary>
/// <remarks>
/// These methods provide common functionality for working with models, such as updating properties from another model.
/// </remarks>
public static class AcModelExtensions
{
    /// <summary>
    /// Updates the properties of the target model with the values from the source model, excluding the Id property.
    /// </summary>
    /// <typeparam name="TModel">Model</typeparam>
    /// <param name="target">Target model</param>
    /// <param name="source">Source model</param>
    public static void UpdateFrom<TModel>(this TModel target, TModel? source)
        where TModel : AcModelBase
    {
        ArgumentNullException.ThrowIfNull(source);

        var properties = typeof(TModel).GetProperties()
            .Where(prop => prop.Name != nameof(AcModelBase.Id));

        foreach (var property in properties)
        {
            var value = property.GetValue(source);
            property.SetValue(target, value);
        }
    }
}
