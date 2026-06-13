namespace InvoiceEngine.API.Features.Invoices.Create;

internal sealed class CreateInvoiceCommandHandler(
    ApplicationDbContext context,
    IIncotermRuleObligationStrategyContext obligationStrategyContext) :
    ICommandHandler<CreateInvoiceCommand, Result<CreateInvoiceResponse>>
{
    public async Task<Result<CreateInvoiceResponse>> Handle(
        CreateInvoiceCommand command, 
        CancellationToken cancellationToken)
    {
        Invoice invoiceEntity = new()
        {
            Id = default,
            IncotermCode = command.Incoterm,
            StatusCode = InvoiceStatus.Pending,
            ExpiresAt = command.ExpiresAt,
            IssuedAt = command.IssuedAt,
            TransactionStartDate = command.TransactionStartDate,
            TransactionEndDate = command.TransactionEndDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        invoiceEntity
            .InvoiceClients
            .AddRange(new List<InvoiceClient>()
            {
                new()
                {
                    ClientId = command.ClientBuyerId,
                    SubjectCode = InvoiceSubject.Buyer
                },
                new()
                {
                    ClientId = command.ClientSellerId,
                    SubjectCode = InvoiceSubject.Seller
                }
            });

        // invoiceEntity.Items = (if not null) Sells, transport, insurance...
        if (command.SellGoodsInvoiceItemRequest is not null)
        {
            var order = await context
                .Orders
                .Include(x => x.Items)
                .FirstOrDefaultAsync(
                    x => x.Id == command.SellGoodsInvoiceItemRequest.OrderId,
                    cancellationToken);

            var price = order.Items.Sum(x => x.TotalAmount);
            var quantity = 1;

            invoiceEntity.Items.Add(new()
            {
                Id = 0,
                Quantity = quantity,
                Price = price,
                Amount = price * quantity,
                ItemTypeCode = InvoiceItemTypeCode.SellGoods,
                ItemOrderDetails = new()
                {
                    new()
                    {
                        Id = 0,
                        OrderId = order.Id
                    }
                }
            });
        }

        if (command.TransportInvoiceItemRequest is not null)
        {
            var transportCompany = await context
                .TransportCompanies
                .FirstOrDefaultAsync(x => x.Id == command.TransportInvoiceItemRequest.TransportCompanyId);

            var quantity = 1;
            var price = command.TransportInvoiceItemRequest.Price;

            invoiceEntity.Items.Add(new()
            {
                Id = 0,
                Quantity = quantity,
                Price = price,
                Amount = price * quantity,
                ItemTypeCode = InvoiceItemTypeCode.Transport,
                ItemTransportDetails = new()
                {
                    new()
                    {
                        TransportCompanyId = transportCompany.Id,
                        AddressFrom = command.TransportInvoiceItemRequest.AddressFrom,
                        AddressTo = command.TransportInvoiceItemRequest.AddressTo
                    }
                }
            });
        }

        if (command.InsuranceInvoiceItemRequest is not null)
        {
            var insuranceCompany = await context
                .InsuranceCompanies
                .FirstOrDefaultAsync(x => x.Id == command.InsuranceInvoiceItemRequest.InsuranceCompanyId);

            var quantity = 1;
            var price = command.InsuranceInvoiceItemRequest.Price;

            invoiceEntity.Items.Add(new()
            {
                Id = 0,
                Quantity = quantity,
                Price = price,
                Amount = price * quantity,
                ItemTypeCode = InvoiceItemTypeCode.Insurance,
                ItemInsuranceDetails = new()
                {
                    new()
                    {
                        InsuranceCompanyId = insuranceCompany.Id,
                        ExpiresAt = command.InsuranceInvoiceItemRequest.ExpiresAt
                    }
                }
            });
        }


        foreach (var invoiceItem in invoiceEntity.Items)
        {
            var dictionary = obligationStrategyContext.ResolveIncotermObligation(
                command.Incoterm,
                invoiceItem.ItemTypeCode,
                invoiceItem.Amount);

            var obligations = dictionary.Select(x => new InvoiceItemObligation
            {
                FromClientSubjectCode = x.Key.from,
                ToClientSubjectCode = x.Key.to,
                OwingAmount = x.Value
            }).ToList();

            if (obligations?.Any() ?? false)
            {
                invoiceItem.ItemObligations.AddRange(
                    obligations);
            }
        }

        await context.Invoices.AddAsync(invoiceEntity);
        await context.SaveChangesAsync();

        return Result.Success<CreateInvoiceResponse>(
            new(invoiceEntity.Id));
    }
}
