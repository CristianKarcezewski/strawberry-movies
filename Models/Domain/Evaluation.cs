using System.ComponentModel.DataAnnotations;

namespace Strawberry.Models.Domain
{
    public class Evaluation
    {
        public int Id { get; set; }
        [Required]
        public int MovieEvaluation { get; set; }
    }
}
