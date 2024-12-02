using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NomDuProjet.Data;
using NomDuProjet.Models;
using NomDuProjet.Models.SeedData;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NomDuProjetContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NomDuProjetContext") ?? throw new InvalidOperationException("Connection string 'NomDuProjetContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

//! Nôtre point de Spawn.
app.MapControllerRoute(
    name: "default",
    //[Controller]/[ActionName]/[Parameters]
    pattern: "{controller=HelloWorld}/{action=Index}/{id?}"); //? Le ? de fin (dans id?) indique que le paramètre id est facultatif.

app.Run();
