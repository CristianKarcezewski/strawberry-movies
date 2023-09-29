using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Strawberry.Models.Domain
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? ReleaseYear { get; set; }
        [Required]
        public string? ReleaseMonth { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }

        [NotMapped]
        [Required]
        public List<int>? GenresIds { get; set; }
        [NotMapped]
        [Required]
        public List<int>? StreamingsIds { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? Genres { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? Streamings { get; set; }
        [NotMapped]
        public List<String>? GenresNames { get; set; }
        [NotMapped]
        public List<String>? StreamingNames { get; set; }

        public string GenresToString()
        {
             return GenresNames != null ? string.Join(", ", GenresNames) : string.Empty;
        }

        public string StreamingsToString()
        {
            return StreamingNames != null ? string.Join(", ", StreamingNames) : string.Empty;
        }
    }
}
