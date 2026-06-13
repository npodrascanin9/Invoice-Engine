namespace InvoiceEngine.API.Database.Entities;

public class Article
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string? Code { get; set; }
    public DateTime? ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Product Product { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}
