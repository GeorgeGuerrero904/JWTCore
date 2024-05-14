using JWTCore.Models;
using JWTCore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<AuthService>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.PrivateKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("tech", p => p.RequireRole("developer"));
});
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", (AuthService service) =>
{
    var user = new User(
        1,
        "bruno.bernardes",
        "Bruno Bernardes",
        "bruno@gmail.com",
        "q1w2e3r4t5",
        ["developer"]);

    return service.Create(user);
});

app.MapGet("/test", () => "OK!")
    .RequireAuthorization();

app.MapGet("/test/tech", () => "tech OK!")
    .RequireAuthorization("tech");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
