using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NomDuProjet.Models;

using NomDuProjet.Data;
using NomDuProjet.Data.Repositories;

namespace NomDuProjet.Controllers.Movie
{
    public class MovieController : Controller
    {
        //! inutile puisque gérer dans le repo
        private readonly NomDuProjetContext _context;
        
        private readonly IMovieRepository _movieRepository;

        
        //! Le contexte de base de données est utilisé dans chacune des méthodes la CRUD du contrôleur.
        public MovieController(NomDuProjetContext context, IMovieRepository movieRepository)
        {
            _context = context;
            _movieRepository = movieRepository;
        }
        
        [HttpPost]
        public string Index(string searchTerm)
        {
            Console.WriteLine($"{searchTerm}");
            return "From [HttpPost]Index: filter on " + searchTerm;
        }

        // GET: Movie
        [HttpGet]
        public async Task<IActionResult> Index(string searchTerm, string movieGenre)
        {
            
            var chartData = new
            {
                Datasets = new List<object>
                {
                    new
                    {
                        Label = "Population Data",
                        Data = new List<object>
                        {
                            new { x = 10, y = 20, r = 15 },
                            new { x = 15, y = 10, r = 10 },
                            new { x = 20, y = 30, r = 20 },
                            new { x = 25, y = 25, r = 25 },
                            new { x = 30, y = 15, r = 30 }
                        },
                        BackgroundColor = new List<string>
                        {
                            "rgba(255, 99, 132, 0.6)",
                            "rgba(54, 162, 235, 0.6)",
                            "rgba(255, 206, 86, 0.6)",
                            "rgba(75, 192, 192, 0.6)",
                            "rgba(153, 102, 255, 0.6)"
                        },
                        BorderColor = "rgba(255, 255, 255, 1)",
                        BorderWidth = 1
                    }
                }
            };

            ViewData["BubbleChartData"] = chartData;  
           
            Console.WriteLine($" Controller Recherche pour : {searchTerm}");
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie' is null.");
            }
            

            var movieGenreVM  = await _movieRepository.SearchMoviesWithGenresAsync(searchTerm, movieGenre);
            return View(movieGenreVM);
        }

        // GET: Movie/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            // Vérifie si l'ID est null
            if (id == null)
            {
                return NotFound(); // Retourne une erreur 404
            }

            // Appelle le repository pour récupérer le film
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound(); // Retourne une erreur 404 si le film n'existe pas
            }

            // Retourne la vue avec le film
            return View(movie);
        }

        // GET: Movie/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Models.Movie movie)
        //! Vous devez inclure dans l’attribut [Bind] uniquement les propriétés que vous souhaitez modifier.
        {
            if (ModelState.IsValid)
            {
                await _movieRepository.AddMovieAsync(movie);  
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        
        // GET: Movie/Edit/5
        [HttpGet] //! Vous pouvez appliquer l’attribut [HttpGet] à la première méthode Edit, mais cela n’est pas nécessaire car [HttpGet] est la valeur par défau
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //! utilisé pour lutter contre la falsification d’une requête. Il est associé à un jeton anti-falsification généré dans le fichier de la vue de modification
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Models.Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid) //! La propriété ModelState.IsValid vérifie que les données envoyées dans le formulaire peuvent être utilisées pour changer (modifier ou mettre à jour) un objet Movie
            {
                try
                {
                    await _movieRepository.UpdateMovieAsync(movie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movieRepository.DeleteMovieAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _movieRepository.GetExisting(id);
        }
    }
}
