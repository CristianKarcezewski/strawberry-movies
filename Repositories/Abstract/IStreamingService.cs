using Strawberry.Models.Domain;

namespace Strawberry.Repositories.Abstract
{
    public interface IStreamingService
    {
        bool Create(Streaming streaming);
        bool Update(Streaming streaming);
        bool Delete(int id);
        Streaming GetById(int id);
        IQueryable<Streaming> Fetch();
    }
}
