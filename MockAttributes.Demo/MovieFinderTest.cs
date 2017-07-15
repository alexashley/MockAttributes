using MockAttributes.Demo.Classes;
using MockAttributes.Extractors;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace MockAttributes.Demo
{
    public class MovieFinderTest
    {
        [MockThis]
        private Mock<MovieRepository> movieRepo;

        [MockThis]
        private Mock<ITranslationService> translationService;

        [InjectMocks]
        private MovieFinder movieFinder;
        private List<Movie> allMovies;

        public MovieFinderTest()
        {
            MockInjector.Inject(this, new MoqProxyObjectExtractor());

            allMovies = GetMovieList();
        }

        private List<Movie> GetMovieList()
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
                    Name = "Full Metal Jacket",
                    Director = "Stanley Kubrick",
                    Language = "en-us"
                }
            };
        }

        [Fact]
        public void ShouldReturnKubrickFilms()
        {
            var expectedMovies = new List<Movie>() { allMovies.ElementAt(1) };
            movieRepo
                .Setup(repo => repo.GetMovies())
                .Returns(allMovies);

            var actualMovies = movieFinder.GetMoviesByDirector("Stanley Kubrick");

            Assert.True(expectedMovies.SequenceEqual(actualMovies));
        }
    }
}
