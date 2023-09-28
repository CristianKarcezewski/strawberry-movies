namespace Strawberry.Models.Domain
{
    public class UserMovieEvaluation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int EvaluationId { get; set; }
    }
}
