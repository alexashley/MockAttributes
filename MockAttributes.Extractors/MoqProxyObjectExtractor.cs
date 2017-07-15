using Moq;

namespace MockAttributes.Extractors
{
    public class MoqProxyObjectExtractor : IProxyObjectExtractor
    {
        public object Extract(object obj)
        {
            return (obj as Mock).Object;
        }
    }
}
