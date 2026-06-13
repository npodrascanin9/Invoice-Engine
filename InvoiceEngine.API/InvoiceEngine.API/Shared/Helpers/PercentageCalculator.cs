namespace InvoiceEngine.API.Shared.Helpers;

public static class PercentageCalculator
{
    public static decimal ApplyPercentage(
        decimal amount, 
        decimal percentage)
    {
        return amount * (percentage / 100m);
    }
}
