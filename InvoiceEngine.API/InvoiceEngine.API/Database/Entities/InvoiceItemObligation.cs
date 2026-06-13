namespace InvoiceEngine.API.Database.Entities;

/*
WARNING: Obligations entity
- InvoiceItems table contains InvoiceItemType (transport, sellgoods, insurance)
- InvoiceItem also has InvoiceId
- Invoices table has IncotermRule column
- IncotermRule enum can tell us who ows
 */
public class InvoiceItemObligation
{
    public int InvoiceItemId { get; set; }
    
    public InvoiceSubject FromClientSubjectCode { get; set; }
    public InvoiceSubject ToClientSubjectCode { get; set; }
    
    public decimal OwingAmount { get; set; }

    public InvoiceItem InvoiceItem { get; set; }
}
