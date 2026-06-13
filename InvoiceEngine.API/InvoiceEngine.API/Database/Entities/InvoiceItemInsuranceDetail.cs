namespace InvoiceEngine.API.Database.Entities;

public class InvoiceItemInsuranceDetail
{
    public int Id { get; set; }
    public int InvoiceItemId { get; set; }
    public int InsuranceCompanyId { get; set; }
    public DateOnly ExpiresAt { get; set; }

    public InvoiceItem InvoiceItem { get; set; }
    public InsuranceCompany InsuranceCompany { get; set; }
}
