var assembly = typeof(Program).Assembly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddSqlDatabaseConfiguration(builder.Configuration)
    .AddMediatrConfiguration(assembly)
    .AddValidatorsFromAssembly(assembly)
    .AddFeatures()
    .AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarter();

app.Run();
