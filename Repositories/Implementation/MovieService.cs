using Microsoft.EntityFrameworkCore;
using Strawberry.Models.Domain;
using Strawberry.Repositories.Abstract;

namespace Strawberry.Repositories.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly DatabaseContext ctx;

        public MovieService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public bool Create(Movie movie)
        {
            try
            {
                ctx.Movies.Add(movie);
                ctx.SaveChanges();
                this.CreateMovieGenres(movie.GenresIds, movie.Id);
                this.CreateMovieStreamings(movie.StreamingsIds, movie.Id);
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
                Movie movie = this.GetById(id);
                if (movie == null)
                {
                    return false;
                }
                this.RemoveMovieGenres(movie.Id);
                this.RemoveMovieStreamings(movie.Id);
                ctx.Movies.Remove(movie);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IQueryable<Movie> Fetch()
        {
            try
            {
                var data = ctx.Movies.AsQueryable();
                foreach(var movie in data)
                {
                    List<string> genresNames = (from Genre genre in ctx.Genres
                                  join MovieGenre movieGenre in ctx.MoviesGenres
                                  on genre.Id equals movieGenre.GenreId
                                  where movieGenre.MovieId == movie.Id
                                  select genre.GenreName).ToList();
                    movie.GenresNames = genresNames;

                    List<string> streamingsNames = (from Streaming streaming in ctx.streamings
                                                    join MovieStreaming movieStreaming in ctx.MovieStreamings
                                                    on streaming.Id equals movieStreaming.StreamingId
                                                    where movieStreaming.MovieId == movie.Id
                                                    select streaming.StreamingName).ToList();
                    movie.StreamingNames = streamingsNames;
                }
                return data;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Movie>().AsQueryable();
            }
        }

        public Movie GetById(int id)
        {
            try
            {
                return ctx.Movies.Find(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Update(Movie movie)
        {
            try
            {
                ctx.Movies.Update(movie);
                ctx.SaveChanges();
                this.UpdateMovieGenres(movie.GenresIds,movie.Id);
                this.UpdateMovieStreamings(movie.StreamingsIds,movie.Id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void RemoveMovieGenres(int movieId)
        {
            try
            {
                ctx.MoviesGenres.RemoveRange(ctx.MoviesGenres.Where(mg => mg.MovieId == movieId));
                ctx.SaveChanges();
            }
            catch(Exception ex) { }
        }

        private void CreateMovieGenres(List<int> genres, int movieId)
        {
            try
            {
                foreach (int genreId in genres)
                {
                    MovieGenre movieGenre = new MovieGenre();
                    movieGenre.MovieId = movieId;
                    movieGenre.GenreId = genreId;

                    ctx.MoviesGenres.Add(movieGenre);
                    ctx.SaveChanges();
                }
            }
            catch(Exception ex) { }
        }

        private void UpdateMovieGenres(List<int> genres, int movieId)
        {
            this.RemoveMovieGenres(movieId);
            this.CreateMovieGenres(genres, movieId);
        }

        private void RemoveMovieStreamings(int movieId)
        {
            try
            {
                ctx.MovieStreamings.RemoveRange(ctx.MovieStreamings.Where(ms => ms.MovieId == movieId));
                ctx.SaveChanges ();
            }
            catch(Exception ex) { }
        }

        private void CreateMovieStreamings(List<int> streamingsIds, int movieId)
        {
            try
            {
                foreach(int streamingId in streamingsIds)
                {
                    MovieStreaming movieStreaming = new MovieStreaming();
                    movieStreaming.MovieId = movieId;
                    movieStreaming.StreamingId = streamingId;

                    ctx.MovieStreamings.Add(movieStreaming);
                    ctx.SaveChanges();
                }
            }
            catch(Exception ex) { }
        }

        private void UpdateMovieStreamings(List<int> streamingsIds, int movieId)
        {
            this.RemoveMovieStreamings(movieId);
            this.CreateMovieStreamings(streamingsIds, movieId);
        }
    }
}
