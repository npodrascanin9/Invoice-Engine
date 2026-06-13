namespace InvoiceEngine.API.Database.Entities;

public class InvoiceItemType
{
    public int Id { get; set; } 
    // Id => InvoiceItemType enum
    // Id is not identity!
    public string Name { get; set; }
    public string Description { get; set; }
}
