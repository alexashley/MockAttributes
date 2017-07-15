using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MockAttributes.Utils;

namespace MockAttributes
{
    public class MockInjector
    {
        public static void Inject(object callerClass)
        {
            var callerType = callerClass.GetType();

            var injectMocksFieldInfo = GetInjectMocksClass(callerType);

            if (injectMocksFieldInfo == null)
            {
                throw new InjectMocksAttributeMissingException(callerType);
            }
            var unsafeCreator = new UnsafeInstanceCreator();
            var fieldsToMockedClasses = GetMockedClasses(callerType)
                                            .ToDictionary(fieldInfo => fieldInfo.Name, fieldInfo => unsafeCreator.CreateInstance(fieldInfo.FieldType));

            foreach (var pair in fieldsToMockedClasses)
            {
                InjectField(callerClass, pair.Key, pair.Value);
            }
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
