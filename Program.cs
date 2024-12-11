using System.Globalization;
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

app.Run();
