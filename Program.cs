using Microsoft.EntityFrameworkCore;
using BetEngage.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 41))
    ));

// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Activate routes of Controllers
app.MapControllers();

app.MapGet("/", () => "Welcome");

app.Run();


