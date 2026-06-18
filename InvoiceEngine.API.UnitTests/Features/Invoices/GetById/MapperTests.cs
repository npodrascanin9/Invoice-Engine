namespace InvoiceEngine.API.UnitTests.Features.Invoices.GetById;

public class GetInvoiceByIdMapperTests : BaseUnitTest
{
    [Test]
    public void MapItemsForClient_ShouldReturnItems()
    {
        // Arrange
        var query = new GetInvoiceByIdQuery(Id: 1);
        var items = new List<InvoiceItem>
        {
            new InvoiceItem
            {
                Id = 10,
                ItemTypeCode = InvoiceItemTypeCode.SellGoods,
                ItemObligations = new List<InvoiceItemObligation>
                {
                    new InvoiceItemObligation
                    {
                        FromClientSubjectCode = InvoiceSubject.Buyer,
                        ToClientSubjectCode = InvoiceSubject.Seller,
                        OwingAmount = 200m
                    },
                    new InvoiceItemObligation
                    {
                        FromClientSubjectCode = InvoiceSubject.Seller,
                        ToClientSubjectCode = InvoiceSubject.Buyer,
                        OwingAmount = 300m
                    }
                }
            }
        };

        // Act
        var result = query.MapItemsForClient(
            InvoiceSubject.Buyer, 
            items);

        // Assert
        result.Should()
            .HaveCount(1);
        result[0].Id.Should()
            .Be(10);
        result[0].ItemTypeCode.Should()
            .Be(InvoiceItemTypeCode.SellGoods);
        result[0].Amount.Should()
            .Be(200m);
        result[0].PaysTo.Should()
            .Be(InvoiceSubject.Seller.ToString());
    }
}
