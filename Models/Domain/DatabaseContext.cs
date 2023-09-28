using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Strawberry.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<UserModel>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Streaming> streamings { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Evaluation> Evaluation { get; set; }
        public DbSet<MovieGenre> MoviesGenres { get; set;}
        public DbSet<MovieStreaming> MovieStreamings { get; set; }
        public DbSet<UserMovieComment> UserMovieComments { get; set; }
        public DbSet<UserMovieEvaluation> UserMovieEvaluation { get; set; }
    }
}
