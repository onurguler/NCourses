namespace NCourses.Services.Catalog.Api.Dtos;

public class CourseCreateDto
{

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string UserId { get; set; } = string.Empty;
    
    public string? Picture { get; set; }

    public FeatureDto? Feature { get; set; }

    public string CategoryId { get; set; } = string.Empty;
}