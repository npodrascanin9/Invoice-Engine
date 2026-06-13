namespace InvoiceEngine.API.Database.Entities;

public class InsuranceCompany
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? IdentificationNumber { get; set; }

    public List<InvoiceItemInsuranceDetail> InvoiceItemInsuranceDetails { get; set; } = new();
}
