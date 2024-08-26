namespace CatalogService.Common.Constants;
public static class ClothingConstants
{
    public const int CodeLength = 10;
    public const int NameLength = 20;
    public const int NoteLength = 1024;
    public const int ImageLength = 1024;
    public const int AddressLength = 1024;
    public const string Required = "{0} is required";
    public const string ParamIdIsNotGuid = "The provided ID is not a valid GUID";
    public const string ApiSwaggerRoute = "/swagger/v1/swagger.json";
    public const string ApiSwaggerVersion = "My API V1";
    public const string Resource = nameof(Resource);
}