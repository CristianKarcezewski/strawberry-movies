using Strawberry.Models.Domain;

namespace Strawberry.Repositories.Abstract
{
    public interface IMovieService
    {
        bool Create(Movie movie);
        bool Update(Movie movie);
        bool Delete(int id);
        Movie GetById(int id);
        IQueryable<Movie> Fetch();
    }
}
