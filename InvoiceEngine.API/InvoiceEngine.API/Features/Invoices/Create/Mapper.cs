namespace InvoiceEngine.API.Features.Invoices.Create;

public static class CreateInvoiceMapper
{
    public static bool CanHaveCustomIncotermObligations(
        this CreateInvoiceCommand command)
    {
        bool isCustomIncoterm = command.Incoterm == IncotermRule.Custom;
        bool hasCustomDictionary = command.CustomIncotermRules is not null;
        return isCustomIncoterm && hasCustomDictionary;
    }
}