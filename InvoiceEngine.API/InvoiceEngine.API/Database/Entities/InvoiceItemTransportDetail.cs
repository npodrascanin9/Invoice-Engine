namespace InvoiceEngine.API.Database.Entities;

public class InvoiceItemTransportDetail
{
    public int Id { get; set; }
    public int InvoiceItemId { get; set; }
    public int? TransportCompanyId { get; set; }
    public string AddressFrom { get; set; }
    public string AddressTo { get; set; }


    public InvoiceItem InvoiceItem { get; set; }
    public TransportCompany TransportCompany { get; set; }
}
