using Strawberry.Models.Domain;

namespace Strawberry.Repositories.Abstract
{
    public interface IGenreService
    {
        bool Create(Genre genre);
        bool Update(Genre genre);
        bool Delete(int id);
        Genre GetById(int id);
        IQueryable<Genre> Fetch();
    }
}
