namespace InvoiceEngine.API.Database;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) :
    DbContext(options)
{
    public DbSet<TransportCompany> TransportCompanies { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceClient> InvoiceClients { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    public DbSet<InvoiceItemOrderDetail> InvoiceItemOrderDetails { get; set; }
    public DbSet<InvoiceItemTransportDetail> InvoiceItemTransportDetails { get; set; }
    public DbSet<InvoiceItemInsuranceDetail> InvoiceItemInsuranceDetails { get; set; }
    public DbSet<InvoiceItemObligation> InvoiceItemObligations { get; set; }
    public DbSet<InvoiceCustomIncotermObligation> InvoiceCustomIncotermObligations { get; set; }
    public DbSet<InvoiceItemType> InvoiceItemTypes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Article> Articles { get; set; }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
