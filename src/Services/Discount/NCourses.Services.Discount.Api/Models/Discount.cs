using Dapper.Contrib.Extensions;

namespace NCourses.Services.Discount.Api.Models;

[Table("discounts")]
public class Discount
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public int Rate { get; set; }
    public string Code { get; set; } = null!;
    public DateTime CreatedTime { get; set; }
}