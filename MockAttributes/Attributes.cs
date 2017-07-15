using System;

namespace MockAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class InjectMocks : Attribute
    { }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class MockThis : Attribute
    {}
}
