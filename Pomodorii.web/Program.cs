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
//builder.Services.AddDbContext<AppDbContext>(options =>
//        options.UseSqlite("Data Source=pomodorii.db"));
//builder.Services.AddDbContext<AppDbContext>(options =>
//        //options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));
//        options.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=PomodoriiDB;Trusted_Connection=true", b => b.MigrationsAssembly("Pomodorii.web")));


// https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql
// pour generer les classes (scaffold) à partir de la base mysql :
// installer les outils https://docs.microsoft.com/en-us/ef/core/cli/dotnet (rq je pense qu'il n'y a pas besoin des outils si on est dans la « console du gestionnaire de package 
// se placer dans le repertoire de l'api
// executer : dotnet ef dbcontext scaffold "server=127.0.0.1;port=3306;user=root;password=root;database=easypingplus" "Pomelo.EntityFrameworkCore.MySql"

// https://www.pragimtech.com/blog/blazor/asp.net-core-rest-api-dbcontext/
// pour générer la bd à partir des classes
// Il faut se mettre obligatoirement dans la « console du gestionnaire de package » et choisir « Pomodorii.Api » en projet par défaut
//Le projet de démarrage de la solution est « Pomodorii.web »
// ensuiite faire :
// Add-Migration InitialCreate
// Update-Database



// Replace with your connection string.
//var connectionString = "mysql://root:root@127.0.0.1:3306/easypingplus"; //"server=localhost;user=root;password=1234;database=ef";
var connectionString = builder.Configuration["DBConnection"];//.GetConnectionString("DBConnection");
//var connectionString = builder.Configuration.GetConnectionString("DBConnectionMySQL");
//var connectionString = "server=127.0.0.1;port=3306;user=root;password=root;database=easypingplus";
//var connectionString = "server=oliadkuxrl9xdugh.chr7pe7iynqr.eu-west-1.rds.amazonaws.com;port=3306;user=ye4z4u7mdlu8enwh;password=biql1dm5jnz2jlcg;database=c084srtqiisxknq4";
//dotnet easypingplus dbcontext scaffold "server=127.0.0.1;port=3306;user=root;password=root;database=easypingplus"; "Pomelo.EntityFrameworkCore.MySql"
// Replace with your server version and type.
// Use 'MariaDbServerVersion' for MariaDB.
// Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
// For common usages, see pull request #1233.
var serverVersion = ServerVersion.AutoDetect(connectionString); //new MySqlServerVersion(new Version(8, 0, 27));

// Replace 'YourDbContext' with the name of your own DbContext derived class.
builder.Services.AddDbContext<AppDbContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion)//, b => b.MigrationsAssembly("Pomodorii.web"))
        // The following three options help with debugging, but should
        // be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());


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
