using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MockAttributes.Utils;
using MockAttributes.Extractors;
using MockAttributes.Exceptions;

namespace MockAttributes
{
    public class MockInjector
    {
        public static void Inject(object callerClass, IProxyObjectExtractor extractor = null)
        {
            extractor = extractor ?? new DefaultProxyObjectExtractor();

            var callerType = callerClass.GetType();

            var injectMocksFieldInfo = GetInjectMocksClass(callerType);

            if (injectMocksFieldInfo == null)
            {
                throw new InjectMocksAttributeMissingException(callerType);
            }
            var unsafeCreator = new UnsafeInstanceCreator();
            var fieldsToMockedClasses = GetMockedClasses(callerType)
                                            .ToDictionary(fieldInfo => fieldInfo.Name, fieldInfo => unsafeCreator.CreateInstance(fieldInfo.FieldType));

            // inject mocked objects into test class
            foreach (var pair in fieldsToMockedClasses)
            {
                InjectField(callerClass, pair.Key, pair.Value);
            }

            // find constructor
            var proxyObjects = fieldsToMockedClasses.Select(pair => extractor.Extract(pair.Value));
            var constructor = injectMocksFieldInfo.FieldType.GetConstructor(proxyObjects.Select(o => o.GetType()).ToArray());

            if (constructor == null)
            {
                throw new NoMatchingConstructorException(callerType, proxyObjects);
            }

            // inject instantiated object into test class
            var instance = constructor.Invoke(proxyObjects.ToArray());
            InjectField(callerClass, injectMocksFieldInfo.Name, instance);
        }

        private static void InjectField(object obj, string fieldName, object fieldValue)
        {
            ReflectionHelpers.GetField(obj.GetType(), fieldName).SetValue(obj, fieldValue);
        }

        private static FieldInfo GetInjectMocksClass(Type type)
        {
            return ReflectionHelpers.GetFieldsWithAttribute(type, typeof(InjectMocks)).FirstOrDefault();
        }

        private static IEnumerable<FieldInfo> GetMockedClasses(Type type)
        {
            return ReflectionHelpers.GetFieldsWithAttribute(type, typeof(MockThis));
        }


    }
}
