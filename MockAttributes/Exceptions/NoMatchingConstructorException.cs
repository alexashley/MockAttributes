using System;
using System.Collections.Generic;
using System.Linq;

namespace MockAttributes.Exceptions
{
    public class NoMatchingConstructorException : Exception
    {
        public NoMatchingConstructorException(Type classType, IEnumerable<object> constructorArgs)
            : base(BuildMessage(classType, constructorArgs))
        {
        }

        private static string BuildMessage(Type classType, IEnumerable<object> constructorArgs)
        {
            var argsList = string.Join(", ", constructorArgs.Select(arg => arg.GetType().Name));

            return $"No matching constructor for class {classType.Name} with parameters {argsList}";
        }
    }
}
