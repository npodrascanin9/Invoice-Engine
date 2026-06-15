using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceEngine.API.Database.Entities;

public class InvoiceItem
{
    public int Id { get; set; }
    public InvoiceItemTypeCode ItemTypeCode { get; set; }
    public int InvoiceId { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }


    public Invoice Invoice { get; set; }
    public List<InvoiceItemObligation> ItemObligations { get; set; } = new();
}
