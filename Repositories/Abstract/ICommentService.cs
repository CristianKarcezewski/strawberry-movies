using Strawberry.Models.Domain;

namespace Strawberry.Repositories.Abstract
{
    public interface ICommentService
    {
        bool Create(Comment comment);
        bool Update(Comment comment);
        bool Delete(int id);
        Comment GetById(int id);
        IQueryable<Comment> Fetch();
    }
}
