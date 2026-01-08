namespace ACleanAPI.Infrastructure.Core;

public static class AcModelExtensions
{
    public static void UpdateFrom<TModel>(this TModel target, TModel source)
        where TModel : AcModelBase
    {
        if (target == null)
            throw new ArgumentNullException(nameof(target));
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var properties = typeof(TModel).GetProperties()
            .Where(prop => prop.CanRead && prop.CanWrite && prop.Name != nameof(AcModelBase.Id));

        foreach (var property in properties)
        {
            var value = property.GetValue(source);
            property.SetValue(target, value);
        }
    }
}
