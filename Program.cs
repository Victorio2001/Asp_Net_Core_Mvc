using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NomDuProjet.Data;
using NomDuProjet.Data.Repositories;
using NomDuProjet.Models;
using NomDuProjet.Models.SeedData;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NomDuProjetContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NomDuProjetContext") ?? throw new InvalidOperationException("Connection string 'NomDuProjetContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix); //! en; fr; es; etc.
//!Localisation du dossier Resources
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    //? nos langages supportés
    var supportedCutures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("fr-FR")
    };
    //! langue par default
    options.DefaultRequestCulture = new RequestCulture("fr-FR");
    options.SupportedCultures = supportedCutures;
    options.SupportedUICultures = supportedCutures;
});
 
builder.Services.AddScoped<IMovieRepository, MovieRepository>();


builder.Services.AddDefaultIdentity<IdentityUser>(options => 
        options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<NomDuProjetContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});


var app = builder.Build();
app.UseRequestLocalization();

//! fixtures || SeedData
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}
//! fixtures || SeedData

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//? Nôtre point de Spawn-Default
app.MapControllerRoute(
    name: "default",
    //[Controller]/[ActionName]/[Parameters]
    pattern: "{controller=Movie}/{action=Index}/{id?}"); //? Le ? de fin (dans id?) indique que le paramètre id est facultatif.

//! Nôtre point de Spawn-AreaPokemon  --  https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/areas?view=aspnetcore-9.0#area-folder-structure
app.MapAreaControllerRoute(
    name: "MyAreaProducts",
    areaName: "Pokemon",
    pattern: "Pokemon/{controller=Pokemon}/{action=Index}/{id?}");


app.MapRazorPages();
app.Run();
