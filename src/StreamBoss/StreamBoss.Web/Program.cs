using StreamBoss.Web;
using StreamBoss.Web.Components;
using StreamBoss.Web.Services;
using StreamBoss.Web.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();
builder.Services.AddHttpClient<WeatherApiClient>(client=> client.BaseAddress = new("http://apiservice"));
builder.Services.AddHttpClient<IApiClient, ApiClient>("Api", client => client.BaseAddress = new("http://apiservice"));
builder.Services.AddScoped<IApiClient, ApiClient>();
builder.Services.AddScoped<ShowSearchViewModel>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();

app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
    
app.MapDefaultEndpoints();

app.Run();
