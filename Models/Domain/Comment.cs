using System.ComponentModel.DataAnnotations;

namespace Strawberry.Models.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string? MovieComment { get; set; }
    }
}
