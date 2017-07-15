using MockAttributes.Demo.Classes;
using MockAttributes.Extractors;
using Moq;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace MockAttributes.Demo
{
    public class MovieFinderInjectedFieldsTest : MovieFinderTest
    {
        # pragma warning disable CS0649
        [MockThis]
        private Mock<MovieRepository> movieRepo;

        [MockThis]
        private Mock<ITranslationService> translationService;

        [InjectMocks]
        private MovieFinder movieFinder;

        # pragma warning restore CS0649

        public MovieFinderInjectedFieldsTest()
        {
            MockInjector.Inject(this, new MoqProxyObjectExtractor());

            allMovies = GetMovieList();
        }

        [Fact]
        public void ShouldReturnKubrickFilm()
        {
            var expectedMovie = new Movie()
            {
                Name = "Full Metal Jacket",
                Director = "Stanley Kubrick",
                Language = "en-us"
            };
            movieRepo
                .Setup(repo => repo.GetMovies())
                .Returns(allMovies.Append(CloneMovie(expectedMovie)));
            translationService
                .Setup(service => service.Translate(It.IsAny<string>(), It.IsAny<CultureInfo>()))
                .Returns("Have you ever seen a Commie drink a glass of water?");

            var actualMovie = movieFinder.GetMoviesByDirector("Stanley Kubrick").First();

            Assert.Equal(expectedMovie.Director, actualMovie.Director);
            Assert.Equal(expectedMovie.Name, actualMovie.Name);
            Assert.Equal(expectedMovie.Language, actualMovie.Language);
        }
    }
}
