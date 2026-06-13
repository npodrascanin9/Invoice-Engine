namespace InvoiceEngine.API.Database.Entities;

public class InvoiceClient
{
    public int InvoiceId { get; set; }
    public int ClientId { get; set; }
    public InvoiceSubject SubjectCode { get; set; }

    public Invoice Invoice { get; set; }
    public Client Client { get; set; }
}
