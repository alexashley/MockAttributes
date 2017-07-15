using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MockAttributes.Utils
{
    public class UnsafeInstanceCreator
    {
        public object CreateInstance(Type type)
        {
            var methodInfo = GetMethod<UnsafeInstanceCreator>(creator => creator.CreateInstance<Object>());
            var genericMethod = methodInfo.MakeGenericMethod(type);

            return genericMethod.Invoke(this, null);
        }

        private T CreateInstance<T>() where T : class
        {
            return Activator.CreateInstance<T>();
        }

        // https://stackoverflow.com/a/1408154/7279065
        public MethodInfo GetMethod<T>(Expression<Action<T>> expr)
        {
            return ((MethodCallExpression)expr.Body)
                .Method
                .GetGenericMethodDefinition();
        }
    }
}
