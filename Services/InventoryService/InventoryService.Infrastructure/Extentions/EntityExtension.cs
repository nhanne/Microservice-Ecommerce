namespace InventoryService.Infrastructure.Extentions;

public static class EntityExtension
{
    public static Guid GetId(this object entity)
    {
        var propertyInfo = entity.GetType().GetProperty("Id");
        var value = propertyInfo?.GetValue(entity, null);

        if (value == null)
        {
            throw new InvalidOperationException("Entity does not have a valid Id");
        }

        return (Guid)value;
    }
}
