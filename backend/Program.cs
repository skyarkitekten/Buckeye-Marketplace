using Microsoft.EntityFrameworkCore;
using backend;

var builder = WebApplication.CreateBuilder(args);

// Connect to the SQLite Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=buckeyemarket.db"));

// ONLY keep the essentials: Controllers and CORS
builder.Services.AddControllers();

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Direct to the point: No Swagger, No API Explorer
app.UseCors();
app.MapControllers(); 

app.Run();