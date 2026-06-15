namespace InvoiceEngine.API.Database;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) :
    DbContext(options)
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceClient> InvoiceClients { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    public DbSet<InvoiceItemObligation> InvoiceItemObligations { get; set; }
    public DbSet<InvoiceCustomIncotermObligation> InvoiceCustomIncotermObligations { get; set; }
    public DbSet<InvoiceItemType> InvoiceItemTypes { get; set; }


    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
