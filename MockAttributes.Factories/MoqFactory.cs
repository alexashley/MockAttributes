using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MockAttributes.Factories
{
    public class MockFactory : IMockFactory
    {
        public object CreateMock(Type mockType)
        {
            var methodInfo = GetMethod<MockFactory>(x => x.CreateInstance<Object>()); 
            var genericMethod = methodInfo.MakeGenericMethod(mockType);

            return genericMethod.Invoke(this, null);
        }

        private T CreateInstance<T>() where T : class
        {
            return Activator.CreateInstance<T>();
        }

        // https://stackoverflow.com/a/1408154/7279065
        public static MethodInfo GetMethod<T>(Expression<Action<T>> expr)
        {
            return ((MethodCallExpression)expr.Body)
                .Method
                .GetGenericMethodDefinition();
        }
    }
}
