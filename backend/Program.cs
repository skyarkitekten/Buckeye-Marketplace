using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using backend;

var builder = WebApplication.CreateBuilder(args);

// 1. Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=buckeyemarket.db"));

// 2. Identity Configuration (Milestone 5)
builder.Services.AddIdentityCore<IdentityUser>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true; // Added to meet rubric: "at least one uppercase letter"
    options.Password.RequiredLength = 8;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();

// 3. Authentication & JWT (Milestone 5)
// No fallback string here — strictly pulling from configuration/secrets
var jwtKey = builder.Configuration["Jwt:Key"]; 
if (string.IsNullOrEmpty(jwtKey))
{
    // This helps you troubleshoot: if the app crashes, you forgot to run 'dotnet user-secrets set'
    throw new Exception("JWT Key is missing! Run 'dotnet user-secrets set \"Jwt:Key\" \"YOUR_KEY\"' in the backend folder.");
}

var key = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options => {
    // Ensuring Admin role policy is ready for Section 5 of the rubric
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});

builder.Services.AddControllers();

// 4. CORS Strategy
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseCors();

// 5. Middleware Pipeline (Order Matters!)
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers(); 

app.Run();