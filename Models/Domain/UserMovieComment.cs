namespace Strawberry.Models.Domain
{
    public class UserMovieComment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int CommentId { get; set; }
    }
}
