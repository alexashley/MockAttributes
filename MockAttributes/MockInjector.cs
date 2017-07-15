using MockAttributes.Exceptions;
using MockAttributes.Extractors;
using MockAttributes.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MockAttributes
{
    public class MockInjector
    {
        public static void Inject(object callerClass, IProxyObjectExtractor extractor = null)
        {
            extractor = extractor ?? new DefaultProxyObjectExtractor();

            var callerType = callerClass.GetType();
            var injectMocksMember = GetInjectMocksClass(callerType);

            if (injectMocksMember == null)
            {
                throw new InjectMocksAttributeMissingException(callerType);
            }

            var memberNamesToMocks = GetMockedClasses(callerType)
                                            .ToDictionary(memberInfo => memberInfo.Name, CreateInstance);

            foreach (var pair in memberNamesToMocks)
            {
                ReflectionHelpers.InjectMember(callerClass, pair.Key, pair.Value);
            }

            var proxyObjects = memberNamesToMocks.Select(pair => extractor.Extract(pair.Value));
            var constructor = ReflectionHelpers.GetMemberType(injectMocksMember).GetConstructor(proxyObjects.Select(o => o.GetType()).ToArray());

            if (constructor == null)
            {
                throw new NoMatchingConstructorException(callerType, proxyObjects);
            }
            var instance = constructor.Invoke(proxyObjects.ToArray());
            ReflectionHelpers.InjectMember(callerClass, injectMocksMember.Name, instance);
        }

        private static MemberInfo GetInjectMocksClass(Type type)
        {
            return ReflectionHelpers.GetMembersWithAttribute(type, typeof(InjectMocks)).FirstOrDefault();
        }

        private static IEnumerable<MemberInfo> GetMockedClasses(Type type)
        {
            return ReflectionHelpers.GetMembersWithAttribute(type, typeof(MockThis));
        }

        private static object CreateInstance(MemberInfo member)
        {
            var unsafeCreator = new UnsafeInstanceCreator();
            return unsafeCreator.CreateInstance(ReflectionHelpers.GetMemberType(member));
        }
    }
}
