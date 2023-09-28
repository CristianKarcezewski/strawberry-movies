using Strawberry.Models.Domain;
using Strawberry.Repositories.Abstract;

namespace Strawberry.Repositories.Implementation
{
    public class StreamingService : IStreamingService
    {
        private readonly DatabaseContext ctx;

        public StreamingService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public bool Create(Streaming streaming)
        {
            try
            {
                ctx.streamings.Add(streaming);
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
                if(data==null)
                {
                    return false;
                }
                ctx.streamings.Remove(data);
                ctx.SaveChanges();
                return true; 
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IQueryable<Streaming> Fetch()
        {
            try
            {
                return ctx.streamings.AsQueryable();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Streaming>().AsQueryable();
            }
        }

        public Streaming GetById(int id)
        {
            try
            {
                return ctx.streamings.Find(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Update(Streaming streaming)
        {
            try
            {
                ctx.streamings.Update(streaming);
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
