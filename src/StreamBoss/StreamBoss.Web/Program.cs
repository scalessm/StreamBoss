using StreamBoss.Web;
using StreamBoss.Web.Components;
using StreamBoss.Web.Services;
using StreamBoss.Web.ViewModels;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAd")
    .EnableTokenAcquisitionToCallDownstreamApi(new[] { "user.read" })
    .AddInMemoryTokenCaches();


builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});
//builder.Services.AddControllersWithViews()
//    .AddMvcOptions(options =>
//    {
//        var policy = new AuthorizationPolicyBuilder()
//                      .RequireAuthenticatedUser()
//                      .Build();
//        options.Filters.Add(new AuthorizeFilter(policy));
//    })
//    .AddMicrosoftIdentityUI();

// Add services to the container.
builder.Services.AddRazorComponents(options =>
{
    
})
    .AddInteractiveServerComponents();



builder.Services.AddHttpClient();
builder.Services.AddHttpClient<WeatherApiClient>(client=> client.BaseAddress = new("http://apiservice"));
builder.Services.AddHttpClient<IApiClient, ApiClient>("Api", client => client.BaseAddress = new("http://apiservice"));
builder.Services.AddScoped<IApiClient, ApiClient>();
builder.Services.AddScoped<ShowSearchViewModel>();
builder.Services.AddScoped<HomeViewModel>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

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
