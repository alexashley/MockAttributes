using System;
using System.Collections.Generic;
using System.Text;

namespace MockAttributes.Demo.Classes
{
    public class MovieRepository
    {
        public virtual IEnumerable<Movie> GetMovies()
        {
            throw new NotImplementedException();
        }
    }
}
