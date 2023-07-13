namespace NCourses.Services.Basket.Api.Dtos;

public class BasketItemDto
{
    public int Quantity { get; set; } = 1;
    public string CourseId { get; set; } = null!;
    public string CourseName { get; set; } = null!;
    public decimal Price { get; set; }
}