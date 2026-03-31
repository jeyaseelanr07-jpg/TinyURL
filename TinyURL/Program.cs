using TinyURL.Components;
using TinyURL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 1. REGISTER THE SERVICE AS SCOPED
// This allows the service to use the HttpClient registered below
builder.Services.AddScoped<UrlService>();

// 2. CONFIGURE HTTPCLIENT
// Ensure this port matches your Backend API's running port
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7269/")
});

// 3. CORS POLICY
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// 4. MIDDLEWARE ORDER
// UseCors must be here if you are handling API calls in this project
app.UseCors();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

// Static assets for Blazor (CSS/JS)
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();