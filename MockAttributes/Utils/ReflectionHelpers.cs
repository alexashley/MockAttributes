using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MockAttributes.Utils
{
    internal class ReflectionHelpers
    {
        private const BindingFlags DefaultBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public static IEnumerable<FieldInfo> GetFieldsWithAttribute(Type type, Type attribute)
        {
            return GetFields(type).Where(field => field.GetCustomAttribute(attribute) != null);
        }

        public static IEnumerable<FieldInfo> GetFields(Type type)
        {
            return type.GetFields(DefaultBindingFlags);
        }

        public static FieldInfo GetField(Type type, string fieldName)
        {
            return type.GetField(fieldName, DefaultBindingFlags);
        }
    }
}
