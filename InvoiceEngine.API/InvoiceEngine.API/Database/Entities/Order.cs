namespace InvoiceEngine.API.Database.Entities;

public class Order
{
    public int Id { get; set; }
    public string Address { get; set; }
    public DateTime SellDate { get; set; }
    public OrderStatus StatusCode { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public List<OrderItem> Items { get; set; } = new();
    public List<InvoiceItemOrderDetail> InvoiceItemOrderDetails { get; set; }
}
