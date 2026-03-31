using Microsoft.EntityFrameworkCore;
using TinyUrlAPI.Data;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{   
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlite("Data Source=shortener.db"));
}
else
{    
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(connectionString));
}

var secretToken = builder.Configuration["SecretToken"];

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options => options.AddDefaultPolicy(p =>
    p.WithOrigins("https://tinyurlapi-cbakhbcsdte2fsbd.centralindia-01.azurewebsites.net") 
     .AllowAnyMethod()
     .AllowAnyHeader()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors();

// --- API ROUTES ---

app.MapGet("/api/links", async (AppDbContext db) =>
    await db.ShortUrls.Where(u => !u.IsPrivate).ToListAsync());

app.MapPost("/api/shorten", async (UrlRequest req, AppDbContext db) =>
{
    if (string.IsNullOrEmpty(req.Url)) return Results.BadRequest();

    string code = Guid.NewGuid().ToString("N").Substring(0, 6);

    var entry = new ShortUrl
    {
        OriginalUrl = req.Url,
        ShortCode = code,
        IsPrivate = req.IsPrivate
    };

    db.ShortUrls.Add(entry);
    await db.SaveChangesAsync();
    return Results.Created($"/api/links/{entry.Id}", entry);
});

// --- THE REDIRECT LOGIC ---
app.MapGet("/{code}", async (string code, AppDbContext db) =>
{
    var entry = await db.ShortUrls.FirstOrDefaultAsync(u => u.ShortCode == code);
    if (entry == null) return Results.NotFound("Link not found.");

    entry.ClickCount++;
    await db.SaveChangesAsync();

    var target = entry.OriginalUrl.StartsWith("http") ? entry.OriginalUrl : $"https://{entry.OriginalUrl}";
    return Results.Redirect(target);
});

app.Run();