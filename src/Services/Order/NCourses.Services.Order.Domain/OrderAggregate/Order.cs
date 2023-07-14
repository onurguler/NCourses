using NCourses.Services.Order.Domain.Core;

namespace NCourses.Services.Order.Domain.OrderAggregate;

public class Order : Entity, IAggregateRoot
{
    public DateTime CreatedDate { get; private set; }
    public Address Address { get; private set; }
    public string BuyerId { get; private set; }

    private readonly List<OrderItem> _orderItems;
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public Order(string buyerId, Address address)
    {
        _orderItems = new List<OrderItem>();
        CreatedDate = DateTime.Now;
        BuyerId = buyerId;
        Address = address;
    }

    public void AddOrderItem(string productId, string productName, string pictureUrl, decimal price)
    {
        var existProduct = _orderItems.Exists(x => x.ProductId == productId);
        if (existProduct)
            return;
        var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
        _orderItems.Add(newOrderItem);
    }

    public decimal TotalPrice => _orderItems.Sum(x => x.Price);
}