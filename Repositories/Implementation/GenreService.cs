using Strawberry.Models.Domain;
using Strawberry.Repositories.Abstract;

namespace Strawberry.Repositories.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly DatabaseContext ctx;

        public GenreService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public bool Create(Genre genre)
        {
            try
            {
                ctx.Genres.Add(genre);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                {
                    return false;
                }
                ctx.Genres.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IQueryable<Genre> Fetch()
        {
            try
            {
                var data = ctx.Genres.AsQueryable();
                return data;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Genre>().AsQueryable();
            }
        }

        public Genre GetById(int id)
        {
            try
            {
                return ctx.Genres.Find(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Update(Genre genre)
        {
            try
            {
                ctx.Genres.Update(genre);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
