namespace InvoiceEngine.API.Database.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ArticleId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Order Order { get; set; }
    public Article Article { get; set; }
}
