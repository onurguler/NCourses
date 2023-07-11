namespace NCourses.Services.Catalog.Api.Configuration;

public class DatabaseSettings : IDatabaseSettings
{
    public string CourseCollectionName { get; set; } = string.Empty;
    public string CategoryCollectionName { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
}