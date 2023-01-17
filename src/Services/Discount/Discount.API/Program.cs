using Discount.API.Repositrise;



var builder = WebApplication.CreateBuilder(args);
//builder.MigrateDatabase<Program>();
//builder.Run();
// Add services to the container.
builder.Services.AddScoped<IDiscountRepositry, DiscountRepositry>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.AddHealthChecks()
//               .AddNpgSql(Configuration["DatabaseSettings:ConnectionString"]);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
