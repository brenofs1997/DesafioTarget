using DesafioTarget.Services;
using DesafioTarget.Services.Strategies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICommissionStrategy, NoCommissionStrategy>();
builder.Services.AddScoped<ICommissionStrategy, OnePercentCommissionStrategy>();
builder.Services.AddScoped<ICommissionStrategy, FivePercentCommissionStrategy>();

builder.Services.AddScoped<CommissionService>();

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
