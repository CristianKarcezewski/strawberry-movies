using Strawberry.Models.Domain;
using Strawberry.Repositories.Abstract;

namespace Strawberry.Repositories.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly DatabaseContext ctx;

        public CommentService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public bool Create(Comment comment)
        {
            try
            {
                ctx.Comments.Add(comment);
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
                if(data == null)
                {
                    return false;
                }
                ctx.Comments.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IQueryable<Comment> Fetch()
        {
            try
            {
                return ctx.Comments.AsQueryable();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public Comment GetById(int id)
        {
            try
            {
                return ctx.Comments.Find(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Update(Comment comment)
        {
            try
            {
                ctx.Comments.Update(comment);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            };
        }
    }
}
