using Basket.Api.Repositries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetValue<string>("CashSettings:ConnectionStrings");
});
builder.Services.AddScoped<IBasketRepositry, BasketRepositry>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
