using DesafioTarget.Common;
using DesafioTarget.Repositories;
using DesafioTarget.Services;
using DesafioTarget.Services.Strategies;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var dataDir = Path.Combine(builder.Environment.ContentRootPath, "Data");

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
builder.Services.AddScoped<IInterestService, InterestService>();

builder.Services.AddScoped<ISaleRepository, SaleRepository>(_ => new SaleRepository(Path.Combine(dataDir, "vendas.json"))); ;
builder.Services.AddScoped<IProductRepository, ProductRepository>( _ => new ProductRepository(Path.Combine(dataDir, "estoque.json")));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowFront", policy => {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowFront");

app.MapControllers();

app.Run();
