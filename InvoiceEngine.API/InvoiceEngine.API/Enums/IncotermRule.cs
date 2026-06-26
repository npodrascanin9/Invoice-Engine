namespace InvoiceEngine.API.Enums;

/// <summary>
/// Defines Incoterm rules used in invoice processing.
/// Each rule specifies how costs and responsibilities
/// are divided between buyer and seller.
/// </summary>
public enum IncotermRule
{
    /// <summary>
    /// EXW (Ex Works):
    /// - Buyer collects goods directly at the seller’s premises.
    /// - Buyer pays 100% of transportation and insurance costs.
    /// - Seller’s responsibility ends once goods are made available.
    /// </summary>
    EXW = 1000,

    /// <summary>
    /// CIF (Cost, Insurance and Freight):
    /// - Seller covers transportation and insurance up to the port of destination.
    /// - Buyer pays only for the goods themselves.
    /// - Commonly used when buyers want minimal logistics obligations.
    /// </summary>
    CIF = 2000,

    /// <summary>
    /// FOB (Free On Board):
    /// - Seller delivers goods onto the vessel at the port of shipment.
    /// - Buyer pays 100% of the goods’ value.
    /// - Transportation and insurance costs are shared equally (50/50).
    /// </summary>
    FOB = 3000,

    /// <summary>
    /// Custom:
    /// - User-defined rule for flexible obligations.
    /// - Allows manual configuration of who pays for goods,
    ///   transportation, and insurance.
    /// - Data is stored in the InvoiceCustomIncotermObligations table.
    /// </summary>
    Custom = 99
}
