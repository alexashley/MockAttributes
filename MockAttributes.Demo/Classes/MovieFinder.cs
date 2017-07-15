using System.Collections.Generic;
using System.Linq;

namespace MockAttributes.Demo.Classes
{
    public class MovieFinder
    {
        private readonly MovieRepository movieRepo;
        private readonly ITranslationService translationService;

        public MovieFinder(MovieRepository movieRepo, ITranslationService translationService)
        {
            this.movieRepo = movieRepo;
            this.translationService = translationService;
        }

        public IEnumerable<Movie> GetMoviesByDirector(string director)
        {
            return movieRepo.GetMovies().Where(movie => movie.Director == director);
        }
    }
}
