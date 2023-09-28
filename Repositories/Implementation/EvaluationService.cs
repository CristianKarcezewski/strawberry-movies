using Strawberry.Models.Domain;
using Strawberry.Repositories.Abstract;

namespace Strawberry.Repositories.Implementation
{
    public class EvaluationService : IEvaluationService
    {
        private readonly DatabaseContext ctx;

        public EvaluationService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public bool Create(Evaluation evaluation)
        {
            try
            {
                ctx.Evaluation.Add(evaluation);
                ctx.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if(data == null)
                {
                    return false;
                }
                ctx.Evaluation.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IQueryable<Evaluation> Fetch()
        {
            try
            {
                return ctx.Evaluation.AsQueryable();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Evaluation>().AsQueryable();
            }
        }

        public Evaluation GetById(int id)
        {
            try
            {
                return ctx.Evaluation.Find(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Update(Evaluation evaluation)
        {
            try
            {
                ctx.Evaluation.Update(evaluation);
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
