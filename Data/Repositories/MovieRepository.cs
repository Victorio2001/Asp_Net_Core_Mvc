using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NomDuProjet.Models;

namespace NomDuProjet.Data.Repositories;

public class MovieRepository : IMovieRepository
{
        private readonly NomDuProjetContext _context;

        public MovieRepository(NomDuProjetContext context)
        {
            _context = context;
        }

        public async Task<MovieGenreViewModel> SearchMoviesWithGenresAsync(string searchTerm, string movieGenre)
        {
            
            IQueryable<string> genreQuery = _context.Movie
                .OrderBy(m => m.Genre)
                .Select(m => m.Genre);
            
            IQueryable<Movie> moviesQuery = _context.Movie;

            //!-------
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                moviesQuery = moviesQuery.Where(m => m.Title != null && m.Title.ToUpper().Contains(searchTerm.ToUpper()));
            }
            if (!string.IsNullOrWhiteSpace(movieGenre))
            {
                moviesQuery = moviesQuery.Where(m => m.Genre == movieGenre);
            }
            //!-------

            // Construction du ViewModel
            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await moviesQuery.ToListAsync()
            };

            return movieGenreVM;
        }




        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movie.ToListAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(int? id)
        {
            //!Cette méthode effectue une requête LINQ sur la base de données en fonction du prédicat spécifié (ici m => m.Id == id).
            //return await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            
            
            //!Cette méthode utilise le change tracker d'Entity Framework Core pour rechercher l'entité dans le contexte de suivi (cache) avant de faire une requête à la base de données.
            return await _context.Movie.FindAsync(id);
        }

        public async Task AddMovieAsync(Movie movie)
        {
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            _context.Movie.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }

        public bool GetExisting(int id)
        {
            return  _context.Movie.Any(e => e.Id == id);
        }
}