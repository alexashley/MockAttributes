using MockAttributes.Demo.Classes;
using MockAttributes.Extractors;
using Moq;
using System.Globalization;
using System.Linq;
using Xunit;

namespace MockAttributes.Demo
{
    public class MovieFinderInjectedPropertiesTest : MovieFinderTest
    {
        [MockThis]
        private Mock<MovieRepository> MovieRepo { get; set; }

        [MockThis]
        private Mock<ITranslationService> TranslationService { get; set; }

        [InjectMocks]
        private MovieFinder MovieFinder { get; set; }

        public MovieFinderInjectedPropertiesTest()
        {
            MockInjector.Inject(this);

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
            MovieRepo
                .Setup(repo => repo.GetMovies())
                .Returns(allMovies.Append(CloneMovie(expectedMovie)));
            TranslationService
                .Setup(service => service.Translate(It.IsAny<string>(), It.IsAny<CultureInfo>()))
                .Returns("Have you ever seen a Commie drink a glass of water?");

            var actualMovie = MovieFinder.GetMoviesByDirector("Stanley Kubrick").First();

            Assert.Equal(expectedMovie.Director, actualMovie.Director);
            Assert.Equal(expectedMovie.Name, actualMovie.Name);
            Assert.Equal(expectedMovie.Language, actualMovie.Language);
        }
    }
}
