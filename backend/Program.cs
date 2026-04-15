using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // Added
using Microsoft.AspNetCore.Authentication.JwtBearer; // Added
using Microsoft.IdentityModel.Tokens; // Added
using System.Text; // Added
using backend;

var builder = WebApplication.CreateBuilder(args);

// Connect to the SQLite Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=buckeyemarket.db"));

// --- MILESTONE 5 SECURITY ADDITIONS ---
builder.Services.AddIdentityCore<IdentityUser>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "A_Very_Long_Temporary_Key_For_Testing_123!");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();
// ---------------------------------------

builder.Services.AddControllers();

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseCors();

// --- ADD THESE TWO LINES ---
app.UseAuthentication(); 
app.UseAuthorization();
// ---------------------------

app.MapControllers(); 

app.Run();