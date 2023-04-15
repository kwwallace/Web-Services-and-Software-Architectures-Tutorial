using System.Collections.Generic;
using MovieApi.Models;


namespace MovieApi.Repository
{
    public class MovieRepository : IMovieRepository {
        private static readonly List<Movie> movies = new List<Movie>(10)
        {
            new Movie {Name = "Citizen Kane", Genre = "Drama", Year = 1941},
            new Movie {Name = "The Wizard of Oz", Genre = "Fantasy", Year = 1939},
            new Movie {Name = "The Godfather", Genre = "Crime", Year = 1972}
        };

        public MovieRepository() { 
            
        }

        public IEnumerable<Movie> GetAll() { return movies; }

        public Movie? GetMovieByName(string name) { 
            foreach (Movie m in movies){
                if (m.Name.Equals(name)){
                    return m;
                };
            }
            return null;
            
        }

        public void InsertMovie(Movie m) {
            movies.Add(m);
        }

        public void UpdateMovie(string name, Movie movieIn) {
            foreach (Movie m in movies){
                if (m.Name.Equals(name)){
                    m.Name = movieIn.Name;
                    m.Genre = movieIn.Genre;
                    m.Year = movieIn.Year;
                };
            }
        }

        public void DeleteMovie(string name) {
            foreach (Movie m in movies){
                if(m.Name.Equals(name)){
                    movies.Remove(m);
                }
            }
        }
    }
}
