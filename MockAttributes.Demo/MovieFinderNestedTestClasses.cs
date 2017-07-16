using MockAttributes.Demo.Classes;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xunit;

namespace MockAttributes.Demo
{
    public class MovieFinderNestedTestClasses : MovieFinderTest
    {
        [MockThis]
        private Mock<MovieRepository> MovieRepo { get; set; }

        [MockThis]
        private Mock<ITranslationService> TranslationService { get; set; }

        [InjectMocks]
        private MovieFinder MovieFinder { get; set; }

        public MovieFinderNestedTestClasses()
        {
            MockInjector.Inject(this);

            allMovies = GetMovieList();
        }

        public class GetDirectorsTest : MovieFinderNestedTestClasses
        {
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
}
