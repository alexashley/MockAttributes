using Moq;
using System;

namespace MockAttributes.Extractors
{
    public class MoqProxyObjectExtractor : IProxyObjectExtractor
    {
        public object Extract(object obj)
        {
            return (obj as Mock).Object;
            //var baseClassType = proxyObj.GetType().DeclaringType;

            //return Convert.ChangeType(proxyObj, baseClassType);
        }
    }
}
