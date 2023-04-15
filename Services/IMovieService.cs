using System.Collections.Generic;
using MovieApi.Models;

namespace MovieApi.Services {
    public interface IMovieService {
        public IEnumerable<Movie> GetMovies();
    }
}