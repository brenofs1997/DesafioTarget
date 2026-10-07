using DesafioTarget.Repositories;
using DesafioTarget.Services;
using DesafioTarget.Services.Strategies;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var dataDir = Path.Combine(AppContext.BaseDirectory, "Data");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICommissionStrategy, NoCommissionStrategy>();
builder.Services.AddScoped<ICommissionStrategy, OnePercentCommissionStrategy>();
builder.Services.AddScoped<ICommissionStrategy, FivePercentCommissionStrategy>();

builder.Services.AddScoped<ICommissionService, CommissionService>();
builder.Services.AddScoped<IStockService, StockService>();

builder.Services.AddScoped<ISaleRepository, SaleRepository>(_ => new SaleRepository(Path.Combine(dataDir, "vendas.json"))); ;
builder.Services.AddScoped<IProductRepository, ProductRepository>( _ => new ProductRepository(Path.Combine(dataDir, "estoque.json")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
