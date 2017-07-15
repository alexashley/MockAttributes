using MockAttributes.Demo.Classes;
using System.Collections.Generic;

namespace MockAttributes.Demo
{
    public class MovieFinderTest
    {
        protected List<Movie> allMovies;

        protected List<Movie> GetMovieList()
        {
            return new List<Movie>()
            {
                new Movie()
                {
                    Name = "O, Brother Where Art Thou",
                    Director = "Coen Brothers",
                    Language = "en-us"
                },
                new Movie()
                {
                    Name = "Clerks",
                    Director = "Kevin Smith",
                    Language = "en-us"
                }
            };
        }

        protected Movie CloneMovie(Movie m)
        {
            return new Movie()
            {
                Name = m.Name,
                Director = m.Director,
                Language = m.Language
            };
        }
    }
}
