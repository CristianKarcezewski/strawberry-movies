using Strawberry.Models.Domain;

namespace Strawberry.Repositories.Abstract
{
    public interface IEvaluationService
    {
        bool Create(Evaluation evaluation);
        bool Update(Evaluation evaluation);
        bool Delete(int id);
        Evaluation GetById(int id);
        IQueryable<Evaluation> Fetch();
    }
}
