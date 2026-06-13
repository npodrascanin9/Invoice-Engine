namespace InvoiceEngine.API.Database.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UnitOfMeasureType UnitOfMeasureCode { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }

    public List<Article> Articles { get; set; } = new();
}
