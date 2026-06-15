namespace InvoiceEngine.API.Database.Entities;

public class InvoiceCustomIncotermObligation
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public InvoiceItemTypeCode ItemTypeCode { get; set; }
    public InvoiceSubject FromSubjectCode { get; set; }
    public InvoiceSubject ToSubjectCode { get; set; }
    public decimal PercentageAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Invoice Invoice { get; set; }
}
