using System;

namespace MockAttributes
{
    public class InjectMocksAttributeMissingException : Exception
    {
        public InjectMocksAttributeMissingException(Type classType)
            : base($"Missing [{nameof(InjectMocks)}] on class " + classType.Name)
        {

        }
    }
}
