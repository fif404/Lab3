using Lab3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ModelDB>(options => options.UseSqlServer(connection));
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapPost("/login", async (User loginData, ModelDB db) =>
{
    User? person = await db.Users.FirstOrDefaultAsync(p => p.Email == loginData.Email && p.Password == loginData.Password);
    if (person is null) return Results.Unauthorized();

    var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Email!) };
    var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encodedJwt,
        username = person.Email
    };

    return Results.Json(response);
});
app.MapGet("api/stoks", [Authorize] async (ModelDB db) => await db.Stoks.ToListAsync());
app.MapGet("api/stoks/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Stoks? stoks = await db.stoks.FirstOrDefaultAsync(x => x.Id == id);
    if (stoks == null) return Results.NotFound(new { message = "stoks not found" });
    return Results.Json(stoks);
});
app.MapGet("api/collectors", [Authorize] async (ModelDB db) => await db.Collectors.ToListAsync());
app.MapGet("api/collectors/select/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Collectors? collectors = await db.Collectors.FirstOrDefaultAsync(x => x.Id == id);
    if (collectors == null) return Results.NotFound(new { message = "Collectors not found" });
    return Results.Json(collectors);
});
app.MapGet("api/collectors/select/{data}", [Authorize] async (string data, ModelDB db) =>
{
    List<Collectors> collectors = await db.Collectors.Where(x => x.DateCollector == DateTime.Parse(data)).ToListAsync();
    return collectors;
});
app.MapPost("/api/collectors", [Authorize] async (Collectors collector, ModelDB db) =>
{
    await db.Collectors.AddAsync(collector);
    await db.SaveChangesAsync();
    return collector;
});
app.MapPost("/api/stoks", [Authorize] async (Stoks stok, ModelDB db) =>
{
    await db.Stoks.AddAsync(stok);
    await db.SaveChangesAsync();
    return stok;
});
app.MapDelete("api/collector/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Collectors? collector = await db.Collectors.FirstOrDefaultAsync(x => x.Id == id);
    if (collector == null) return Results.NotFound(new { message = "Collector not found" });
    db.Collectors.Remove(collector);
    await db.SaveChangesAsync();
    return Results.Json(collector);
});
app.MapDelete("api/stoks/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Stoks? stok = await db.Stoks.FirstOrDefaultAsync(x => x.Id == id);
    if (stok == null) return Results.NotFound(new { message = "User not found" });
    db.Stoks.Remove(price);
    await db.SaveChangesAsync();
    return Results.Json(stok);
});
app.MapPut("/api/collector", [Authorize] async (Collectors collectorData, ModelDB db) =>
{
    Collectors? collector = await db.Collectors.FirstOrDefaultAsync(u => u.Id == collectorData.Id);
    if (collector == null) return Results.NotFound(new { message = "Collector not found" });
    collector.Full_name = collectorData.Full_name;
    collector.Name = collectorData.Name;
    collector.Share = collectorData.Share;
    collector.Number = collectorData.Number;
    collector.Date = collectorData.Date;
    collector.Accruals = collectorData.Accruals;
    await db.SaveChangesAsync();
    return Results.Json(collector);
});
app.MapPut("/api/Stoks", [Authorize] async (Stoks stokData, ModelDB db) =>
{
    Stoks? stok = await db.Stoks.FirstOrDefaultAsync(u => u.Id == stokData.Id);
    if (stok == null) return Results.NotFound(new { message = "Stok not found" });
    stok.Name = stokData.Name;
    stok.Cost = stokData.Cost;
    stok.Dividends = stokData.Dividends;
    await db.SaveChangesAsync();
    return Results.Json(stok);
});
app.Run();