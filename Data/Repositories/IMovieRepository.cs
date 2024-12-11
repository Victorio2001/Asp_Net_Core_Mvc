using NomDuProjet.Models;

namespace NomDuProjet.Data.Repositories;
public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAllMoviesAsync();
    Task<Movie?> GetMovieByIdAsync(int? id);
    Task AddMovieAsync(Movie movie);
    Task UpdateMovieAsync(Movie movie);
    Task DeleteMovieAsync(int id);
    //Task<MovieGenreViewModel> SearchMoviesAsync(string searchTerm, string movieGenre);
    Task<MovieGenreViewModel> SearchMoviesWithGenresAsync(string searchTerm, string movieGenre);

    bool GetExisting(int id);
}