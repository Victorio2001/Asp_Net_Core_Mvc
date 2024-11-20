using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers;

public class HelloWorldController : Controller
{
    /// <summary>
    /// GET: /HelloWorld/
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        // !exécuté l’instruction return View();, laquelle spécifiait que la méthode doit utiliser un fichier de modèle de vue
        // ?Dans cet exemple, la vue par défaut porte le même nom que la méthode d’action Index. Le modèle de vue /Views/HelloWorld/Index.cshtml est utilisé.
        return View();
    }

    /// <summary>
    /// GET: /HelloWorld/Welcome?nom=Rick&nombre=4.
    /// GET: /HelloWorld/Welcome
    /// </summary>
    /// <param name="nom"></param>
    /// <param name="nombre"></param>
    /// <returns></returns>
    public IActionResult Welcome(string nom = "victorio", int nombre = 1) //Defult value
    {
        ViewData["nombre"] = nombre;
        ViewData["Message"] = "Welcome " + nom + " " + nombre;
        //! Utilise HtmlEncoder.Default.Encode pour protéger l’application contre les entrées malveillantes, par exemple via JavaScript.
        var date = DateTime.Now;
        //return HtmlEncoder.Default.Encode($"Salut {nom}, Numero: {nombre}, il est {date}");
        return View();
    }
}