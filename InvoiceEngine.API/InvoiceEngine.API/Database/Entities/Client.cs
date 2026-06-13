namespace InvoiceEngine.API.Database.Entities;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? IdentificationNumber { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<InvoiceClient> InvoiceClients { get; set; } = new();
}
