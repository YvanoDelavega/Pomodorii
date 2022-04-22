using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Pomodorii.Api;
using Pomodorii.Api.Models;
using Pomodorii.Models;
using Pomodorii.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// base de données à utiliser
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite("Data Source=pomodorii.db"));

// fourni les instances utiles à l'application
builder.Services.AddScoped<ITomateRepository, TomateRepository>();
builder.Services.AddHttpClient<ITomateService, TomateService>();
builder.Services.AddScoped<ISemiRepository, SemiRepository>();
builder.Services.AddHttpClient<ISemiService, SemiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
// sans cette ligne une erreur blazor apparait 
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");



// Initialize the database
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Database.EnsureCreated())
    {
      // todo
    }
}

app.Run();
