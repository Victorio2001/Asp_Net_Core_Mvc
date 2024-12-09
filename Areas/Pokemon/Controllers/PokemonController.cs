using Microsoft.AspNetCore.Mvc;

namespace NomDuProjet.Areas.Pokemon.Controllers;

[Area("Pokemon")]
public class PokemonController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}