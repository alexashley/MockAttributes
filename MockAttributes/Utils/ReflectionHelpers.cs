using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MockAttributes.Utils
{
    internal class ReflectionHelpers
    {
        private const BindingFlags DefaultBindingFlags = BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            // BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy;

        public static void InjectMember(object obj, string memberName, object memberValue)
        {
            var member = GetMember(obj.GetType(), memberName);
            if (member.MemberType == MemberTypes.Property)
            {
                (member as PropertyInfo).SetValue(obj, memberValue);
            } else
            {
                (member as FieldInfo).SetValue(obj, memberValue);
            }
        }

        public static MemberInfo GetMember(Type type, string memberName)
        {
            return GetAllMembers(type.GetTypeInfo()).Where(member => member.Name == memberName && IsPropertyOrField(member)).First();
        }

        public static Type GetMemberType(MemberInfo member)
        {
            return member.MemberType == MemberTypes.Property ? (member as PropertyInfo).PropertyType : (member as FieldInfo).FieldType;
        }

        public static IEnumerable<MemberInfo> GetMembersWithAttribute(Type type, Type attribute)
        {
            return GetMembers(type).Where(member => member.GetCustomAttribute(attribute) != null);
        }

        public static IEnumerable<MemberInfo> GetMembers(Type type)
        {
            return GetAllMembers(type.GetTypeInfo()).Where(IsPropertyOrField);
        }

        public static IEnumerable<MemberInfo> GetAllMembers(TypeInfo typeInfo, List<MemberInfo> members = null)
        {
            members = members ?? new List<MemberInfo>();
            if (typeInfo == null)
                return members;

            members.AddRange(typeInfo.DeclaredMembers);

            return GetAllMembers(typeInfo.BaseType?.GetTypeInfo() , members);
        }

        private static bool IsPropertyOrField(MemberInfo member)
        {
            return new List<MemberTypes>() { MemberTypes.Property, MemberTypes.Field }.Contains(member.MemberType);
        }
    }
}
