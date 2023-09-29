using Microsoft.AspNetCore.Mvc.Rendering;
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

        public List<Movie> Fetch(string search = "")
        {
            try
            {
                List<Movie> data = ctx.Movies.AsQueryable().ToList();
                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();
                    data = data.Where(m => m.Title.ToLower().Contains(search)).ToList();
                }
                foreach(Movie movie in data)
                {
                    movie.GenresIds = new List<int>();
                    movie.GenresNames = new List<string>();
                    movie.StreamingsIds = new List<int>();
                    movie.StreamingNames = new List<string>();

                    List<Genre> genresData = (from Genre genre in ctx.Genres
                                  join MovieGenre movieGenre in ctx.MoviesGenres
                                  on genre.Id equals movieGenre.GenreId
                                  where movieGenre.MovieId == movie.Id
                                  select genre).ToList();

                    genresData.ForEach(g =>
                    {
                        if (g.Id > 0) { movie.GenresIds.Add(g.Id); }
                        if (!string.IsNullOrEmpty(g.GenreName)) { movie.GenresNames.Add(g.GenreName); }
                    });

                    List<Streaming> streamingsData = (from Streaming streaming in ctx.streamings
                                                    join MovieStreaming movieStreaming in ctx.MovieStreamings
                                                    on streaming.Id equals movieStreaming.StreamingId
                                                    where movieStreaming.MovieId == movie.Id
                                                    select streaming).ToList();
                    streamingsData.ForEach(s =>
                    {
                        if (s.Id > 0) { movie.StreamingsIds.Add(s.Id); }
                        if (!string.IsNullOrEmpty(s.StreamingName)) { movie.StreamingNames.Add(s.StreamingName); }
                    });
                }
                return data;
            }
            catch (Exception ex)
            {
                return new List<Movie>();
            }
        }

        public Movie GetById(int id)
        {
            try
            {
                Movie movie = ctx.Movies.Find(id);
                movie.GenresIds = new List<int>();
                movie.GenresNames = new List<string>();
                movie.StreamingsIds = new List<int>();
                movie.StreamingNames = new List<string>();

                List<Genre> genresData = (from Genre genre in ctx.Genres
                                          join MovieGenre movieGenre in ctx.MoviesGenres
                                          on genre.Id equals movieGenre.GenreId
                                          where movieGenre.MovieId == movie.Id
                                          select genre).ToList();

                genresData.ForEach(g =>
                {
                    if (g.Id > 0) { movie.GenresIds.Add(g.Id); }
                    if (!string.IsNullOrEmpty(g.GenreName)) { movie.GenresNames.Add(g.GenreName); }
                });

                List<Streaming> streamingsData = (from Streaming streaming in ctx.streamings
                                                  join MovieStreaming movieStreaming in ctx.MovieStreamings
                                                  on streaming.Id equals movieStreaming.StreamingId
                                                  where movieStreaming.MovieId == movie.Id
                                                  select streaming).ToList();
                streamingsData.ForEach(s =>
                {
                    if (s.Id > 0) { movie.StreamingsIds.Add(s.Id); }
                    if (!string.IsNullOrEmpty(s.StreamingName)) { movie.StreamingNames.Add(s.StreamingName); }
                });

                return movie;
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
