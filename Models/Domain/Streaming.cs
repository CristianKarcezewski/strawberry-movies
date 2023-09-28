using System.ComponentModel.DataAnnotations;

namespace Strawberry.Models.Domain
{
    public class Streaming
    {
        public int Id { get; set; }
        [Required]
        public string? StreamingName { get; set; }
    }
}
