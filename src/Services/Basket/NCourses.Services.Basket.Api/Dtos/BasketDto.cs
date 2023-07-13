namespace NCourses.Services.Basket.Api.Dtos;

public class BasketDto
{
    public string UserId { get; set; } = null!;
    public string DiscountCode { get; set; } = null!;
    public List<BasketItemDto> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}