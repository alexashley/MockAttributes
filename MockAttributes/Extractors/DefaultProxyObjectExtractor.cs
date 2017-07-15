namespace MockAttributes.Extractors
{
    public class DefaultProxyObjectExtractor : IProxyObjectExtractor
    {
        public object Extract(object obj)
        {
            return obj;
        }
    }
}
