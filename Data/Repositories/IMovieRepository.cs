using NomDuProjet.Models;

namespace NomDuProjet.Data.Repositories;
public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAllMoviesAsync();
    Task<Movie?> GetMovieByIdAsync(int? id);
    Task AddMovieAsync(Movie movie);
    Task UpdateMovieAsync(Movie movie);
    Task DeleteMovieAsync(int id);
    Task<IEnumerable<Movie>> SearchMoviesAsync(string searchTerm);
    bool GetExisting(int id);
}